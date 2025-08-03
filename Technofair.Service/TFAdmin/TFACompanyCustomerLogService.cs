
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.TFAdmin;


namespace TFSMS.Admin.Service.TFAdmin
{
 
    public interface ITFACompanyCustomerLogService
    {
        Task<Operation> Save(TFACompanyCustomerLog obj);
        Task<Operation> AddWithNoCommit(TFACompanyCustomerLog obj);
        //Operation Update(TFACompanyCustomerLog obj);
        //Operation Delete(TFACompanyCustomerLog obj);
        //TFACompanyCustomerLog GetById(int Id);
        //List<TFACompanyCustomerLog> GetAll();
    }

    public class TFACompanyCustomerLogService : ITFACompanyCustomerLogService
    {

        private ITFACompanyCustomerLogRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        public TFACompanyCustomerLogService(ITFACompanyCustomerLogRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(TFACompanyCustomerLog obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = await _UnitOfWork.CommitAsync();
                return objOperation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Operation> AddWithNoCommit(TFACompanyCustomerLog obj)
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
}
