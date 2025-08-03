using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure;

namespace TFSMS.Admin.Data.Infrastructure.TFAdmin
{
    
    public class AdminDatabaseFactory : Disposable, IAdminDatabaseFactory
    {
        private TFAdminContext dataContext;
        public TFAdminContext Get()
        {
            return dataContext ?? (dataContext = new TFAdminContext());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
