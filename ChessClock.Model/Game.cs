using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessClock.Model
{
    public class Game : IEquatable<Game>
    {
        /// <summary>
        /// Unique Game Id
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Game Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Player's list
        /// </summary>
        public Player[] Players { get; private set; } = new Player[4];
        /// <summary>
        /// Current Player
        /// </summary>
        public Player CurrentPlayer { get; private set; }
        /// <summary>
        /// Savefile Name
        /// </summary>
        public string SavefileName { get; set; } = string.Empty;
        /// <summary>
        /// Last time the game data was updated (NOT THE SAVE FILE)
        /// </summary>
        public DateTimeOffset LastUpdated { get; set; } = DateTimeOffset.UtcNow;

        public Game()
        {
            Name = "New Game";
            CurrentPlayer = Player.One;
            Players[0] = CurrentPlayer;
        }

        public Game(string name, IEnumerable<Player> players, Player currentPlayer = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("This is not a valid player name", nameof(name));
            }

            Name = name;

            Players = players.ToArray();

            if (Players.Length < 2)
            {
                throw new ArgumentException("A game must contain at least 2 players");
            }

            currentPlayer ??= Players.First();

            if (!Players.Contains(currentPlayer))
            {
                throw new ArgumentException($"Player {currentPlayer} is not part of Game {name}", nameof(currentPlayer));
            }

            CurrentPlayer = currentPlayer;
        }

        public virtual void NextTurn()
        {
            var currentPlayerIndex = Array.IndexOf(Players, CurrentPlayer);

            if (++currentPlayerIndex == Players.Length)
            {
                currentPlayerIndex = 0;
            }

            CurrentPlayer = Players[currentPlayerIndex];
        }

        public override string ToString()
        {
            return $"{Name} - {Id}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Game) obj);
        }

        public static bool operator ==(Game left, Game right)
        {
            return EqualityComparer<Game>.Default.Equals(left, right);
        }

        public static bool operator !=(Game left, Game right)
        {
            return !(left == right);
        }

        public bool Equals(Game? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && Name == other.Name && Players.Equals(other.Players) && CurrentPlayer.Equals(other.CurrentPlayer) && SavefileName == other.SavefileName && LastUpdated.Equals(other.LastUpdated);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Players, CurrentPlayer, SavefileName, LastUpdated);
        }
    }
}
