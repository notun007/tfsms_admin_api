
using Technofair.Lib.Model;

using TFSMS.Admin.Model.HRM;
using TFSMS.Admin.Data.Repository.HRM;
using Technofair.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.HRM
{
    public interface IHrmFileCategoryService
    {
        Operation Save(HrmFileCategory obj);
        List<HrmFileCategory> GetAll();
        Operation Delete(HrmFileCategory obj);
        HrmFileCategory GetById(int Id);
        Operation Update(HrmFileCategory obj);
    }
    public class HrmFileCategoryService : IHrmFileCategoryService
    {
        private IHrmFileCategoryRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        public HrmFileCategoryService(IHrmFileCategoryRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
            var dbfactory = new AdminDatabaseFactory();
        }
        public HrmFileCategory GetById(int Id)
        {
            HrmFileCategory obj = repository.GetById(Id);
            return obj;
        }
        public List<HrmFileCategory> GetAll()
        {

            return repository.GetAll();
        }
        public Operation Save(HrmFileCategory obj)
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

        public Operation Update(HrmFileCategory obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            repository.Update(obj);

            try
            {
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Delete(HrmFileCategory obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            repository.Delete(obj);

            try
            {
                _UnitOfWork.Commit();
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
