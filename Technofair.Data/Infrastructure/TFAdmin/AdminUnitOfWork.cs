using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Data.Infrastructure.TFAdmin
{
   
    public class AdminUnitOfWork : IUnitOfWork
    {
        private readonly IAdminDatabaseFactory databaseFactory;
        private TFAdminContext dataContext;

        public AdminUnitOfWork(IAdminDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected TFAdminContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                await DataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> CommitWithTransaction()
        {
            using (var trns = DataContext.Database.BeginTransaction())
            {
                try
                {
                    await DataContext.SaveChangesAsync();
                    trns.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    throw ex;
                }
            }
        }

    }


}
