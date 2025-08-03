using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Technofair.Lib.Model;
using System.Threading.Tasks;
using Technofair.Model.TFAdmin;
using Technofair.Model.ViewModel.Accounts;
using Technofair.Model.ViewModel.TFAdmin;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
    #region Interface
    public interface ITFAClientPackageLogService
    {
        Operation Save(TFAClientPackageLog obj);
        Operation Update(TFAClientPackageLog obj);
        Operation Delete(TFAClientPackageLog obj);
        List<TFAClientPackageLog> GetAll();
        TFAClientPackageLog GetById(int id);
        Task<Operation> AddWithNoCommit(TFAClientPackageLog obj);
    }
    #endregion


    #region Member
    public class TFAClientPackageLogService : ITFAClientPackageLogService
    {
        private ITFAClientPackageLogRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public TFAClientPackageLogService(ITFAClientPackageLogRepository _repository, IaDMINUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        

        

       
        public Operation Save(TFAClientPackageLog obj)
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
        public Operation Update(TFAClientPackageLog obj)
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
        public Operation Delete(TFAClientPackageLog obj)
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

        public List<TFAClientPackageLog> GetAll()
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

        public TFAClientPackageLog GetById(int Id)
        {
            try
            {
                return repository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //New: 22.08.2024
        public async Task<Operation> AddWithNoCommit(TFAClientPackageLog obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
    }

    #endregion
}
