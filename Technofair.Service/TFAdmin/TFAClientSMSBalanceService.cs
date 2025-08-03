
using TFSMS.Admin.Model.ViewModel.Common;
using Technofair.Lib.Model;

using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Service.TFAdmin
{
    #region Interface
    public interface ITFAClientSMSBalanceService
    {
        Operation Save(TFAClientSMSBalance obj);
        Operation Update(TFAClientSMSBalance obj);
        TFAClientSMSBalance GetById(int id);
        //List<TFAClientSMSBalance> GetAll();
        Task<List<TFASMSBalanceViewModel>> GetCustomerDetails();
        List<TFAClientSMSBalance> GetByClientId(int clientId);
        List<TFASMSBalanceViewModel> GetSMSBalanceByClientId(int clientId);
    }
    #endregion


    #region Member
    public class TFAClientSMSBalanceService : ITFAClientSMSBalanceService
    {
        private ITFAClientSMSBalanceRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public TFAClientSMSBalanceService(ITFAClientSMSBalanceRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation Save(TFAClientSMSBalance obj)
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

        public Operation Update(TFAClientSMSBalance obj)
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
        public TFAClientSMSBalance GetById(int id)
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

        //public List<TFAClientSMSBalance> GetAll()
        //{
        //    try
        //    {
        //        return repository.GetAll().ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<List<TFASMSBalanceViewModel>> GetCustomerDetails()
        {
            return await repository.GetCustomerDetails();
        }

        public List<TFAClientSMSBalance> GetByClientId(int clientId)
        {
            try
            {
                return repository.GetMany(w => w.TFACompanyCustomerId == clientId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFASMSBalanceViewModel> GetSMSBalanceByClientId(int clientId)
        {
            try
            {
                return repository.GetSMSBalanceByClientId(clientId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

    #endregion
}
