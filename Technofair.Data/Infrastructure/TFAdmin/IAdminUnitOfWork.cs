using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Data.Infrastructure.TFAdmin
{   
    public interface IAdminUnitOfWork
    {
        void Commit();
        Task<bool> CommitAsync();
        Task<bool> CommitWithTransaction();
    }
}
