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
using Technofair.Model.TFLoan.Device;
using TFSMS.Admin.Data.Repository.TFLoan.Device;



namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface ILnLoanModelService
    {
        Task<Operation> Save(LnLoanModel obj);
        Operation Update(LnLoanModel obj);
        Task<Operation> AddWithNoCommitAsync(LnLoanModel obj);
        List<LnLoanModel> GetAll();
        LnLoanModel GetById(Int16 id);
        Task<List<LnLoanModel>> GetActiveLoanModel();
    }
    public class LnLoanModelService : ILnLoanModelService
    {
        private ILnLoanModelRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnLoanModelService(ILnLoanModelRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(LnLoanModel obj)
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

        public Operation Update(LnLoanModel obj)
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

        public async Task<Operation> AddWithNoCommitAsync(LnLoanModel obj)
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

        public List<LnLoanModel> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public List<LnLoanModel> GetLoanModel()
        {
            return repository.GetAll().ToList();
        }

        public async Task<List<LnLoanModel>> GetActiveLoanModel()
        {
            return await repository.GetActiveLoanModel();
        }

        public LnLoanModel GetById(Int16 id)
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
