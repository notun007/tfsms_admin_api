using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;

//using Technofair.Data.Repository.Loan.Device;
using Technofair.Lib.Model;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Service.TFLoan.Device;
using TFSMS.Admin.Data.Repository.TFLoan.Device;
using Technofair.Utiity.Http;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnDeviceLoanDisbursementController : ControllerBase
    {
        private ILnDeviceLoanDisbursementService service;
        private ITFACompanyCustomerService serviceCompanyCustomer;
        private IWebHostEnvironment _hostingEnvironment;

        public LnDeviceLoanDisbursementController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnDeviceLoanDisbursementService(new LnDeviceLoanDisbursementRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceCompanyCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            //var dbfactory = new DatabaseFactory();
            //service = new LnDeviceLoanDisbursementService(new LnDeviceLoanDisbursementRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<LnDeviceLoanDisbursement> GetAll()
        {
            List<LnDeviceLoanDisbursement> list = service.GetAll();
            return list;
        }

        //Asad-2
        [HttpPost("GetLoanDisbursementDdlByLoaneeId")]
        public async Task<List<DropDownViewModel>> GetLoanDisbursementDdlByLoaneeId(int loaneeId)
        {
           var loans = await service.GetLoanDisbursementDdlByLoaneeId(loaneeId);
            return loans;
        }
      

        //Asad
        [HttpPost("GetLoanDisbursementByLoaneeId")]
        public async Task<List<LnDeviceLoanDisbursement>> GetLoanDisbursementByLoaneeId(int loaneeId)
        {
            return await service.GetLoanDisbursementByLoaneeId(loaneeId);
        }

        //Farida
        [HttpPost("GetLoanNoByLoaneeId")]
        public async Task<List<string>> GetLoanNoByLoaneeId(int loaneeId)
        {
            return await service.GetLoanNoByLoaneeId(loaneeId);
        }

        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] LnDeviceLoanDisbursementViewModel obj)
        {
            Operation objOperation = new Operation();



            //LnDeviceLoanDisbursement/SaveLoanDisbursement
            LnDeviceLoanDisbursementViewModel objPolicy = new LnDeviceLoanDisbursementViewModel();

            var objDisbursementExit = service.GetById(obj.Id);

            LnDeviceLoanDisbursementViewModel objPayload = new LnDeviceLoanDisbursementViewModel();

            if (objDisbursementExit == null)
            {
                #region Save To SMS Database by SMS Api

                

                objPayload.Id = obj.Id;
                objPayload.LoanNo = service.NextLoanNo();
                objPayload.LenderId = obj.LenderId;
                objPayload.LoaneeId = obj.LoaneeId;
                objPayload.NumberOfDevice = obj.NumberOfDevice;
                objPayload.Rate = obj.Rate;

                objPayload.TotalAmount = obj.TotalAmount;
                objPayload.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
                objPayload.DueAmountPerDevice = obj.DueAmountPerDevice;
                objPayload.DownPaymentAmount = obj.DownPaymentAmount;
                objPayload.LoanAmount = obj.LoanAmount;
                objPayload.LnTenureId = obj.LnTenureId;
                objPayload.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;

                objPayload.Remarks = obj.Remarks;
                objPayload.InstallmentStartDate = obj.InstallmentStartDate;
                objPayload.IsClosed = false;
                objPayload.CreatedBy = obj.CreatedBy;
                objPayload.CreatedDate = DateTime.Now;

                var objCompanyCustomer = serviceCompanyCustomer.GetById(obj.LoaneeId);

                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                var url = smsApiBaseUrl + "/api/LnDeviceLoanDisbursement/SaveLoanDisbursement";

                objOperation = await Request<LnDeviceLoanDisbursementViewModel, Operation>.Post(url, objPayload);


                #endregion
            }


            LnDeviceLoanDisbursement objLnDeviceLoanDisbursement = new LnDeviceLoanDisbursement();

            var objExit = service.GetById(obj.Id);

            if (objOperation.Success == true)
            {
                if (objExit == null)
                {
                    objLnDeviceLoanDisbursement.Id = obj.Id;
                    objLnDeviceLoanDisbursement.LoanNo = objPayload.LoanNo;
                    objLnDeviceLoanDisbursement.LenderId = obj.LenderId;
                    objLnDeviceLoanDisbursement.LoaneeId = obj.LoaneeId;
                    objLnDeviceLoanDisbursement.NumberOfDevice = obj.NumberOfDevice;
                    objLnDeviceLoanDisbursement.Rate = obj.Rate;

                    objLnDeviceLoanDisbursement.TotalAmount = obj.TotalAmount;
                    objLnDeviceLoanDisbursement.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
                    objLnDeviceLoanDisbursement.DueAmountPerDevice = obj.DueAmountPerDevice;
                    objLnDeviceLoanDisbursement.DownPaymentAmount = obj.DownPaymentAmount;
                    objLnDeviceLoanDisbursement.LoanAmount = obj.LoanAmount;
                    objLnDeviceLoanDisbursement.LnTenureId = obj.LnTenureId;
                    objLnDeviceLoanDisbursement.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;

                    objLnDeviceLoanDisbursement.Remarks = obj.Remarks;
                    objLnDeviceLoanDisbursement.InstallmentStartDate = obj.InstallmentStartDate;
                    objLnDeviceLoanDisbursement.IsClosed = false;

                    objLnDeviceLoanDisbursement.CreatedBy = obj.CreatedBy;
                    objLnDeviceLoanDisbursement.CreatedDate = DateTime.Now;    
                    objOperation = await service.Save(objLnDeviceLoanDisbursement);
                    objOperation.Message = "Device Loan Disbursement Created Successfully.";
                }
                if (objExit != null)
                {
                    objExit.LenderId = obj.LenderId;
                    objExit.LoaneeId = obj.LoaneeId;
                    objExit.NumberOfDevice = obj.NumberOfDevice;
                    objExit.Rate = obj.Rate;

                    objLnDeviceLoanDisbursement.TotalAmount = obj.TotalAmount;
                    objLnDeviceLoanDisbursement.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
                    objLnDeviceLoanDisbursement.DueAmountPerDevice = obj.DueAmountPerDevice;
                    objLnDeviceLoanDisbursement.DownPaymentAmount = obj.DownPaymentAmount;
                    objLnDeviceLoanDisbursement.LoanAmount = obj.LoanAmount;
                    objLnDeviceLoanDisbursement.LnTenureId = obj.LnTenureId;
                    objLnDeviceLoanDisbursement.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;
                    objExit.Remarks = obj.Remarks;
                    objExit.InstallmentStartDate = obj.InstallmentStartDate;
                    objExit.ModifiedBy = obj.ModifiedBy;
                    objExit.ModifiedDate = DateTime.Now;
                    objOperation = service.Update(objExit);
                    objOperation.Message = "Device Loan Disbursement Updated Successfully.";
                }
            }
            return objOperation;
        }

        [HttpPost("GetDeviceLoanDisbursement")]
        public Task<List<LnDeviceLoanDisbursementViewModel>> GetDeviceLoanDisbursement()
        {
            return service.GetDeviceLoanDisbursement();
        }
    }
}
