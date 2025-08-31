using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Technofair.Data.Repository.TFLoan.Device;

using TFSMS.Admin.Data.Repository.TFAdmin;
using Technofair.Lib.Model;

using TFSMS.Admin.Model.TFLoan.Device;

using TFSMS.Admin.Model.ViewModel.TFLoan;

using Technofair.Utiity.Http;
using TFSMS.Admin.Service.TFLoan.Device;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.ViewModel.Subscription;
using Technofair.Model.ViewModel.TFLoan;


namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnDeviceLenderLoaneePolicyController : ControllerBase
    {
        private ILnDeviceLenderLoaneePolicyService service;
        private ITFACompanyCustomerService serviceCompanyCustomer;
      

        public LnDeviceLenderLoaneePolicyController()
        {
            //New
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnDeviceLenderLoaneePolicyService(new LnDeviceLenderLoaneePolicyRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            serviceCompanyCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            //Old
            //var dbfactory = new DatabaseFactory();
            //service = new LnDeviceLenderLoaneePolicyService(new LnDeviceLenderLoaneePolicyRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<LnDeviceLenderLoaneePolicy> GetAll()
        {
            List<LnDeviceLenderLoaneePolicy> list =  service.GetAll();
            return list;
        }

        //From Admin
        [HttpPost("SaveDeviceLenderLoaneePolicy")]
        public async Task<Operation> SaveDeviceLenderLoaneePolicy([FromBody] LnDeviceLenderLoaneePolicyViewModel obj)
        {
            Operation objOperation = new Operation();

            //LnDeviceLoanDisbursement/SaveLoanDisbursement
            LnDeviceLenderLoaneePolicy objPolicy = new LnDeviceLenderLoaneePolicy();

            var objPolicyExit = service.GetById(obj.Id);
                      

            if (objPolicyExit == null)
            {
                #region Save To SMS Database by SMS Api

                LnDeviceLenderLoaneePolicyViewModel objPayload = new LnDeviceLenderLoaneePolicyViewModel();

                objPayload.Id = obj.Id;
                objPayload.LenderId = obj.LenderId;
                objPayload.LoaneeId = obj.LoaneeId;           
                //objPayload.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
                objPayload.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                objPayload.IsActive = obj.IsActive;
                objPayload.CreatedBy = obj.CreatedBy;
                objPayload.CreatedDate = DateTime.Now;

                var objCompanyCustomer = serviceCompanyCustomer.GetById(obj.LoaneeId);

                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                var url = smsApiBaseUrl + "/api/LnDeviceLenderLoaneePolicy/SaveDeviceLenderLoaneePolicy";

                objOperation = await Request<LnDeviceLenderLoaneePolicyViewModel, Operation>.Post(url, objPayload);
                               

                #endregion
            }


            //Save In Admin Database
            if (objOperation.Success == true)
            {

                if (objPolicyExit == null)
                {
                    objPolicy.Id = obj.Id;
                    objPolicy.LenderId = obj.LenderId;
                    objPolicy.LoaneeId = obj.LoaneeId;
                    //objPolicy.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
                    objPolicy.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                    objPolicy.IsActive = obj.IsActive;
                    objPolicy.CreatedBy = obj.CreatedBy;
                    objPolicy.CreatedDate = DateTime.Now;
                    objOperation = await service.Save(objPolicy);
                    objOperation.Message = "Loan Policy Created Successfully.";
                }
                if (objPolicyExit != null)
                {
                    //objPolicyExit.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
                    objPolicyExit.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                    objPolicyExit.IsActive = obj.IsActive;
                    objPolicyExit.ModifiedBy = obj.ModifiedBy;
                    objPolicyExit.ModifiedDate = DateTime.Now;
                    objOperation = service.Update(objPolicyExit);
                    objOperation.Message = "Loan Policy Updated Successfully.";
                }
            }




            return objOperation;
        }

        //Old: From SMS --ata r lagba ne... remove hobe....
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] LnDeviceLenderLoaneePolicyViewModel obj)
        {
            Operation objOperation = new Operation();
            Operation objAdminOperation = new Operation();
            Operation objSmsOperation = new Operation();

            LnDeviceLenderLoaneePolicy objLnDeviceLenderLoaneePolicy = new LnDeviceLenderLoaneePolicy();

            try
            {

                var objPolicyList = service.GetAll();

                if (objPolicyList.Count() == 0)
                {
                    objLnDeviceLenderLoaneePolicy.Id = obj.Id;
                    objLnDeviceLenderLoaneePolicy.LenderId = obj.LenderId;
                    objLnDeviceLenderLoaneePolicy.LoaneeId = obj.LoaneeId;
                    objLnDeviceLenderLoaneePolicy.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                    objLnDeviceLenderLoaneePolicy.IsActive = obj.IsActive;
                    objLnDeviceLenderLoaneePolicy.CreatedBy = obj.CreatedBy;
                    objLnDeviceLenderLoaneePolicy.CreatedDate = DateTime.Now;
                    objAdminOperation = await service.Save(objLnDeviceLenderLoaneePolicy);
                    objAdminOperation.Message = "Loan Policy Created Successfully.";
                }
                else
                {
                    var objExit = service.GetById(obj.Id);

                    if (objExit != null)
                    {
                        objExit.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                        objExit.IsActive = obj.IsActive;
                        objExit.ModifiedBy = obj.ModifiedBy;
                        objExit.ModifiedDate = DateTime.Now;
                        objAdminOperation = service.Update(objExit);
                        objAdminOperation.Message = "Loan Policy Updated Successfully.";
                    }
                }
            }
            catch(Exception exp)
            {
                objAdminOperation.Success = false;
                objAdminOperation.Message = "Unable to create policy in Admin side";
            }

            try
            {
                if (objAdminOperation.Success == true)
                {
                    var objCompanyCustomer = serviceCompanyCustomer.GetById(obj.LoaneeId);
                    var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;
                    var url = smsApiBaseUrl + "/api/LnDeviceLenderLoaneePolicy/SaveLenderLoaneePolicyToSms";
                    objSmsOperation = await Request<LnDeviceLenderLoaneePolicyViewModel, Operation>.Post(url, obj);
                }
            }
            catch(Exception exp)
            {
                objSmsOperation.Success = false;
                objSmsOperation.Message = "Unable to create policy in Admin side";
            }
            
            if(objAdminOperation.Success == true && objSmsOperation.Success == false)
            {
                objOperation.Success = true;
                objOperation.Message = "Policy Created in admin db successflly and failed in sms db";
            }

            if (objAdminOperation.Success == false && objSmsOperation.Success == false)
            {
                objOperation.Success = false;
                objOperation.Message = "Unable to create policy";
            }

            if (objAdminOperation.Success == true && objSmsOperation.Success == true)
            {
                objOperation.Success = true;
                objOperation.Message = "Policy Created Successfully";
            }

            return objOperation;
        }
        

        [HttpPost("SaveLenderLoaneePolicyToSms")]
        public async Task<Operation> SaveLenderLoaneePolicyToSms(LnDeviceLenderLoaneePolicyViewModel obj)
        {
            Operation objOperation = new Operation();
           //LnDeviceLenderLoaneePolicy objLnDeviceLenderLoaneePolicy = new LnDeviceLenderLoaneePolicy();


            //var objExit = service.GetById(obj.Id);

            //if (objExit == null)
            //{
            //    objLnDeviceLenderLoaneePolicy.Id = obj.Id;
            //    objLnDeviceLenderLoaneePolicy.LenderId = obj.LenderId;
            //    objLnDeviceLenderLoaneePolicy.LoaneeId = obj.LoaneeId;
            //    objLnDeviceLenderLoaneePolicy.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
            //    objLnDeviceLenderLoaneePolicy.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
            //    objLnDeviceLenderLoaneePolicy.IsActive = obj.IsActive;
            //    objLnDeviceLenderLoaneePolicy.CreatedBy = obj.CreatedBy;
            //    objLnDeviceLenderLoaneePolicy.CreatedDate = DateTime.Now;
            //    objOperation = await service.Save(objLnDeviceLenderLoaneePolicy);
            //    objOperation.Message = "Device Lender Loanee Policy Created Successfully.";
            //}
            //if (objExit != null)
            //{
            //    objExit.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
            //    objExit.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
            //    objExit.IsActive = obj.IsActive;
            //    objExit.ModifiedBy = obj.ModifiedBy;
            //    objExit.ModifiedDate = DateTime.Now;
            //    objOperation = service.Update(objExit);
            //    objOperation.Message = "Device Lender Loanee Policy Updated Successfully.";
            //}

            return objOperation;
        }

            [HttpPost("GetDeviceLenderLoaneePolicy")]
        public async Task<List<LnDeviceLenderLoaneePolicyViewModel>> GetDeviceLenderLoaneePolicy()
        {
            List<LnDeviceLenderLoaneePolicyViewModel> list = await service.GetDeviceLenderLoaneePolicy();

            return list;
        }
    }
}
