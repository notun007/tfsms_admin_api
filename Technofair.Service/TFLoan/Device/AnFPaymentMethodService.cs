using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;

//using Technofair.Data.Repository.Accounts;
using Technofair.Lib.Model;
using Technofair.Model.Accounts;
using Technofair.Model.ViewModel.Accounts;
using Technofair.Model.ViewModel.Security;
using Technofair.Model.ViewModel.TFAdmin;
using Technofair.Utiity.Security;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.Accounts;

namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface IAnFPaymentMethodService
    {
        Operation Save(AnFPaymentMethod obj);
        Operation Update(AnFPaymentMethod obj);
        Operation Delete(AnFPaymentMethod obj);
        AnFPaymentMethod GetById(Int16 Id);
        Task<AnFPaymentMethod> GetByIdAsync(Int16 Id);
        List<AnFPaymentMethod> GetAll();
        Task<List<AnFPaymentMethodViewModel>> GetList();
        AnFPaymentMethod? GetPaymentMethodByName(string name);

        Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId);
        Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId, Int16 anFPaymentChannelId);
        Task<Operation> Authenticate(LoginViewModel user);

    }
    public class AnFPaymentMethodService : IAnFPaymentMethodService
    {
        private IAnFPaymentMethodRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public AnFPaymentMethodService(IAnFPaymentMethodRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public List<AnFPaymentMethod> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public AnFPaymentMethod GetById(Int16 Id)
        {
            AnFPaymentMethod obj = repository.GetById(Id);
            return obj;
        }
        public async Task<AnFPaymentMethod> GetByIdAsync(Int16 Id)
        {
            AnFPaymentMethod obj = new AnFPaymentMethod();
            try
            {
                obj = await repository.GetByIdAsync(Id);
            }
            catch(Exception exp)
            {

            }
            return obj;
        }

        public async Task<List<AnFPaymentMethodViewModel>> GetList()
        {
            return await repository.GetList();
        }
        public Operation Save(AnFPaymentMethod obj)
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
        public Operation Update(AnFPaymentMethod obj)
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
        public Operation Delete(AnFPaymentMethod obj)
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

        public AnFPaymentMethod? GetPaymentMethodByName(string name)
        {
            return repository.GetPaymentMethodByName(name);
        }

        public async Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId)
        {
            return await repository.GetPaymentMethodByUserId(userId);
        }

        public async Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId, Int16 anFPaymentChannelId)
        {
            return await repository.GetPaymentMethodByUserId(userId, anFPaymentChannelId);
        }

        public async Task<Operation> Authenticate(LoginViewModel user)
        {
            Operation objOperation = new Operation();
            objOperation.Success = false;
            objOperation.Message = "Failed to Authenticate";

            try
            {
                var objUser = await repository.GetPaymentMethodByUserId(user.UserId);

                if (objUser != null)
                {
                    if (objUser.Password == user.Password
                      //&& objUser.AnFPaymentMethodId == user.AnFPaymentMethodId
                      )
                    {
                        objOperation.Success = true;
                        objOperation.Message = "Authenticated";
                    }
                    else
                    {
                        objOperation.Success = false;
                        objOperation.Message = "invalid Credential";
                    }
                }
                else
                {
                    objOperation.Success = false;
                    objOperation.Message = "Failed to Authenticate";
                }

            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Failed to Authenticate";
            }
            return objOperation;
        }
    }
}
