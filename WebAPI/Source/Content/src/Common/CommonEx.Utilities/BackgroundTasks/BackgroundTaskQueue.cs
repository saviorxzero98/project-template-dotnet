using System.Collections.Concurrent;

namespace CommonEx.Utilities.BackgroundTasks
{
    public class BackgroundTaskQueue
    {
        public bool IsDisposed { get; private set; }
        private readonly ConcurrentQueue<BackgroundTaskContext> _taskQueue;
        private readonly SemaphoreSlim _signal;

        public BackgroundTaskQueue()
        {
            _taskQueue = new ConcurrentQueue<BackgroundTaskContext>();
            _signal = new SemaphoreSlim(0);
        }


        /// <summary>
        /// 加入 Background Task
        /// </summary>
        /// <param name="task"></param>
        public void Enqueue(Func<CancellationToken, Task> task)
        {
            Enqueue(new BackgroundTaskContext(task));
        }

        /// <summary>
        /// 加入 Background Task
        /// </summary>
        /// <param name="task"></param>
        /// <param name="exception"></param>
        public void Enqueue(Func<CancellationToken, Task> task, Action<Exception> exception)
        {
            Enqueue(new BackgroundTaskContext(task, exception));
        }

        /// <summary>
        /// 加入 Background Task
        /// </summary>
        /// <param name="taskContext"></param>
        public void Enqueue(BackgroundTaskContext taskContext)
        {
            _taskQueue.Enqueue(taskContext);
            _signal.Release();
        }

        /// <summary>
        /// 取出 Background Task
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BackgroundTaskContext> DequeueAsync(CancellationToken token)
        {
            await _signal.WaitAsync(token);
            _taskQueue.TryDequeue(out var ticket);

            return ticket;
        }


        public void Close()
        {
            if (!IsDisposed)
            {
                _signal.Dispose();
                IsDisposed = true;
            }
        }
    }
}
