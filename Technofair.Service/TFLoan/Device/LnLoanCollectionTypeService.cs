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
//using Technofair.Model.Loan.Device;

namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface ILnLoanCollectionTypeService
    {
        Task<Operation> Save(LnLoanCollectionType obj);
        Operation Update(LnLoanCollectionType obj);
        Task<Operation> AddWithNoCommitAsync(LnLoanCollectionType obj);
        List<LnLoanCollectionType> GetAll();
        LnLoanCollectionType GetById(Int16 id);
        Task<List<LnLoanCollectionType>> GetManualCollectionTypes();
    }
    public class LnLoanCollectionTypeService : ILnLoanCollectionTypeService
    {
        private ILnLoanCollectionTypeRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnLoanCollectionTypeService(ILnLoanCollectionTypeRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(LnLoanCollectionType obj)
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

        public Operation Update(LnLoanCollectionType obj)
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

        public async Task<Operation> AddWithNoCommitAsync(LnLoanCollectionType obj)
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

        public List<LnLoanCollectionType> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnLoanCollectionType GetById(Int16 id)
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
        public async Task<List<LnLoanCollectionType>> GetManualCollectionTypes()
        {
            return await repository.GetManualCollectionTypes();
        }
    }
}
