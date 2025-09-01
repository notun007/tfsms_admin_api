
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;

using TFSMS.Admin.Model.TFAdmin;

using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
    public interface ITFACompanyCustomerService
    {
        
        Task<Operation> Save(TFACompanyCustomer obj);
        Task<Operation> AddWithNoCommit(TFACompanyCustomer obj);
        Operation Update(TFACompanyCustomer obj);
        Task<Operation> UpdateWithNoCommit(TFACompanyCustomer obj);
        Operation Delete(TFACompanyCustomer obj);
        TFACompanyCustomer GetById(int Id);
        List<TFACompanyCustomer> GetAll();
        TFACompanyCustomer GetByEmailID(string email);
        TFACompanyCustomer GetClientByDomain(string domain);
        TFACompanyCustomer GetByCode(string code);
        Task<TFACompanyCustomer> GetCompanyCustomerByAppKey(string appKey);
        Task<TFACompanyCustomer> GetCompanyCustomerByLoaneeCode(string loaneeCode);
        string GetLastCode();
        List<CompanyCustomerWithClientPackageViewModel> GetActiveCompanyCustomerWithClientPackage(int monthId, int year);
    }

    public class TFACompanyCustomerService : ITFACompanyCustomerService
    {
        private ITFACompanyCustomerRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        public TFACompanyCustomerService(ITFACompanyCustomerRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<TFACompanyCustomer> GetCompanyCustomerByAppKey(string appKey)
        {
            return await repository.GetCompanyCustomerByAppKey(appKey);
        }

        public async Task<TFACompanyCustomer> GetCompanyCustomerByLoaneeCode(string loaneeCode)
        {
            return await repository.GetCompanyCustomerByLoaneeCode(loaneeCode);
        }

        public string GetLastCode()
        {
            return repository.GetLastCode();
        }

        public async Task<Operation> Save(TFACompanyCustomer obj)
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

        public async Task<Operation> AddWithNoCommit(TFACompanyCustomer obj)
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

        public Operation Update(TFACompanyCustomer obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Update(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
                return objOperation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Operation> UpdateWithNoCommit(TFACompanyCustomer obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.UpdateAsync(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Delete(TFACompanyCustomer obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Delete(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
                return objOperation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFACompanyCustomer GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public List<TFACompanyCustomer> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public TFACompanyCustomer GetByEmailID(string email)
        {
            return repository.GetMany(w => w.Email.ToUpper() == email.Trim().ToUpper()).FirstOrDefault();
        }
        public TFACompanyCustomer GetClientByDomain(string domain)
        {
            return repository.GetMany(w => w.Web.ToUpper() == domain.Trim().ToUpper()).FirstOrDefault();
        }
        public TFACompanyCustomer GetByCode(string code)
        {
            return repository.GetMany(w => w.Code == code).FirstOrDefault();
        }

        public List<CompanyCustomerWithClientPackageViewModel> GetActiveCompanyCustomerWithClientPackage(int monthId, int year)
        {
            return repository.GetActiveCompanyCustomerWithClientPackage(monthId, year);
        }

    }

}
