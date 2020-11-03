using Azure.Storage.Blobs;
using ChessClock.Model;
using ChessClock.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Cosmos.Table.Queryable;
using System.Threading.Tasks;
using System.IO;
using System.Buffers;

namespace ChessClock.Data.Azure
{
    public class AzureBlobGameRepository : IGameRepository
    {

        internal static readonly string[] ColumnKeys = new string[] { "Name", "Players", "CurrentPlayer", "SavefileName" };

        private readonly string connectionString;
        private readonly CloudStorageAccount cloudStorageAccount;
        private readonly CloudTableClient tableClient;

        private const string TableName = "games";
        private const string ContainerName = "games";

        private List<Game> games;

        public AzureBlobGameRepository(string connectionString)
        {
            cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            tableClient = cloudStorageAccount.CreateCloudTableClient(new TableClientConfiguration());
            this.connectionString = connectionString;
        }

        public void Add(Game game)
        {
            EnsureGamesLoaded();

            if (games.Contains(game))
            {
                games.Remove(game);
            }

            games.Add(game);

            var gamesTable = GetGamesTable();

            var insertOp = TableOperation.InsertOrReplace(new AzureGameEntity(game));
            gamesTable.ExecuteAsync(insertOp).GetAwaiter().GetResult();
        }

        public Game FirstOrDefault(Func<Game, bool> predicate)
        {
            EnsureGamesLoaded();

            return games.FirstOrDefault(predicate);
        }

        public void Remove(Game game)
        {
            EnsureGamesLoaded();

            games.Remove(game);

            var gamesTable = GetGamesTable();

            var deleteOp = TableOperation.Delete(new AzureGameEntity(game));

            gamesTable.ExecuteAsync(deleteOp).GetAwaiter().GetResult();
        }

        public void UploadSaveFile(Game game, string directory)
        {
            var fullSaveName = Filesystem.GetHotSeatSaveFileFullName(directory, game.SavefileName);
            if (!File.Exists(fullSaveName))
            {
                throw new FileNotFoundException(null, fullSaveName);
            }

            var blobClient = CreateBlobClientForGame(game);
            blobClient.DeleteIfExistsAsync().GetAwaiter().GetResult();
            blobClient.UploadAsync(fullSaveName).GetAwaiter().GetResult();
        }

        private BlobClient CreateBlobClientForGame(Game game) => new BlobClient(connectionString, ContainerName, Filesystem.GetSaveFileName(game));

        private void EnsureGamesLoaded()
        {
            if (games == null)
            {
                games = GetGamesAsync().GetAwaiter().GetResult();
            }
        }

        private CloudTable GetGamesTable()
        {
            return tableClient.GetTableReference(TableName);
        }

        private async Task<List<Game>> GetGamesAsync()
        {
            var gamesTable = GetGamesTable();

            _ = gamesTable.CreateIfNotExistsAsync().GetAwaiter().GetResult();

            TableContinuationToken continuationToken = null;
            var games = new List<Game>();

            do
            {
                var queryResult = await gamesTable.ExecuteQuerySegmentedAsync(new TableQuery(), Resolve, continuationToken);

                games.AddRange(queryResult.Results);

                continuationToken = queryResult.ContinuationToken;

            } while (continuationToken != null);

            return games;
        }

        public IEnumerable<Game> All()
        {
            EnsureGamesLoaded();

            return GetGamesAsync().GetAwaiter().GetResult();
        }

        private Game Resolve(string pk, string rk, DateTimeOffset ts, IDictionary<string, EntityProperty> props, string etag)
        {
            var validProperties = ValidateProperties(props.Keys);

            if (!validProperties)
            {
                return new Game();
            }

            Guid id;
            Guid.TryParse(rk, out id);

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

        internal static bool ValidateProperties(IEnumerable<string> propertyNames)
        {
            return propertyNames.All(p => ColumnKeys.Contains(p));
        }

        public IEnumerable<Player> AllPlayers()
        {
            EnsureGamesLoaded();

            return games.SelectMany(g => g.Players).Distinct().ToArray();
        }

        public IEnumerable<Game> AllForPlayer(Player player)
        {
            EnsureGamesLoaded();

            return games.Where(g => g.Players.Contains(player)).ToArray();
        }

        public bool HasUpdated(Game game)
        {
            EnsureGamesLoaded();

            var gamesTable = GetGamesTable();

            var query = TableOperation.Retrieve(AzureGameEntity.PartitionKeyConstant, game.Id.ToString(), new List<string>() { "Timestamp" });

            var queryResult = gamesTable.ExecuteAsync(query).GetAwaiter().GetResult().Result as DynamicTableEntity;

            var gameLastUpdated = queryResult.Timestamp;

            return gameLastUpdated > game.LastUpdated;
        }

        public DateTimeOffset GetSaveGameLastModifiedTime(Game game)
        {
            var saveClient = CreateBlobClientForGame(game);
            var properties = saveClient.GetPropertiesAsync().GetAwaiter().GetResult().Value;

            return properties.LastModified;
        }

        public Game UpdateGameAndSaveFile(Game game, string savefileFullPath)
        {
            var gamesTable = GetGamesTable();

            var rowFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, game.Id.ToString());
            var partFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, AzureGameEntity.PartitionKeyConstant);

            var query = new TableQuery<AzureGameEntity>().Where(TableQuery.CombineFilters(rowFilter, TableOperators.And, partFilter));
            var newGame = gamesTable.ExecuteQuery(query).First();

            var blobClient = CreateBlobClientForGame(game);
            blobClient.DownloadToAsync(savefileFullPath).GetAwaiter().GetResult();
            
            return newGame.ToGame();
        }
    }
}
