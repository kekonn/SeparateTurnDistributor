using ChessClock.Model;
using System;
using System.Collections.Generic;

namespace ChessClock.Data
{
    public interface IGameRepository
    {
        void Add(Game game);
        void Remove(Game game);
        IEnumerable<Game> All();
        IEnumerable<Game> AllForPlayer(Player player);
        Game FirstOrDefault(Func<Game, bool> predicate);
        IEnumerable<Player> AllPlayers();
        void UploadSaveFile(Game game, string directory);
        bool HasUpdated(Game game);
        DateTimeOffset GetSaveGameLastModifiedTime(Game game);
        Game UpdateGameAndSaveFile(Game game, string savefileFullPath);
    }
}
