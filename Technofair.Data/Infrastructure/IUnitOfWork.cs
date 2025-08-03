using System;

namespace TFSMS.Admin.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        Task<bool> CommitAsync();
        Task< bool> CommitWithTransaction();
    }
}
