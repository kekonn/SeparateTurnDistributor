using ChessClock.Model;
using ChessClock.SyncEngine.Azure.Extensions;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace ChessClock.SyncEngine.Azure
{
    internal class AzureCiv6GameEntity : TableEntity
    {
        internal const string PartitionKeyConstant = "CIV6";

        public string Name { get; set; }
        public string Players { get; set; }
        public string CurrentPlayer { get; set; }
        public string SavefileName { get; set; } = string.Empty;

        public AzureCiv6GameEntity(Game game) : base()
        {
            PartitionKey = PartitionKeyConstant;
            RowKey = game.Id.ToString();
            Name = game.Name;
            Players = game.Players.ToJson();
            CurrentPlayer = game.CurrentPlayer.ToJson();
            SavefileName = game.SavefileName;
            Timestamp = game.LastUpdated;
        }

        public AzureCiv6GameEntity() : base()
        {
        }

        public Game ToGame()
        {
            return new Game(Name, Players.ToPlayers(), CurrentPlayer.ToPlayer()) { Id = Guid.Parse(RowKey), LastUpdated = this.Timestamp, SavefileName = this.SavefileName };
        }
    }
}
