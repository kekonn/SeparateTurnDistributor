using ChessClock.Model;
using System.Linq;
using System.Collections.Generic;
using static System.Windows.Forms.CheckedListBox;

namespace ChessClock.UI
{
    internal static class ControlExtensions
    {
        internal static List<Player> ToPlayerList(this CheckedItemCollection checkedItems)
        {
            return checkedItems.OfType<Player>().ToList();
        }
    }
}
