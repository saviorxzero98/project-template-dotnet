namespace CommonEx.Utilities.BackgroundTasks
{
    public class BackgroundTaskContext
    {
        private readonly Func<CancellationToken, Task> _task;
        private readonly Action<Exception> _exception;


        public BackgroundTaskContext(Func<CancellationToken, Task> task)
        {
            _task = task;
            _exception = (exception) => { };
        }
        public BackgroundTaskContext(Func<CancellationToken, Task> task, Action<Exception> exception)
        {
            _task = task;
            _exception = exception;
        }

        public Task ExecuteAsync(CancellationToken token)
        {
            return _task(token);
        }

        public void OnException(Exception e)
        {
            _exception(e);
        }
    }
}
