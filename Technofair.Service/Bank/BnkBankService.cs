using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Bank;
using Technofair.Lib.Model;
using Technofair.Model.Bank;


namespace Technofair.Service.Bank
{
    public interface IBnkBankService
    {
        Operation Save(BnkBank obj);
        Operation Update(BnkBank obj);
        Operation Delete(BnkBank obj);
        BnkBank GetById(int Id);
        List<BnkBank> GetAll();
    }

    public class BnkBankService : IBnkBankService
    {
        private IBnkBankRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public BnkBankService(IBnkBankRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public List<BnkBank> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public BnkBank GetById(int Id)
        {
            BnkBank obj = repository.GetById(Id);
            return obj;
        }

        public Operation Save(BnkBank obj)
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

        public Operation Update(BnkBank obj)
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

        public Operation Delete(BnkBank obj)
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


    }
}
