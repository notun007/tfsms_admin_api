using Microsoft.AspNetCore.Authorization;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Common;
using Technofair.Service.Common;
using System;
using System.Data;
using System.Web;
using Technofair.Service.Security;
using Technofair.Data.Repository.Security;
using Technofair.Data.Infrastructure.TFAdmin;

namespace Technofair.SMS.Filters
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        private PermissionEnum permission;
        private bool ispermissionset=false;
        public PermissionEnum Permission {
            get {
                return permission;
            } 
            set { 
                ispermissionset=true;
                permission = value;
            } }

        public string ResourceTag { get; set; }

        private ISecMenuService menuService;

        public AuthorizeUserAttribute()
        {
            var dbFactory = new AdminDatabaseFactory();
            menuService = new SecMenuService(new SecMenuRepository(dbFactory), new AdminUnitOfWork(dbFactory));
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    bool isAuthorized = base.AuthorizeCore(httpContext);
            
        //    int userId = Convert.ToInt32(httpContext.Session["userId"]);
        //    int moduleId = Convert.ToInt32(httpContext.Session["moduleId"]);
        //    if (!isAuthorized || userId == 0 || moduleId==0)
        //    {
        //        return false;
        //    }
        //    //if (string.IsNullOrEmpty(ResourceTag))
        //    //{
        //        string controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
        //        string actionName = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
        //        ResourceTag = controllerName + "/" + actionName;
        //    //}
        //    DataTable dt = _secResourceService.GetResourcePermissionByUserId(ResourceTag, userId, moduleId);
        //    if (dt !=null && dt.Rows.Count > 0)
        //    {
        //        if (ispermissionset)
        //        {
        //            if (!(bool)dt.Rows[0][Permission.ToString()])
        //            {
                        
        //                return false;
        //            }
        //        }                
        //    }
        //    else
        //    {
        //        isAuthorized = false;
        //    }
        //    return isAuthorized;
        //}
    }
}