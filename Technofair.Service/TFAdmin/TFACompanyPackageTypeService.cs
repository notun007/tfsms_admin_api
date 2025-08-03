
using Technofair.Lib.Model;

using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.TFAdmin;
using Technofair.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Service.TFAdmin
{
    #region Interface
    public interface ITFACompanyPackageTypeService
    {
        Operation Save(TFACompanyPackageType obj);
        Operation Update(TFACompanyPackageType obj);
        Operation Delete(TFACompanyPackageType obj);
        TFACompanyPackageType GetById(int id);
        List<TFACompanyPackageType> GetAll();
        List<TFACompanyPackageType> GetCompanyPackageTypeByAllowPackage(bool allowPackage);
    }
    #endregion


    #region Member
    public class TFACompanyPackageTypeService : ITFACompanyPackageTypeService
    {
        private ITFACompanyPackageTypeRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public TFACompanyPackageTypeService(ITFACompanyPackageTypeRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation Save(TFACompanyPackageType obj)
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

        public Operation Update(TFACompanyPackageType obj)
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
        public Operation Delete(TFACompanyPackageType obj)
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

        public TFACompanyPackageType GetById(int id)
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

        public List<TFACompanyPackageType> GetAll()
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

        public List<TFACompanyPackageType> GetCompanyPackageTypeByAllowPackage(bool allowPackage)
        {
            try
            {
                return repository.GetCompanyPackageTypeByAllowPackage(allowPackage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    #endregion
}
