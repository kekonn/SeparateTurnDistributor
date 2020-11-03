using System;

namespace ChessClock.Model
{
    public class Player
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Player)
            {
                return Equals(obj as Player);
            }

            return base.Equals(obj);
        }

        private bool Equals(Player otherPlayer)
        {
            return otherPlayer != null && Id == otherPlayer.Id;
        }

        public override string ToString()
        {
            return $"{Name} - {Id}";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static Player One => new Player { Name = "Player One" };
    }
}
