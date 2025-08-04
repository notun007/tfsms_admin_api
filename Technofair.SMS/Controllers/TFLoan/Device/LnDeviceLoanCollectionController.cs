using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Technofair.Lib.Model;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Common;
using TFSMS.Admin.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.Utility;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Service.Common;
using TFSMS.Admin.Service.TFLoan.Device;
//using TFSMS.Admin.Data.Infrastructure.TFAdmin;

//using TFSMS.Admin.Model.Utility;
//using TFSMS.Admin.Model.ViewModel.TFLoan;
//using TFSMS.Admin.Service.TFLoan.Device;


namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnDeviceLoanCollectionController : ControllerBase
    {        
        private ILnDeviceLoanCollectionService service;
        private ICmnCompanyService serviceCompany;

        private IWebHostEnvironment _hostingEnvironment;

        public LnDeviceLoanCollectionController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnDeviceLoanCollectionService(new LnDeviceLoanCollectionRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceCompany = new CmnCompanyService(new CmnCompanyRepository(dbfactory), new AdminUnitOfWork(dbfactory));

        }

        [HttpPost("GetAll")]
        public List<LnDeviceLoanCollection> GetAll()
        {
            List<LnDeviceLoanCollection> list = service.GetAll();
            return list;
        }

        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] LnDeviceLoanCollectionViewModel obj)
        {
            Operation objOperation = new Operation();
            LnDeviceLoanCollection objDeviceLoanCollection = new LnDeviceLoanCollection();
                        
            //Farida: 27052025
            RevenueSubscription objRevSubscription = new RevenueSubscription();

            objRevSubscription.DistributorCompanyId = obj.LenderId;
            objRevSubscription.MSOCompanyId = obj.LoaneeId;

            //objRevSubscription = serviceCompany.SetLoanCompany(objRevSubscription);

            var objLoanInfo = service.GetDeviceLoanInfo(Convert.ToInt32(objRevSubscription.DistributorCompanyId), Convert.ToInt32(objRevSubscription.MSOCompanyId));

            if (objLoanInfo.IsPaid == true || objLoanInfo.LoanBalance <= 0)
            {
                objOperation.Success = false;
                objOperation.Message = "All the loan is paid";
                return (objOperation);
            }

            if (obj.Amount > objLoanInfo.LoanBalance)
            {   
                objOperation.Success = false;
                objOperation.Message = "Collection amount exceeds loan balance";
                return (objOperation);
            }

            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                
                objDeviceLoanCollection.Id = obj.Id;
                objDeviceLoanCollection.ScpSubscriberInvoiceDetailId = null;
                objDeviceLoanCollection.LnLoanCollectionTypeId = obj.LnLoanCollectionTypeId;
                objDeviceLoanCollection.AnFPaymentMethodId = obj.AnFPaymentMethodId;
                objDeviceLoanCollection.LenderId = obj.LenderId;
                objDeviceLoanCollection.LoaneeId = obj.LoaneeId;
                objDeviceLoanCollection.Amount = obj.Amount;
                objDeviceLoanCollection.Remarks = obj.Remarks;
                objDeviceLoanCollection.CollectionDate = obj.CollectionDate;
                objDeviceLoanCollection.IsCancel = obj.IsCancel;
                objDeviceLoanCollection.CancelBy = obj.CancelBy;
                objDeviceLoanCollection.CancelDate = obj.CancelDate;

                objDeviceLoanCollection.CreatedBy = obj.CreatedBy;
                objDeviceLoanCollection.CreatedDate = DateTime.Now;
                objOperation = await service.Save(objDeviceLoanCollection);
                //await service.AddDeviceLoanCollectionAsync(objDeviceLoanCollection);
                objOperation.Message = "Device Loan Collection Created Successfully.";
            }

            //Do not allow update, it's financial trx
            //if (objExit != null)
            //{
            //    objExit.LnLoanCollectionTypeId = obj.LnLoanCollectionTypeId;
            //    objExit.AnFPaymentMethodId = obj.AnFPaymentMethodId;
            //    objExit.LenderId = obj.LenderId;
            //    objExit.LoaneeId = obj.LoaneeId;
            //    objExit.Amount = obj.Amount;
            //    objExit.Remarks = obj.Remarks;
            //    objExit.CollectionDate = obj.CollectionDate;
            //    objExit.ModifiedBy = obj.ModifiedBy;
            //    objExit.ModifiedDate = DateTime.Now;
            //    objOperation = service.Update(objExit);
            //    objOperation.Message = "Device Loan Collection Updated Successfully.";
            //}

            return objOperation;
        }

        [Authorize]
        [HttpPost("GetLoanCollection")]
        public List<LnDeviceLoanCollectionViewModel> GetLoanCollection(int lenderId, int loaneeId)
        {
            List<LnDeviceLoanCollectionViewModel> list = service.GetLoanCollection(lenderId, loaneeId);
            return list;
        }

        [HttpPost("GetDeviceLoanInfo")]
        public DeviceLoanInfoViewModel GetDeviceLoanInfo(int lenderId, int loaneeId)
        {
            var loanInfo = service.GetDeviceLoanInfo(lenderId, loaneeId);
            return loanInfo;
        }

        [HttpGet("GetDeviceLoanInfoByAppKey")]
        public DeviceLoanInfoViewModel GetDeviceLoanInfoByAppKey(string appKey)
        {
            var loanInfo = service.GetDeviceLoanInfoByAppKey(appKey);
            return loanInfo;
        }

    }
}
