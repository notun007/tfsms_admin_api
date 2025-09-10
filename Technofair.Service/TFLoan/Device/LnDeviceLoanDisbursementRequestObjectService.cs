using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Model.TFLoan.Device;
using TFSMS.Admin.Model.TFLoan.Device;

namespace Technofair.Service.TFLoan.Device
{
    public interface ILnDeviceLoanDisbursementRequestObjectService
    {
        Task<Operation> Save(LnDeviceLoanDisbursementRequestObject obj);
        Operation Update(LnDeviceLoanDisbursementRequestObject obj);
        Task<Operation> AddWithNoCommitAsync(LnDeviceLoanDisbursementRequestObject obj);
        List<LnDeviceLoanDisbursementRequestObject> GetAll();
        LnDeviceLoanDisbursementRequestObject GetById(Int64 id);
    }
    public class LnDeviceLoanDisbursementRequestObjectService: ILnDeviceLoanDisbursementRequestObjectService
    {
        private ILnDeviceLoanDisbursementRequestObjectRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnDeviceLoanDisbursementRequestObjectService(ILnDeviceLoanDisbursementRequestObjectRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(LnDeviceLoanDisbursementRequestObject obj)
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
        public Operation Update(LnDeviceLoanDisbursementRequestObject obj)
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
        public async Task<Operation> AddWithNoCommitAsync(LnDeviceLoanDisbursementRequestObject obj)
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

        public List<LnDeviceLoanDisbursementRequestObject> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnDeviceLoanDisbursementRequestObject GetById(Int64 id)
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
