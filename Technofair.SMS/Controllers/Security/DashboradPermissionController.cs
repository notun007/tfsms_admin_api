using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Security;
using TFSMS.Admin.Model.Security;
using Technofair.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Technofair.Lib.Utilities;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.ViewModel.Security;


namespace TFSMS.Admin.Controllers.Security
{
    //public class DashboardPermissionController : Controller
    //{
    //    //
    //    // GET: /Security/DashboardPermission/
    //    private ISecDashboardPermissionService _dashboardPermission;

    //    //[AuthorizeUser]
    //    //[ResourcePermissionAttribute]
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }
    //    public DashboardPermissionController()
    //    {
    //        var datafactory = new DatabaseFactory();
    //        _dashboardPermission = new SecDashboardPermissionService(new SecDashboardPermissionRepository(datafactory), new UnitOfWork(datafactory));

    //    }
    //    [HttpGet]
    //    public ActionResult GetDashboardPermissionByRoleId(int roleId, int moduleId)
    //    {
    //        var dt = _dashboardPermission.GetDashboardPermissionByRoleId(roleId, moduleId);
    //        List<SecDashboardPermissionViewModel> list = dt.DataTableToList<SecDashboardPermissionViewModel>();
    //        return Json(list, JsonRequestBehavior.AllowGet);

    //    }

    //    public ActionResult GetPermittedDashBoard(int companyId, int roleId,int moduleId)
    //    {
    //        //int companyId = Convert.ToInt32(Session["companyId"]), roleId = 2, moduleId = Convert.ToInt32(Session["moduleId"]);
    //        var list = _dashboardPermission.GetPermittedDashBoard(companyId,roleId,moduleId);
    //        return Json(list, JsonRequestBehavior.AllowGet);
    //    }


    //    [HttpPost]
    //    public ActionResult Save(List<SecDashboardPermission> secDashboardPermissionsList, int roleId, int moduleId)
    //    {
    //        int userId = Convert.ToInt32(Session["userId"]);
    //        Operation objOperation = new Operation { Success = false };
    //        if (ModelState.IsValid)
    //        {
    //            int del= _dashboardPermission.Delete(roleId, moduleId);
               
    //            objOperation = _dashboardPermission.Save(secDashboardPermissionsList,userId);
    //        }

    //        return Json(objOperation, JsonRequestBehavior.DenyGet);
    //    }
    //}
}
