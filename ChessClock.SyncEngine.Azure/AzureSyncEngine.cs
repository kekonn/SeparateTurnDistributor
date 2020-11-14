using Azure.Storage.Blobs;
using ChessClock.Model;
using ChessClock.SyncEngine.Azure.Extensions;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Cosmos.Table.Queryable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChessClock.SyncEngine.Events;
using Microsoft.Extensions.Logging;

namespace ChessClock.SyncEngine.Azure
{
    public class AzureSyncEngine : BaseSyncEngine
    {
        private static readonly string[] ColumnKeys = { "Name", "Players", "CurrentPlayer", "SavefileName" };

        private readonly string connectionString;
        private readonly CloudTableClient tableClient;
        private readonly string tableName;
        private readonly string containerName;
        private readonly Civ6Filesystem filesystem;

        private bool tableExists = false;

        /// <summary>
        /// Creates a new instance of the AzureSyncEngine
        /// </summary>
        /// <param name="options">Options for the AzureSyncEngine</param>
        /// <param name="autoSyncStrategy">The IAutoSyncStrategy to use</param>
        /// <param name="logger">The logger to use</param>
        public AzureSyncEngine(AzureSyncEngineOptions options, IAutoSyncStrategy? autoSyncStrategy, ILogger<ISyncEngine> logger, Civ6Filesystem filesystem)
            : base(options.SystemPlayer, autoSyncStrategy, logger)
        {
            connectionString = options.ConnectionString;
            tableName = options.TableName;
            containerName = options.ContainerName;
            this.filesystem = filesystem;
            
            tableClient = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient(new TableClientConfiguration());
        }

        public override async ValueTask PassTurnAsync(Game game)
        {
            Logger.LogDebug($"Moving turn on game {game}");

            game.NextTurn();
            await InsertOrMergeGame(game);
        }

        public override async ValueTask SubmitTurnAsync(Game game)
        {
            Logger.LogDebug($"Submitting turn for game {game}");

            await PassTurnAsync(game);
            await UploadSavefile(game);
        }

        protected override void Sync(Game game)
        {
            Logger.LogDebug($"Syncing game {game}");

            var newGame = CreateGameSource().FirstOrDefault(g => g.Id == game.Id);
            if (newGame is null)
            {
                Logger.LogDebug($"Game only exists locally");
                return;
            }

            if (game.LastUpdated > newGame.LastUpdated)
            {
                Logger.LogDebug($"Local game is newer");
                return;
            }

            var successfullySyncedArgs = new SuccessfullySyncedEventArgs(newGame, DateTimeOffset.Now);
            OnSuccessfullySynced(successfullySyncedArgs);

            if (newGame.CurrentPlayer != SystemPlayer)
            {
                Logger.LogDebug($"Pulled in remote game, but it's not our turn");
                return;
            }

            DownloadSavefile(newGame).GetAwaiter().GetResult();

            var myTurnArgs = new MyTurnEventArgs(newGame);
            OnMyTurnReached(myTurnArgs);
        }

        protected override IQueryable<Game> CreateGameSource()
        {
            var tableRef = tableClient.GetTableReference(tableName);

            if (!tableExists)
            {
                tableExists = tableRef.CreateIfNotExists();
            }

            return tableRef.CreateQuery<AzureCiv6GameEntity>().Resolve(Resolve);
        }

        protected override DateTimeOffset GetGameLastModifiedTime(Game game)
        {
            var remoteGame = CreateGameSource().FirstOrDefault(g => g.Id == game.Id);

            return remoteGame?.LastUpdated ?? DateTimeOffset.MinValue;
        }

        protected override DateTimeOffset GetLocalSavefileLastModifiedTime(Game game)
        {
            throw new NotImplementedException();
        }

        protected override DateTimeOffset GetRemoteSavefileLastModifiedTime(Game game)
        {
            var blobClient = CreateBlobClientForGame(game);
            var properties = blobClient.GetProperties();

            return properties.Value.LastModified;
        }

        private static Game Resolve(string pk, string rk, DateTimeOffset ts, IDictionary<string, EntityProperty> props, string etag)
        {
            var validProperties = ValidateProperties(props.Keys);

            if (!validProperties)
            {
                return new Game();
            }

            Guid.TryParse(rk, out var id);

            var name = props[ColumnKeys[0]].StringValue;
            var playersJson = props[ColumnKeys[1]].StringValue;
            var playerJson = props[ColumnKeys[2]].StringValue;

            return new Game(name, playersJson.ToPlayers(), playerJson.ToPlayer())
            {
                SavefileName = props[ColumnKeys[3]].StringValue,
                LastUpdated = ts,
                Id = id
            };
        }

        private static bool ValidateProperties(IEnumerable<string> propertyNames)
        {
            return propertyNames.All(p => ColumnKeys.Contains(p));
        }

        private BlobClient CreateBlobClientForGame(Game game) => new BlobClient(connectionString, containerName, Civ6Filesystem.GetSaveFileName(game));

        private async ValueTask InsertOrMergeGame(Game game)
        {
            var gamesTable = tableClient.GetTableReference(tableName);
            await gamesTable.CreateIfNotExistsAsync();

            var insert = TableOperation.InsertOrMerge(new AzureCiv6GameEntity(game));

            await gamesTable.ExecuteAsync(insert);
        }

        private async ValueTask UploadSavefile(Game game)
        {
            var filePath = GetFullSavefilePath(game);
            var blobClient = CreateBlobClientForGame(game);

            Logger.LogDebug($"Uploading savefile at {filePath} for game {game}");

            var alreadyExists = (await blobClient.ExistsAsync()).Value;
            if (alreadyExists)
            {
                try
                {
                    var properties = (await blobClient.GetPropertiesAsync()).Value;
                    var version = Convert.ToInt32(properties.VersionId) + 1;

                    await blobClient.WithVersion(version.ToString()).UploadAsync(File.OpenRead(filePath));
                }
                catch (Exception e)
                {
                    Logger.LogError(e, $"Error occurred when uploading {game}");
                    throw;
                }
            }
            else
            {
                try
                {
                    await blobClient.WithVersion("1").UploadAsync(File.OpenRead(filePath));
                }
                catch (Exception e)
                {
                    Logger.LogError(e, $"Error occurred when uploading {game} for the first time");
                    throw;
                }
            }
        }

        private async ValueTask DownloadSavefile(Game game)
        {
            var filePath = GetFullSavefilePath(game);

            Logger.LogDebug($"Downloading savefile for game {game} to {filePath}");

            try
            {
                var blobClient = CreateBlobClientForGame(game);
                var remoteFileExists = (await blobClient.ExistsAsync()).Value;

                if (!remoteFileExists)
                {
                    Logger.LogDebug($"No remote savefile for game {game} exists");
                    return;
                }

                await blobClient.DownloadToAsync(filePath);
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error occurred when downloading save file for game {game}");
                throw;
            }
        }

        private string GetFullSavefilePath(Game game)
        {
            return filesystem.GetHotSeatSaveFileFullName(game);
        }
    }
}
