using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Model.TFLoan.Device;
using Technofair.Model.ViewModel.TFLoan;
using TFSMS.Admin.Data.Infrastructure;

namespace Technofair.Service.TFLoan.Device
{
    public interface ILnRechargeLoanCollectionService
    {
        Task<Operation> Save(LnRechargeLoanCollection obj);
        Operation Update(LnRechargeLoanCollection obj);
        Operation AddWithNoCommit(LnRechargeLoanCollection obj);
        Task<Operation> AddWithNoCommitAsync(LnRechargeLoanCollection obj);
        List<LnRechargeLoanCollection> GetAll();
        LnRechargeLoanCollection GetById(Int64 id);
       RechargeLoanCollectionSummaryViewModel GetRechargeLoanCollectionByLoanNo(string loanNo);

    }
    public class LnRechargeLoanCollectionService : ILnRechargeLoanCollectionService
    {
        private ILnRechargeLoanCollectionRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnRechargeLoanCollectionService(ILnRechargeLoanCollectionRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(LnRechargeLoanCollection obj)
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

        public Operation Update(LnRechargeLoanCollection obj)
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

        public Operation AddWithNoCommit(LnRechargeLoanCollection obj)
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

        public async Task<Operation> AddWithNoCommitAsync(LnRechargeLoanCollection obj)
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

        public List<LnRechargeLoanCollection> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnRechargeLoanCollection GetById(Int64 id)
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

        public RechargeLoanCollectionSummaryViewModel GetRechargeLoanCollectionByLoanNo(string loanNo)
        {
            return repository.GetRechargeLoanCollectionByLoanNo(loanNo);
        }

    }
}
