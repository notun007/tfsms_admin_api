using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Common;
using Technofair.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;
using Technofair.Service.Security;
using Technofair.Data.Repository.Security;
using Technofair.Data.Infrastructure.TFAdmin;

namespace Technofair.SMS.Filters
{
    public class ResourcePermissionAttribute : ActionFilterAttribute
    {
        private ISecMenuService service;
        public ResourcePermissionAttribute()
        {
            var dbFactory = new AdminDatabaseFactory();
            service = new SecMenuService(new SecMenuRepository(dbFactory),new AdminUnitOfWork(dbFactory));        
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //string actionName = filterContext.ActionDescriptor.ActionName;
                //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //string tag = controllerName + "/" + actionName;

                //int userId = Convert.ToInt32(filterContext.HttpContext.Session["userId"]);
                //int moduleId = Convert.ToInt32(filterContext.HttpContext.Session["moduleId"]);
                //DataTable dt = service.GetResourcePermissionByUserId(tag, userId, moduleId);
                //if (dt.Rows.Count > 0)
                //{
                //    //filterContext.Controller.ViewData.Add("ReadOnly", dt.Rows[0][0]);
                //    ButtonPermission.ReadOnly = Convert.ToBoolean(dt.Rows[0][0]);
                //    //filterContext.Controller.ViewData.Add("Add", dt.Rows[0][1]);
                //    ButtonPermission.Add = Convert.ToBoolean(dt.Rows[0][1]);
                //    //filterContext.Controller.ViewData.Add("Edit", dt.Rows[0][2]);
                //    ButtonPermission.Edit = Convert.ToBoolean(dt.Rows[0][2]);
                //    //filterContext.Controller.ViewData.Add("Delete", dt.Rows[0][3]);
                //    ButtonPermission.Delete = Convert.ToBoolean(dt.Rows[0][3]);
                //    //filterContext.Controller.ViewData.Add("Print", dt.Rows[0][4]);
                //    ButtonPermission.Print = Convert.ToBoolean(dt.Rows[0][4]);
                //}


            }
            

            
        }
    }
}