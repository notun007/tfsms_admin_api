using Technofair.Data;
//using Technofair.Data.Infrastructure;

namespace TFSMS.Admin.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private TechnofairContext dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected TechnofairContext DataContext
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
