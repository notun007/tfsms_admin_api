
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Model.ViewModel.TFLoan;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;


namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface ILnDeviceLenderLoaneePolicyService
    {
        Task<Operation> Save(LnDeviceLenderLoaneePolicy obj);
        Operation Update(LnDeviceLenderLoaneePolicy obj);
        Task<Operation> AddWithNoCommitAsync(LnDeviceLenderLoaneePolicy obj);
        List<LnDeviceLenderLoaneePolicy> GetAll();
        LnDeviceLenderLoaneePolicy GetById(Int16 id);
        Task<List<LnDeviceLenderLoaneePolicyViewModel>> GetDeviceLenderLoaneePolicy();
    }
    public class LnDeviceLenderLoaneePolicyService : ILnDeviceLenderLoaneePolicyService
    {
        private ILnDeviceLenderLoaneePolicyRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnDeviceLenderLoaneePolicyService(ILnDeviceLenderLoaneePolicyRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(LnDeviceLenderLoaneePolicy obj)
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

        public Operation Update(LnDeviceLenderLoaneePolicy obj)
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

        public async Task<Operation> AddWithNoCommitAsync(LnDeviceLenderLoaneePolicy obj)
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

        public List<LnDeviceLenderLoaneePolicy> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnDeviceLenderLoaneePolicy GetById(Int16 id)
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
        public async Task<List<LnDeviceLenderLoaneePolicyViewModel>> GetDeviceLenderLoaneePolicy()
        {
            List<LnDeviceLenderLoaneePolicyViewModel> list = new List<LnDeviceLenderLoaneePolicyViewModel>();
            try
            {
                 list = await repository.GetDeviceLenderLoaneePolicy();
            }
            catch(Exception exp)
            {
            }

            return list;
        }
    }
}
