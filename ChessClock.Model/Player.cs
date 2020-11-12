using System;

namespace ChessClock.Model
{
    public class Player : IEquatable<Player>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public static bool operator ==(Player a, Player b)
        {
            return a?.Equals(b) ?? false;
        }

        public static bool operator !=(Player a, Player b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{Name} - {Id}";
        }

        public static Player One => new Player { Name = "Player One" };

        public bool Equals(Player? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
