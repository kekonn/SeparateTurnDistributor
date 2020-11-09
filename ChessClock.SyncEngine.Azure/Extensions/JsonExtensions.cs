using ChessClock.Model;
using System.Collections.Generic;
using System.Text.Json;

namespace ChessClock.SyncEngine.Azure.Extensions
{
    public static class JsonExtensions
    {
        public static Player ToPlayer(this string json)
        {
            return JsonSerializer.Deserialize<Player>(json);
        }

        public static Player[] ToPlayers(this string json)
        {
            return JsonSerializer.Deserialize<Player[]>(json);
        }

        public static string ToJson(this Player player)
        {
            return JsonSerializer.Serialize(player);
        }

        public static string ToJson(this IEnumerable<Player> players)
        {
            return JsonSerializer.Serialize(players);
        }
    }
}
