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
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Data.Repository.TFLoan.Device;


//using Technofair.Model.ViewModel.Loan.Device;

namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface ILnDeviceLoanCollectionService
    {
        Task<Operation> Save(LnDeviceLoanCollection obj);
        Operation Update(LnDeviceLoanCollection obj);
        Operation AddWithNoCommit(LnDeviceLoanCollection obj);
        Task<Operation> AddWithNoCommitAsync(LnDeviceLoanCollection obj);
        List<LnDeviceLoanCollection> GetAll();
        LnDeviceLoanCollection GetById(Int64 id);
        Task AddDeviceLoanCollectionAsync(LnDeviceLoanCollection obj);
        Task AddRangeDeviceLoanCollectionAsync(List<LnDeviceLoanCollection> objList);
        Task<List<LnDeviceLoanCollectionViewModel>> GetLoanCollection(LnDeviceLoanCollectionViewModel obj);
        DeviceLoanInfoViewModel GetDeviceLoanInfo(int lenderId, int loaneeId);
        DeviceLoanInfoViewModel GetDeviceLoanInfoByAppKey(string appKey);
    }
    public class LnDeviceLoanCollectionService : ILnDeviceLoanCollectionService
    {
        private ILnDeviceLoanCollectionRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnDeviceLoanCollectionService(ILnDeviceLoanCollectionRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task AddDeviceLoanCollectionAsync(LnDeviceLoanCollection obj)
        {
            await repository.AddDeviceLoanCollectionAsync(obj);
        }

        public async Task AddRangeDeviceLoanCollectionAsync(List<LnDeviceLoanCollection> objList)
        {
           await repository.AddRangeDeviceLoanCollectionAsync(objList);
        }
        public async Task<Operation> Save(LnDeviceLoanCollection obj)
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
             
        public Operation Update(LnDeviceLoanCollection obj)
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

        public Operation AddWithNoCommit(LnDeviceLoanCollection obj)
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

        public async Task<Operation> AddWithNoCommitAsync(LnDeviceLoanCollection obj)
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

        public List<LnDeviceLoanCollection> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnDeviceLoanCollection GetById(Int64 id)
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

        public async Task<List<LnDeviceLoanCollectionViewModel>> GetLoanCollection(LnDeviceLoanCollectionViewModel obj)
        {
            return await repository.GetLoanCollection(obj);
        }

        public DeviceLoanInfoViewModel GetDeviceLoanInfo(int lenderId, int loaneeId)
        {
            try
            {
                return repository.GetDeviceLoanInfo(lenderId, loaneeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DeviceLoanInfoViewModel GetDeviceLoanInfoByAppKey(string appKey)
        {
            try
            {
                return repository.GetDeviceLoanInfoByAppKey(appKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
