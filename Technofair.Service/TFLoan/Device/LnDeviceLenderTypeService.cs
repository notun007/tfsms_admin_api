using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;

//using Technofair.Data.Repository.Loan.Device;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Data.Repository.TFLoan.Device;
//using Technofair.Model.Loan.Device;

namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface ILnDeviceLenderTypeService
    {
        Task<Operation> Save(LnDeviceLenderType obj);
        Operation Update(LnDeviceLenderType obj);
        Task<Operation> AddWithNoCommitAsync(LnDeviceLenderType obj);
        List<LnDeviceLenderType> GetAll();
        LnDeviceLenderType GetById(Int16 id);
        Task<List<LnDeviceLenderType>> GetActiveDeviceLenderType();
    }
    public class LnDeviceLenderTypeService : ILnDeviceLenderTypeService
    {
        private ILnDeviceLenderTypeRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnDeviceLenderTypeService(ILnDeviceLenderTypeRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(LnDeviceLenderType obj)
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

        public Operation Update(LnDeviceLenderType obj)
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

        public async Task<Operation> AddWithNoCommitAsync(LnDeviceLenderType obj)
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

        public List<LnDeviceLenderType> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnDeviceLenderType GetById(Int16 id)
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
        public async Task<List<LnDeviceLenderType>> GetActiveDeviceLenderType()
        {
            return await repository.GetActiveDeviceLenderType();
        }
    }
}
