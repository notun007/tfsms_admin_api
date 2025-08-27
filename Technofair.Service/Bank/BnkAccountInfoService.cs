using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Bank;
using Technofair.Lib.Model;
using Technofair.Model.Bank;
using Technofair.Model.ViewModel.Bank;

namespace Technofair.Service.Bank
{

    #region Interface
    public interface IBnkAccountInfoService
    {
        Operation Save(BnkAccountInfo obj);
        Operation Update(BnkAccountInfo obj);
        Operation Delete(BnkAccountInfo obj);
        //List<BnkAccountInfo> GetByCompanyId(int companyId);
        BnkAccountInfo GetById(long Id);
       // List<BnkAccountInfoViewModel> BankAccountInfoByCompanyId(int companyId);
    }
    #endregion


    #region Member
    public class BnkAccountInfoService : IBnkAccountInfoService
    {
        private IBnkAccountInfoRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        public BnkAccountInfoService(IBnkAccountInfoRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation Save(BnkAccountInfo obj)
        {
            Operation objOperation = new Operation { Success = false };            
           
            long Id = repository.AddEntity(obj);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }

            return objOperation;
        }

        public Operation Update(BnkAccountInfo obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };

            
            try
            {
                repository.Update(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }
        public Operation Delete(BnkAccountInfo obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            repository.Delete(obj);
            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        //public List<BnkAccountInfo> GetByCompanyId(int companyId)
        //{
        //    return repository.GetMany(a => a.CmnCompanyId == companyId).ToList(); ;
        //}

        //public List<BnkAccountInfoViewModel> BankAccountInfoByCompanyId(int companyId)
        //{
        //    try
        //    {
        //        return repository.BankAccountInfoByCompanyId(companyId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public BnkAccountInfo GetById(long Id)
        {
            return repository.GetById(Id);
            
        }

       
    }

    #endregion
}
