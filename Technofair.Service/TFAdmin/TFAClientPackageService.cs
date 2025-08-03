using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
    #region Interface
    public interface ITFAClientPackageService
    {
        Operation Save(TFAClientPackage obj);
        Operation Update(TFAClientPackage obj);
        Operation Delete(TFAClientPackage obj);
        List<TFAClientPackage> GetAll();
        TFAClientPackage GetById(int id);
        List<TFAClientPackage> GetActivePackageByClientId(int clientId);
        List<TFAClientPackageViewModel> GetAllClientPackage();
        TFAClientPackage GetActivePackageByClientIdPackageId(int clientId, int TFACompanyPackageTypeId, int? packageId);
        TFAClientPackage GetClientPackageByCustomerId(int TFACompanyCustomerId);
        List<TFAClientPackage> GetActiveClientPackageByCustomerId(int? TFACompanyCustomerId);
        List<TFAClientPackage> GetCompanyPackageExceptItSelf(TFAClientPackageViewModel objTFAClientPackage);
        //Task<List<CompanyCustomerWithClientPackageViewModel>> GetActiveCompanyCustomerWithClientPackage(int monthId, int year);
        Task<Operation> AddWithNoCommit(TFAClientPackage obj);
        Operation UpdateWithNoCommit(TFAClientPackage obj);
    }
    #endregion


    #region Member
    public class TFAClientPackageService : ITFAClientPackageService
    {
        private ITFAClientPackageRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public TFAClientPackageService(ITFAClientPackageRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        //public List<AnFClientPackage> GetByStudentAndTutorId(int studentId, int batchId, long profileId)
        //{
        //    try
        //    {
        //        return repository.GetByStudentAndTutorId(studentId,batchId, profileId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<TFAClientPackage> GetActivePackageByClientId(int clientId)
        {
            try
            {
                return repository.GetMany(w => w.TFACompanyCustomerId == clientId && w.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFAClientPackage GetActivePackageByClientIdPackageId(int clientId,int TFACompanyPackageTypeId, int? packageId)
        {
            try
            {
                return repository.GetMany(w => w.TFACompanyCustomerId == clientId && w.TFACompanyPackageTypeId == TFACompanyPackageTypeId && w.TFACompanyPackageId == packageId && w.IsActive == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFAClientPackageViewModel> GetAllClientPackage()
        {
            return repository.GetAllClientPackage();
        }
        public Operation Save(TFAClientPackage obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = repository.AddEntity(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Update(TFAClientPackage obj)
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
        public Operation Delete(TFAClientPackage obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Delete(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public List<TFAClientPackage> GetAll()
        {
            try
            {
                return repository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFAClientPackage GetById(int Id)
        {
            try
            {
                return repository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       public TFAClientPackage GetClientPackageByCustomerId(int TFACompanyCustomerId)
        {
            return repository.GetClientPackageByCustomerId(TFACompanyCustomerId);
        }

      public List<TFAClientPackage> GetActiveClientPackageByCustomerId(int? companyCustomerId)
        {
            return repository.GetActiveClientPackageByCustomerId(companyCustomerId);
        }
       public List<TFAClientPackage> GetCompanyPackageExceptItSelf(TFAClientPackageViewModel objTFAClientPackage)
        {
            return repository.GetCompanyPackageExceptItSelf(objTFAClientPackage);
        }

        //New: 22.08.2024
        public async Task<Operation> AddWithNoCommit(TFAClientPackage obj)
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

        public Operation UpdateWithNoCommit(TFAClientPackage obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.UpdateEntity(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }


        //public async Task<List<CompanyCustomerWithClientPackageViewModel>> GetActiveCompanyCustomerWithClientPackage(int monthId, int year)
        //  {
        //      return await repository.GetActiveCompanyCustomerWithClientPackage(monthId, year);
        //  }

        //public AnFPackageViewModel GetPayablePackageByStudentId(int studentId, long profileId)
        //{
        //    try
        //    {
        //        return repository.GetPayablePackageByStudentId(studentId, profileId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public AnFPackageViewModel GetRenewPackageByStudentId(int studentId, int batchId, long profileId)
        //{
        //    try
        //    {
        //        return repository.GetRenewPackageByStudentId(studentId,batchId, profileId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }

    #endregion
}
