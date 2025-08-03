using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Data.Infrastructure.TFAdmin
{   
    public interface IAdminDatabaseFactory : IDisposable
    {
        TFAdminContext Get();
    }
}
