namespace BGCTest.Api.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit transaction to the database
        /// </summary>
        /// <returns></returns>
        int Commit();
        /// <summary>
        /// Commit transaction to the database
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///  Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        ///  is called after the changes have been sent successfully to the database.
        /// </param>
        /// <returns></returns>
        int Commit(bool acceptAllChangesOnSuccess);
        /// <summary>
        /// Commit transaction to the database
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///  Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        ///  is called after the changes have been sent successfully to the database.
        /// </param>
        /// <param name="cancellationToken">
        /// A System.Threading.CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <returns></returns>
        Task<int> CommitAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        /// <summary>
        /// Commit transaction to the database
        /// </summary>
        /// <param name="cancellationToken">
        /// A System.Threading.CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <returns></returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}