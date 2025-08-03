using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;

//using Technofair.Data.Repository.Loan.Device;
using Technofair.Lib.Model;

using Technofair.Model.TFLoan.Device;
using Technofair.Model.ViewModel.Subscription;
using Technofair.Model.ViewModel.TFLoan;
using Technofair.Service.TFAdmin;
using Technofair.Service.TFLoan.Device;
using Technofair.Utiity.Http;
//using Technofair.Service.Loan.Device;

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnDeviceLenderLoaneePolicyController : ControllerBase
    {
        private ILnDeviceLenderLoaneePolicyService service;
        private ITFACompanyCustomerService serviceCompanyCustomer;
        //private IWebHostEnvironment _hostingEnvironment;

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
            LnDeviceLenderLoaneePolicy objLnDeviceLenderLoaneePolicy = new LnDeviceLenderLoaneePolicy();


            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                objLnDeviceLenderLoaneePolicy.Id = obj.Id;
                objLnDeviceLenderLoaneePolicy.LenderId = obj.LenderId;
                objLnDeviceLenderLoaneePolicy.LoaneeId = obj.LoaneeId;
                objLnDeviceLenderLoaneePolicy.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
                objLnDeviceLenderLoaneePolicy.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                objLnDeviceLenderLoaneePolicy.IsActive = obj.IsActive;
                objLnDeviceLenderLoaneePolicy.CreatedBy = obj.CreatedBy;
                objLnDeviceLenderLoaneePolicy.CreatedDate = DateTime.Now;
                objOperation = await service.Save(objLnDeviceLenderLoaneePolicy);
                objOperation.Message = "Device Lender Loanee Policy Created Successfully.";
            }
            if (objExit != null)
            {
                objExit.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
                objExit.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                objExit.IsActive = obj.IsActive;
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExit);
                objOperation.Message = "Device Lender Loanee Policy Updated Successfully.";
            }
            return objOperation;
        }

        //Old: From SMS
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] LnDeviceLenderLoaneePolicyViewModel obj)
        {
            Operation objOperation = new Operation();
            LnDeviceLenderLoaneePolicy objLnDeviceLenderLoaneePolicy = new LnDeviceLenderLoaneePolicy();


            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                objLnDeviceLenderLoaneePolicy.Id = obj.Id;
                objLnDeviceLenderLoaneePolicy.LenderId = obj.LenderId;
                objLnDeviceLenderLoaneePolicy.LoaneeId = obj.LoaneeId;
                objLnDeviceLenderLoaneePolicy.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
                objLnDeviceLenderLoaneePolicy.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                objLnDeviceLenderLoaneePolicy.IsActive = obj.IsActive;
                objLnDeviceLenderLoaneePolicy.CreatedBy = obj.CreatedBy;
                objLnDeviceLenderLoaneePolicy.CreatedDate = DateTime.Now;
                objOperation = await service.Save(objLnDeviceLenderLoaneePolicy);
                objOperation.Message = "Device Lender Loanee Policy Created Successfully.";
            }
            if (objExit != null)
            {
                objExit.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
                objExit.PerRechargeInstallmentAmount = obj.PerRechargeInstallmentAmount;
                objExit.IsActive = obj.IsActive;
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExit);
                objOperation.Message = "Device Lender Loanee Policy Updated Successfully.";
            }


            var objCompanyCustomer = serviceCompanyCustomer.GetById(obj.LoaneeId);

            var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

            var url = smsApiBaseUrl + "/api/LnDeviceLenderLoaneePolicy/SaveLenderLoaneePolicyToSms";

            var objSubscriptionInfo = await Request<LnDeviceLenderLoaneePolicyViewModel, Operation>.Post(url, obj);



            //await serviceCompanyCustomer.GetCompanyCustomerByAppKey(appKey);

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
