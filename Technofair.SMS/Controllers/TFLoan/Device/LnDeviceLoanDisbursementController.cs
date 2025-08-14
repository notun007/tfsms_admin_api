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

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnDeviceLoanDisbursementController : ControllerBase
    {
        private ILnDeviceLoanDisbursementService service;
        private IWebHostEnvironment _hostingEnvironment;

        public LnDeviceLoanDisbursementController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnDeviceLoanDisbursementService(new LnDeviceLoanDisbursementRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            

            //var dbfactory = new DatabaseFactory();
            //service = new LnDeviceLoanDisbursementService(new LnDeviceLoanDisbursementRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<LnDeviceLoanDisbursement> GetAll()
        {
            List<LnDeviceLoanDisbursement> list = service.GetAll();
            return list;
        }

        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] LnDeviceLoanDisbursementViewModel obj)
        {
            Operation objOperation = new Operation();
            LnDeviceLoanDisbursement objLnDeviceLoanDisbursement = new LnDeviceLoanDisbursement();

            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {               
                objLnDeviceLoanDisbursement.Id = obj.Id;
                objLnDeviceLoanDisbursement.LoanNo = service.NextLoanNo();
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
                objLnDeviceLoanDisbursement.MonthlyInstallment = obj.MonthlyInstallment;

                objLnDeviceLoanDisbursement.Remarks = obj.Remarks;

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
                objLnDeviceLoanDisbursement.MonthlyInstallment = obj.MonthlyInstallment;
                objExit.Remarks = obj.Remarks;
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExit);
                objOperation.Message = "Device Loan Disbursement Updated Successfully.";
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
