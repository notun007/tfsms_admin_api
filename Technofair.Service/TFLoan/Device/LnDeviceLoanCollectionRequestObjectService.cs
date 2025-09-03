using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Model.TFLoan.Device;
using TFSMS.Admin.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;

namespace Technofair.Service.TFLoan.Device
{
   
    public interface ILnDeviceLoanCollectionRequestObjectService
    {
        Task<Operation> Save(LnDeviceLoanCollectionRequestObject obj);
        Operation Update(LnDeviceLoanCollectionRequestObject obj);
        Operation AddWithNoCommit(LnDeviceLoanCollectionRequestObject obj);
        Task<Operation> AddWithNoCommitAsync(LnDeviceLoanCollectionRequestObject obj);

        LnDeviceLoanCollectionRequestObject GetById(Int64 id);
     }
    public class LnDeviceLoanCollectionRequestObjectService : ILnDeviceLoanCollectionRequestObjectService
    {
        private ILnDeviceLoanCollectionRequestObjectRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnDeviceLoanCollectionRequestObjectService(ILnDeviceLoanCollectionRequestObjectRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }
                   
        public async Task<Operation> Save(LnDeviceLoanCollectionRequestObject obj)
        {
            Operation objOperation = new Operation { Success = false };

            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = await _UnitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(LnDeviceLoanCollectionRequestObject obj)
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

        public Operation AddWithNoCommit(LnDeviceLoanCollectionRequestObject obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = repository.AddEntity(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public async Task<Operation> AddWithNoCommitAsync(LnDeviceLoanCollectionRequestObject obj)
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

        public List<LnDeviceLoanCollectionRequestObject> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnDeviceLoanCollectionRequestObject GetById(Int64 id)
        {
            try
            {
                return repository.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
