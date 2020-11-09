using ChessClock.Model;

namespace ChessClock.SyncEngine.Azure
{
    public class AzureSyncEngineOptions
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public string ContainerName { get; set; }
        public Player SystemPlayer { get; set; }
    }
}
