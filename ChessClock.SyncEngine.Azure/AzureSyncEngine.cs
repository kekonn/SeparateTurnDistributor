﻿using Azure.Storage.Blobs;
using ChessClock.Data;
using ChessClock.Model;
using ChessClock.SyncEngine.Azure.Extensions;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Cosmos.Table.Queryable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChessClock.SyncEngine.Azure
{
    public class AzureSyncEngine : BaseSyncEngine
    {
        private static readonly string[] ColumnKeys = new string[] { "Name", "Players", "CurrentPlayer", "SavefileName" };

        private readonly string connectionString;
        private readonly CloudStorageAccount cloudStorageAccount;
        private readonly CloudTableClient tableClient;
        private readonly string tableName;
        private readonly string containerName;

        public AzureSyncEngine(AzureSyncEngineOptions options) : base(options.SystemPlayer)
        {
            connectionString = options.ConnectionString;
            tableName = options.TableName;
            cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            tableClient = cloudStorageAccount.CreateCloudTableClient(new TableClientConfiguration());
            containerName = options.ContainerName;
        }

        public override async ValueTask PassTurnAsync(Game game)
        {
            game.NextTurn();
            await InsertOrMergeGame(game);
        }

        public override ValueTask SubmitTurnAsync(Game game)
        {
            throw new NotImplementedException();
        }

        protected override void Sync(Game game)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Game> CreateGameSource()
        {
            var query = new TableQuery<AzureCiv6GameEntity>()
                .Resolve(Resolve);

            return query;
        }

        protected override DateTimeOffset GetGameLastModifiedTime(Game game)
        {
            throw new NotImplementedException();
        }

        protected override DateTimeOffset GetRemoteSavefileLastModifiedTime(Game game)
        {
            var blobClient = CreateBlobClientForGame(game);
            var properties = blobClient.GetProperties();

            return properties.Value.LastModified;
        }

        private Game Resolve(string pk, string rk, DateTimeOffset ts, IDictionary<string, EntityProperty> props, string etag)
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

            var result = await gamesTable.ExecuteAsync(insert);
        }
    }
}