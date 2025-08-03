using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Accounts;
using Technofair.Data.Repository.Common;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Model.ViewModel.Accounts.Reports;
using TFSMS.Admin.Model.ViewModel.Common;
using Technofair.Service.Accounts;
using Technofair.Service.Common;
using Technofair.SMS.Filters;

namespace TFSMS.Admin.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearController : ControllerBase
    {
        //
        // GET: /Common/FinancialYear/
        private ICmnFinancialYearService service;
        //private IAnFOpeningBalanceService serviceOpeningBalance;
        //private AnFReportService serviceReport;
        //private AnFChartOfAccountService serviceChart;
        public FinancialYearController()
        {
            var dbfactory = new DatabaseFactory();
            service = new CmnFinancialYearService(new CmnFinancialYearRepository(dbfactory), new UnitOfWork(dbfactory));
            //serviceOpeningBalance = new AnFOpeningBalanceService(new AnFOpeningBalanceRepository(dbfactory), new UnitOfWork(dbfactory));
            //serviceReport = new AnFReportService(new AnFReportRepository(dbfactory));
            //serviceChart = new AnFChartOfAccountService(new AnFChartOfAccountRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetById")]
        public async Task<CmnFinancialYear> GetById(int Id)
        {
            CmnFinancialYear obj = service.GetById(Id);
            return obj;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<CmnFinancialYear>> GetAll()
        {
            List<CmnFinancialYear> list = service.GetAll();
            if (list != null)
            {
                list = list.OrderByDescending(o => o.OpeningDate).ToList();
            }
            return list;
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] CmnFinancialYear obj)
        {
            Operation objOperation = new Operation { Success = false };
            if (obj.Id == 0)//&& ButtonPermission.Add
            {
                objOperation = service.Save(obj);
            }
            else if (obj.Id > 0)//&& ButtonPermission.Edit
            {
                objOperation = service.Update(obj);
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Delete")]
        public async Task<Operation> Delete(int Id)
        {
            Operation objOperation = new Operation() { Success = false };
            if (Id > 0 && ButtonPermission.Delete)
            {
                CmnFinancialYear obj = service.GetById(Id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }            
            return objOperation;
        }

        //[HttpPost]
        //public async Task<List<ReportAnFTrialBalanceResultViewModel>> GetTrialBalance(int selectedFinancilaYearId, int businessId, int projectId, int companyId)
        //{
        //    CmnFinancialYear obj = service.GetById(selectedFinancilaYearId);

        //    SearchViewModel objReportView = new SearchViewModel();
        //    objReportView.CmnFinancialYearId = selectedFinancilaYearId;
        //    objReportView.CmnCompanyId = companyId;
        //    objReportView.DateFrom = obj.OpeningDate;
        //    objReportView.DateTo = obj.ClosingDate;
        //    //objReportView.CmnProjectId = projectId;
        //    objReportView.CmnBusinessId = businessId;
        //    objReportView.Status = true;
        //    List<ReportAnFTrialBalanceResultViewModel> list = new List<ReportAnFTrialBalanceResultViewModel>();
        //    list = serviceReport.GetTrialBalance(objReportView);
        //    //return Json(list, JsonRequestBehavior.AllowGet);
        //    return list;
        //}

        //public ActionResult GetAllFinantialYearsForView()
        //{
        //    int companyId = Convert.ToInt32(Session["companyId"]);
        //    List<CmnFinancialYearsForView> list = new List<CmnFinancialYearsForView>();
        //    list = service.GetFinancialYearsForView(companyId);
        //    return Json(list, JsonRequestBehavior.AllowGet);

        //}

        //[HttpPost]
        //public async Task<List<CmnFinancialYear>> GetByCompanyId(int companyId)
        //{
        //    List<CmnFinancialYear> list = service.GetByCompanyId(companyId);
        //    list = list.OrderByDescending(o => o.OpeningDate).ToList();
        //    return list;
        //}

        //[HttpPost]
        //public async Task<CmnFinancialYearResultForRange> CheckFinancialYear(int financialYearId)
        //{
        //    CmnFinancialYearResultForRange dts = new CmnFinancialYearResultForRange();
        //    //int financialYearId= Convert.ToInt32(Session["financialYearId"].ToString());

        //    CmnFinancialYear obj = service.GetById(financialYearId);
        //    dts.OpeningDate = obj.OpeningDate.ToString("dd/MM/yyyy");
        //    dts.ClosingDate = obj.ClosingDate.ToString("dd/MM/yyyy");
        //    return dts;
        //}

        //[HttpPost]
        //public async Task<Operation> SaveFinancialYearClosing(int selectedFinancilaYearId, List<ReportAnFTrialBalanceResultViewModel> list, int projectId, int comId, int yearId)
        //{
        //    Operation objOperation = new Operation { Success = false };
        //    CmnFinancialYear obj = service.GetById(selectedFinancilaYearId);

        //    if (list != null && list.Count > 0)
        //    {
        //        obj.YearClosingStatus = true;
        //        service.Update(obj);

        //        foreach (ReportAnFTrialBalanceResultViewModel objView in list)
        //        {
        //            AnFChartOfAccount objChart = serviceChart.GetByCode(objView.Code.ToString(), comId);
        //            if (objChart != null && objChart.Id > 0)
        //            {
        //                AnFOpeningBalance objOpening = serviceOpeningBalance.GetByCOAandProjectId(yearId, objChart.Id, projectId);
        //                if (objOpening == null)
        //                {
        //                    objOpening = new AnFOpeningBalance();
        //                    objOpening.CmnCompanyId = comId;
        //                    objOpening.CmnProjectId = projectId;
        //                    objOpening.CmnFinancialYearId = yearId;
        //                    objOpening.AnFChartOfAccountId = objChart.Id;
        //                    objOpening.Debit = objView.CumDebit;
        //                    objOpening.Credit = objView.CumCredit;
        //                    await serviceOpeningBalance.Save(objOpening);
        //                }
        //                else if (objOpening != null && objOpening.Id > 0)
        //                {
        //                    objOpening.Debit = objView.CumDebit; ;
        //                    objOpening.Credit = objView.CumCredit;
        //                    objOperation = await serviceOpeningBalance.Update(objOpening);
        //                }
        //            }
        //        }
        //    }
        //    return objOperation;
        //}

    }
}
