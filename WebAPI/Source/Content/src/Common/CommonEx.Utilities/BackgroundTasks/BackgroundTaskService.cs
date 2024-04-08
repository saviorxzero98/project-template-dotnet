using Microsoft.Extensions.Hosting;

namespace CommonEx.Utilities.BackgroundTasks
{
    public class BackgroundTaskService : BackgroundService
    {
        private readonly BackgroundTaskQueue _taskQueue;

        public BackgroundTaskService(BackgroundTaskQueue taskQueue)
        {
            _taskQueue = taskQueue;
        }

        /// <summary>
        /// 執行 Background Task
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var taskContext = await _taskQueue.DequeueAsync(cancellationToken);

                Task task = new Task(async () =>
                {
                    try
                    {
                        await taskContext.ExecuteAsync(cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        taskContext.OnException(e);
                    }
                });
                task.Start();
            }
        }

        public override void Dispose()
        {
            _taskQueue.Close();
            base.Dispose();
        }
    }
}
