using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Model.TFLoan.Device;
using Technofair.Model.ViewModel.TFLoan;
using Technofair.Service.TFLoan.Device;
using Technofair.Utiity.Http;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Service.TFAdmin;

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnRechargeLoanCollectionController : ControllerBase
    {
        private ILnRechargeLoanCollectionService service;
        private ITFACompanyCustomerService serviceCompanyCustomer;

        public LnRechargeLoanCollectionController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new LnRechargeLoanCollectionService(new LnRechargeLoanCollectionRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceCompanyCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<LnRechargeLoanCollection> GetAll()
        {
            List<LnRechargeLoanCollection> list = service.GetAll();
            return list;
        }

        
        [HttpGet("GetRechargeLoanCollectionByLoanNo")]
        public async Task<RechargeLoanCollectionSummaryViewModel> GetRechargeLoanCollectionByLoanNo(string appKey, string loanNo)
        {
            var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByAppKey(appKey);

            var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

            var url = smsApiBaseUrl + "/api/LnRechargeLoanCollection/GetRechargeLoanCollectionByLoanNo?loanNo=" + loanNo;

            var list = await Request<RechargeLoanCollectionSummaryViewModel, RechargeLoanCollectionSummaryViewModel>.GetObject(url);

            return list;
        }
    }
}
