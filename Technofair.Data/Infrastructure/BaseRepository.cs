using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
//using Technofair.Model.Inventory;

namespace TFSMS.Admin.Data.Infrastructure
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        //private readonly ApplicationContext _context;

        //public BaseRepository()
        //{
        //    _context = new ApplicationContext();

        //}

        //public ApplicationContext Context
        //{

        //    get { return _context == null ? new ApplicationContext() : _context; }
        //}


        
        private TechnofairContext dataContext;
        //private readonly IDbSet<T> dbset;
        private readonly DbSet<TEntity> dbset;
        protected BaseRepository(IDatabaseFactory databaseFactory)//IDatabaseFactory databaseFactory
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<TEntity>();
            //dataContext = new TechnofairContext();            
            if (DataContext != null)
            {
                
                //dataContext.Configuration.ProxyCreationEnabled = false;
            }
        }
        //public BaseRepository()
        //{
        //    dataContext = new TechnofairContext();

        //}
        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected TechnofairContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
            //get { return dataContext == null ? new TechnofairContext() : dataContext; }
        }

        public virtual void Add(TEntity entity)
        {
            dbset.Add(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await dbset.AddAsync(entity);
        }
        public virtual async Task AddRangeAsync(List<TEntity> entities)
        {
            await dbset.AddRangeAsync(entities);
        }
        public virtual async Task<int> AddWithCommitAsync(TEntity entity)
        {
            using (var context = new TechnofairContext())
            {
                await context.AddAsync(entity);
                var result = await context.SaveChangesAsync();
                return result;
            }
        }
        public virtual void Update(TEntity entity)
        {
            //dbset.Attach(entity);
            //dataContext.Entry(entity).State = EntityState.Modified;
            using (var context = new TechnofairContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges(); //Must be in using block
            }
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            using (var context = new TechnofairContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                var result = await context.SaveChangesAsync();
                return result;
            }
        }
                
        //New: 16.01.2023
        //public Int64 UpdateEntityAsync(PrdDeviceNumber obj)
        //{
        //    DataContext.PrdDeviceNumbers.Update(obj);
        //    return obj.Id;
        //}

        public virtual void Delete(TEntity entity)
        {
            dbset.Remove(entity);
        }

        public virtual void DeleteAsync(TEntity entity)
        {
            using (var context = new TechnofairContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChangesAsync();
            }
        }

        public virtual void Delete(TEntity entity, int id)
        {
            using (var context = new TechnofairContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = dbset.Where<TEntity>(where).AsEnumerable();
            foreach (TEntity obj in objects)
                dbset.Remove(obj);
        }
        public virtual TEntity GetById(Int16 id)
        {
            return dbset.Find(id);
        }

		//New
		public virtual async Task<TEntity> GetByIdAsync(Int16 id)
		{
			return await dbset.FindAsync(id);
		}

		public virtual TEntity GetById(int id)
        {
            return dbset.Find(id);
        }

		//New
		public async virtual Task<TEntity> GetByIdAsync(int id)
		{
			return await dbset.FindAsync(id);
		}
		public virtual TEntity GetById(long id)
        {
            return dbset.Find(id);
        }

		//New
		public async virtual Task<TEntity> GetByIdAsync(long id)
		{
			return await dbset.FindAsync(id);
		}
		public virtual TEntity GetByName(string name)
        {
            return dbset.Find(name);
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbset.ToList();
        }
		//New
		public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await dbset.ToListAsync();
		}
        //New
		public virtual long Count()
        {
            return dbset.Count();
        }
        //New
		public async virtual Task<long> CountAsync()
		{
			return await dbset.CountAsync();
		}

		public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
		//New
		public async virtual Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
		{
			return await dbset.Where(where).ToListAsync();
		}
		public virtual IEnumerable<TEntity> GetManyWithInclude(Expression<Func<TEntity, bool>> where, string include)
        {
            return dbset.Include(include).Where(where).ToList();
        }

		//New
		public async virtual Task<IEnumerable<TEntity>> GetManyWithIncludeAsync(Expression<Func<TEntity, bool>> where, string include)
		{
			return await dbset.Include(include).Where(where).ToListAsync();
		}

		/// <summary>
		/// Return a paged list of entities
		/// </summary>
		/// <typeparam name="TOrder"></typeparam>
		/// <param name="page">Which page to retrieve</param>
		/// <param name="where">Where clause to apply</param>
		/// <param name="order">Order by to apply</param>
		/// <returns></returns>
		//public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
		//{
		//    var results = dbset.OrderBy(order).Where(where).GetPage(page).ToList();
		//    var total = dbset.Count(where);
		//    return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
		//}

		public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<TEntity>();
        }

        public async Task<DataTable> GetDataTable(string procName, SqlParameter[] param)
        {
            var dataTable = new DataTable();

            using var command = DataContext.Database.GetDbConnection().CreateCommand();
            try
            {
                command.CommandText = procName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(param);

                command.Connection.Open();
                DbDataReader dbDataReader = await command.ExecuteReaderAsync();
                using var result = dbDataReader;

                dataTable.Load(result);
                command.Connection.Close();

            }
            catch (Exception ex)
            {
                command.Connection.Close();

            }
            return dataTable;
        }

        public DataTable GetFromStoredProcedure(string storeProcedure, SqlParameter[] parameters, bool isStoredProcedure = true)
        {
            DataTable dt = new DataTable();
            var sqlString = DataContext.Database.GetDbConnection().ConnectionString;
            var conn = new SqlConnection(sqlString);
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = storeProcedure;
                    if (isStoredProcedure)
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    if (parameters != null)
                    {
                        foreach (var item in parameters)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return dt;

        }
        public virtual int SaveChanges()
        {
            return dataContext.SaveChanges();
        }
        public virtual IDbContextTransaction BeginTransaction()
        {
            return dataContext.Database.BeginTransaction();
        }
        public virtual void Rollback(IDbContextTransaction contextTxn)
        {
            contextTxn.Rollback();
        }
        public virtual void Commit(IDbContextTransaction contextTxn)
        {
            contextTxn.Commit();
        }

    }
}
