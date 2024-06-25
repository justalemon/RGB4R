using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using GTA;

namespace RGB4R.Extensions;

/// <summary>
/// Extension for running tasks with SHVDN safely.
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Yield<T>(this Task<T> task)
    {
        Task<T> newTask = Task.Run(() => task);

        while (!newTask.IsCompleted)
        {
            Script.Yield();
        }

        if (newTask.IsFaulted && newTask.Exception != null && newTask.Exception.InnerException != null)
        {
            ExceptionDispatchInfo.Capture(newTask.Exception.InnerException).Throw();
        }

        return newTask.Result;
    }
}
