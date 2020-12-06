using System.Threading.Tasks;

namespace ChessClock.UI.Extensions
{
    public static class TaskExtensions
    {
        public static void Await(this Task task)
        {
            task.GetAwaiter().GetResult();
        }

        public static void Await(this ValueTask task)
        {
            task.GetAwaiter().GetResult();
        }
    }
}
