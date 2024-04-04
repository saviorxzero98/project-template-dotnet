using System.Data;

namespace CommonEx.Database.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Get Database Connection
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Saves the change.
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangeAsync();
    }
}
