
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;

using TFSMS.Admin.Model.TFAdmin;

using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
    #region Interface
    public interface ITFACompanyPackageService
    {
        Operation Save(TFACompanyPackage obj);
        Operation Update(TFACompanyPackage obj);
        Operation Delete(TFACompanyPackage obj);
        TFACompanyPackage GetById(int id);
        List<TFACompanyPackage> GetAll();
        List<TFACompanyPackageViewModel> GetCurrentPackage();
        List<TFACompanyPackageViewModel> GetAllCompanyPackage();
        List<TFACompanyPackageViewModel> GetCompanyPackageByPackageType(int anFCompanyPackageTypeId);
        TFACompanyPackage? GetTFACompanyPackageByPackageTypeIdMinMaxSub(int TFACompanyPackageTypeId, int minSub, int maxSub);
        List<TFACompanyPackage> GetCompanyPackageExceptItSelf(TFACompanyPackageViewModel objTFACompanyPackage);
    }
    #endregion


    #region Member
    public class TFACompanyPackageService : ITFACompanyPackageService
    {
        private ITFACompanyPackageRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public TFACompanyPackageService(ITFACompanyPackageRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation Save(TFACompanyPackage obj)
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

        public Operation Update(TFACompanyPackage obj)
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
        public Operation Delete(TFACompanyPackage obj)
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

        public TFACompanyPackage GetById(int id)
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

        public List<TFACompanyPackage> GetAll()
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

        public List<TFACompanyPackageViewModel> GetCurrentPackage()
        {
            try
            {
                //DateTime dt = DateTime.Now.Date;
                //return repository.GetMany(w => dt >= w.StartDate.Date && dt <= w.EndDate.Date).ToList();
                return repository.GetCurrentPackage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFACompanyPackageViewModel> GetAllCompanyPackage()
        {
            return repository.GetAllCompanyPackage();
        }
        public List<TFACompanyPackageViewModel> GetCompanyPackageByPackageType(int anFCompanyPackageTypeId)
        {
            return repository.GetCompanyPackageByPackageType(anFCompanyPackageTypeId);
        }
        public TFACompanyPackage? GetTFACompanyPackageByPackageTypeIdMinMaxSub(int TFACompanyPackageTypeId, int minSub, int maxSub)
        {
            return repository.GetTFACompanyPackageByPackageTypeIdMinMaxSub(TFACompanyPackageTypeId, minSub, maxSub);
        }
        public List<TFACompanyPackage> GetCompanyPackageExceptItSelf(TFACompanyPackageViewModel objTFACompanyPackage)
        {
           return repository.GetCompanyPackageExceptItSelf(objTFACompanyPackage);
        }



    }

    #endregion
}
