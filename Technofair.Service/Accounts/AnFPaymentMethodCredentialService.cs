using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Repository.Accounts;
using Technofair.Lib.Model;
using Technofair.Model.Accounts;
using Technofair.Model.ViewModel.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Model.Utility;
using TFSMS.Admin.Model.ViewModel.Security;

namespace Technofair.Service.Accounts
{
    public interface IAnFPaymentMethodCredentialService
    {
        Operation Save(AnFPaymentMethodCredential obj);
        Operation Delete(AnFPaymentMethodCredential obj);
        AnFPaymentMethodCredential GetById(Int16 Id);
        Operation Update(AnFPaymentMethodCredential obj);
        AnFPaymentMethodCredential GetByPaymentMethodId(Int16 paymentMethodId);
        //Task<List<AnFPaymentMethodCredentialViewModel>> GetPaymentMethod();
       // AnFPaymentMethodCredential? GetByAnFPaymentMethodId(int AnFPaymentMethodId);
        ///AnFPaymentMethodCredential GetByUserId(string userId);
        //Task<AnFPaymentMethodCredential> GetPaymentMethodCredentialByUserId(string userId);
        //Task<AnFPaymentMethodCredential> GetPayBillCredentialByUserId(string userId);
        
        //Task<List<AnFPaymentMethodCredential>> GetPaymentMethodCredentialByCompanies(List<Company> Companies, Int16 anfPaymentMethodId);
        //Task<Operation> Authenticate(LoginViewModel user);
        //AnFPaymentMethodCredential? GetPaymentMethodByCompanyIdPaymentMethod(int? companyId, int paymentMethodId);
    }
    public class AnFPaymentMethodCredentialService : IAnFPaymentMethodCredentialService
    {
        private IAnFPaymentMethodCredentialRepository repository;
        private IUnitOfWork _UnitOfWork;
        public AnFPaymentMethodCredentialService(IAnFPaymentMethodCredentialRepository _repository, IUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }
        //public async Task<List<AnFPaymentMethodCredentialViewModel>> GetPaymentMethod()
        //{
        //    return await repository.GetPaymentMethod();
        //}

        public Operation Save(AnFPaymentMethodCredential obj)
        {
            Operation objOperation = new Operation { Success = false };

            Int16 Id = repository.AddEntity(obj);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }
        public Operation Update(AnFPaymentMethodCredential obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            repository.Update(obj);

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
        public Operation Delete(AnFPaymentMethodCredential obj)
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
        public AnFPaymentMethodCredential GetById(Int16 Id)
        {
            return repository.GetById(Id);
            //AnFPaymentMethodDetail obj = repository.GetById(Id);
            //return obj;
        }
        public AnFPaymentMethodCredential GetByPaymentMethodId(Int16 paymentMethodId)
        {
            AnFPaymentMethodCredential obj = repository.GetMany(p => p.AnFPaymentMethodId == paymentMethodId).FirstOrDefault();
            return obj;
        }
        //public AnFPaymentMethodCredential? GetByAnFPaymentMethodId(int AnFPaymentMethodId)
        //{
        //    AnFPaymentMethodCredential? paymentMethodDetail = repository.GetByAnFPaymentMethodId(AnFPaymentMethodId);
        //    return paymentMethodDetail;
        //}

        //public AnFPaymentMethodCredential GetByUserId(string userId)
        //{
        //    return repository.GetByUserId(userId);
        //}

        //public async Task<AnFPaymentMethodCredential> GetPaymentMethodCredentialByUserId(string userId)
        //{
        //    return await repository.GetPaymentMethodCredentialByUserId(userId);
        //}

        //public async Task<AnFPaymentMethodCredential> GetPayBillCredentialByUserId(string userId)
        //{
        //    return await repository.GetPayBillCredentialByUserId(userId);
        //}
        //public async Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId, Int16 AnFPaymentChannelId)
        //{
        //    return await repository.GetPaymentMethodByUserId(userId, AnFPaymentChannelId);
        //}
        //public async Task<List<AnFPaymentMethodCredential>> GetPaymentMethodCredentialByCompanies(List<Company> Companies, Int16 anfPaymentMethodId)
        //{
        //    return await repository.GetPaymentMethodCredentialByCompanies(Companies, anfPaymentMethodId);
        //}
        //public async Task<Operation> Authenticate(LoginViewModel user)
        //{
        //    Operation objOperation = new Operation();
        //    objOperation.Success = false;
        //    objOperation.Message = "Failed to Authenticate";

        //    try
        //    {
        //        var objUser = await repository.GetPaymentMethodCredentialByUserId(user.UserId);

        //        if (objUser != null)
        //        {
        //            if (AES.GetPlainText(objUser.AuthorizationCode) == user.Password
        //              //&& objUser.AnFPaymentMethodId == user.AnFPaymentMethodId
        //              )
        //            {
        //                objOperation.Success = true;
        //                objOperation.Message = "Authenticated";
        //            }
        //            else
        //            {
        //                objOperation.Success = false;
        //                objOperation.Message = "invalid Credential";
        //            }
        //        }
        //        else
        //        {
        //            objOperation.Success = false;
        //            objOperation.Message = "Failed to Authenticate";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        objOperation.Success = false;
        //        objOperation.Message = "Failed to Authenticate";
        //    }
        //    return objOperation;
        //}

        //public AnFPaymentMethodCredential? GetPaymentMethodByCompanyIdPaymentMethod(int? companyId, int paymentMethodId)
        //{
        //    return repository.GetPaymentMethodByCompanyIdPaymentMethod(companyId, paymentMethodId);
        //}
    
    }
}
