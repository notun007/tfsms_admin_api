using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Accounts;
using Technofair.Lib.Model;
using Technofair.Model.Accounts;
using Technofair.Model.TFAdmin;
using Technofair.Model.ViewModel.Accounts;
using Technofair.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
    public interface ITFABillGenPermssionService
    {
        Task<Operation> Save(TFABillGenPermssion obj);        
        Operation Update(TFABillGenPermssion obj);      
        Operation Delete(TFABillGenPermssion obj);
        List<TFABillGenPermssion> GetBillGenPermissionExceptItSelf(TFABillGenPermssionViewModel objTFABillGenPermssion);
        List<TFABillGenPermssion> GetBillGenPermission(int id);
        Task<List<TFABillGenPermssionViewModel>> GetBillGenPermission();
        Task<List<TFABillGenPermssionViewModel>> GetOpenBillGenPermission();
        Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedYear();
        Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedMonthByYear(int year);
        TFABillGenPermssion GetById(int Id);
        List<TFABillGenPermssion> GetAll();
        TFABillGenPermssion? GetBillGenPermissionByMonthIdYear(int monthId, int year);
        Task<List<TFABillGenPermssionViewModel>> GetList();
    }
    public class TFABillGenPermssionService : ITFABillGenPermssionService
    {
        private ITFABillGenPermssionRepository repository;       
        private IAdminUnitOfWork _UnitOfWork;

        public TFABillGenPermssionService(ITFABillGenPermssionRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
            var dbfactory = new DatabaseFactory();
        }

        public List<TFABillGenPermssion> GetBillGenPermissionExceptItSelf(TFABillGenPermssionViewModel objTFABillGenPermssion)
        {
            return repository.GetBillGenPermissionExceptItSelf(objTFABillGenPermssion);
        }
        public async Task< Operation> Save(TFABillGenPermssion obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {                
                objOperation.OperationId = repository.AddEntity(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(TFABillGenPermssion obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Update(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Delete(TFABillGenPermssion obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Delete(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public TFABillGenPermssion GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public List<TFABillGenPermssion> GetAll()
        {
            try
            {
                return repository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TFABillGenPermssion> GetBillGenPermission(int id)
        {
            try
            {
                return repository.GetBillGenPermission(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  TFABillGenPermssion? GetBillGenPermissionByMonthIdYear(int monthId, int year)
        {
            return repository.GetBillGenPermissionByMonthIdYear(monthId, year);
        }

        public async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermission()
        {
            return await repository.GetBillGenPermission();
        }
        public async Task<List<TFABillGenPermssionViewModel>> GetOpenBillGenPermission()
        {
            return await repository.GetOpenBillGenPermission();
        }

        public async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedYear()
        {
            return await repository.GetBillGenPermittedYear();
        }

        public async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedMonthByYear(int year)
        {
            return await repository.GetBillGenPermittedMonthByYear(year);
        }

        public async Task<List<TFABillGenPermssionViewModel>> GetList()
        {
            return await repository.GetList();
        }

    }
}
