using Technofair.Data.Infrastructure;
using Technofair.Model;

namespace TFSMS.Admin.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private TechnofairContext dataContext;
        public TechnofairContext Get()
        {
            return dataContext ?? (dataContext = new TechnofairContext());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
