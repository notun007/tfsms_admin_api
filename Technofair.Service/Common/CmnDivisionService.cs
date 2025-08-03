using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Infrastructure;

namespace TFSMS.Admin.Service.Common
{

    #region Interface
    public interface ICmnDivisionService
    {
        Operation Save(CmnDivision obj);
        Operation Update(CmnDivision obj);
        Operation Delete(CmnDivision obj);
        List<CmnDivision> GetAll();
        CmnDivision GetById(int Id);
    }
    #endregion


    #region Member
    public class CmnDivisionService : ICmnDivisionService
    {
        private ICmnDivisionRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        public CmnDivisionService(ICmnDivisionRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation Save(CmnDivision obj)
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

        public Operation Update(CmnDivision obj)
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
        public Operation Delete(CmnDivision obj)
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

       
        public List<CmnDivision> GetAll()
        {
            return repository.GetAll().ToList();
        }
        public CmnDivision GetById(int Id)
        {
            return repository.GetById(Id);
        }
        
    }

    #endregion
}
