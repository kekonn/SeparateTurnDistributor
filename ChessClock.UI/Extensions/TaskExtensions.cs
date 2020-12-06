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

        public static TResult Await<TResult>(this Task<TResult> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}
