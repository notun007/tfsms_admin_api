using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Numerics;

namespace TFSMS.Admin.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        Task<EntityEntry<T>> AddAsync(T entity);
        void Update(T entity);
        Task<int> UpdateAsync(T entity);
        void Delete(T entity);
        void DeleteAsync(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(Int16 id);
		//New
		Task<T> GetByIdAsync(Int16 id);
		T GetById(int id);
		//New
		Task<T> GetByIdAsync(int id);
		T GetById(long id);
		//New
		Task<T> GetByIdAsync(long id);
		T GetByName(string name);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
		//New
		Task<IEnumerable<T>> GetAllAsync();
        //New
		long Count();
        //New
		Task<long> CountAsync();
		IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
		//New
		Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where);
		IEnumerable<T> GetManyWithInclude(Expression<Func<T, bool>> where,string include);
		//New
		Task<IEnumerable<T>> GetManyWithIncludeAsync(Expression<Func<T, bool>> where, string include);
		DataTable GetFromStoredProcedure(string storeProcedure, SqlParameter[] parameters, bool isStoredProcedure = true);

        int SaveChanges();
        IDbContextTransaction BeginTransaction();
        void Commit(IDbContextTransaction contextTxn);
        void Rollback(IDbContextTransaction contextTxn);
    }
}
