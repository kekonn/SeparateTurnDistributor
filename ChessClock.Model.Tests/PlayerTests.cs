using System;
using Xunit;

namespace ChessClock.Model.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerOneDoesntChange()
        {
            Assert.NotNull(Player.One);
            Assert.True(Player.One == Player.One);
        }

        [Fact]
        public void PlayerOneNotEqualToRandomPlayer()
        {
            Assert.NotNull(Player.One);
            var playerA = new Player() {Name = "Jonas"};

            Assert.True(Player.One != playerA);
        }
    }
}
