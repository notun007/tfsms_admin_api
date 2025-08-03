
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Security;
using Technofair.Lib.Model;
using Technofair.Model.Security;
using Technofair.Model.ViewModel.Common;
using Technofair.Model.ViewModel.Security;
using Technofair.Service.Security;

namespace TFSMS.Admin.Controllers.Security
{
    [Route("Security/[Controller]")]
    [ApiController]
    public class MenuPermissionController : ControllerBase
    {

        private ISecMenuPermissionService service;
        public MenuPermissionController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new SecMenuPermissionService(new SecMenuPermissionRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpGet("ModuleMenuByRole")]
        public async Task<List<Modules>> ModuleMenuByRole(int id)
        {

            var headers = Request.Headers;

            foreach (var header in headers)
            {
                var Key = header.Key;
                var value = header.Value;
            }

            return service.ModuleMenuByRole(id, "No");
        }

        [Authorize(Policy = "Authenticated")]
        [HttpGet("HasPermision")]
        public async Task<bool> HasPermision(string linkurl, int roleId)
        {
            var users = await service.HasPermision(linkurl, roleId);
            return users;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetMenuPermissionByUserOrRoleId")]
        public async Task<List<SecMenuPermissionViewModel>> GetMenuPermissionByUserOrRoleId([FromBody] RequestObject objReq)
        {
            List<SecMenuPermissionViewModel> list = service.GetMenuPermissionByUserOrRoleId(objReq.roleId, objReq.userId, objReq.moduleId);
            return list;
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] RequestObject objReq)
        {
            Operation objOperation = new Operation { Success = false };
            if (objReq != null)
            {
                List<SecMenuPermission> list = objReq.list;
                List<SecMenuPermission> listExist = service.GetByModuleAndRoleId(objReq.moduleId, objReq.roleId, objReq.userId);
                if (listExist != null && listExist.Count > 0)
                {
                    if (list != null && list.Count > 0)
                    {
                        foreach (SecMenuPermission objExist in listExist)
                        {
                            SecMenuPermission obj = (from objR in list where objR.SecMenuId == objExist.SecMenuId select objR).FirstOrDefault();
                            if (obj == null || (obj.Read || obj.Add || obj.Edit || obj.Delete || obj.Print))
                            {
                                objOperation = service.Delete(objExist);
                            }
                            else if (obj != null && !(obj.Read || obj.Add || obj.Edit || obj.Delete || obj.Print))
                            {
                                objOperation = service.Delete(objExist);
                            }
                            else if (obj != null)
                            {
                                obj.Id = objExist.Id;
                                if ((obj.Read || obj.Add || obj.Edit || obj.Delete || obj.Print))
                                {
                                    obj.IsActive = true;
                                }
                                else
                                {
                                    obj.IsActive = false;
                                }
                                obj.CreatedBy = objExist.CreatedBy;
                                obj.CreatedDate = objExist.CreatedDate;
                                obj.ModifiedDate = DateTime.Now;
                                objOperation = service.Update(obj);
                            }
                        }
                    }
                    else
                    {
                        foreach (SecMenuPermission obj in listExist)
                        {
                            objOperation = service.Delete(obj);
                        }
                    }
                }

                if (list != null && list.Count > 0)
                {
                    try
                    {
                        foreach (SecMenuPermission obj in list)
                        {
                            obj.SecRoleId = objReq.roleId;
                            obj.SecUserId = objReq.userId;
                            if (listExist != null && listExist.Count > 0)
                            {
                                SecMenuPermission objExist = service.GetByRoleAndMenuId(objReq.roleId, objReq.userId, obj.SecMenuId);
                                if (objExist == null || (obj.Read || obj.Add || obj.Edit || obj.Delete || obj.Print))
                                {
                                    obj.Id = 0;
                                    if ((obj.Read || obj.Add || obj.Edit || obj.Delete || obj.Print))
                                    {
                                        obj.IsActive = true;
                                    }
                                    else
                                    {
                                        obj.IsActive = false;
                                    }
                                    obj.CreatedDate = DateTime.Now;
                                    objOperation = await service.Save(obj);
                                }
                            }
                            else if ((obj.Read || obj.Add || obj.Edit || obj.Delete || obj.Print))
                            {
                                obj.Id = 0;
                                if ((obj.Read || obj.Add || obj.Edit || obj.Delete || obj.Print))
                                {
                                    obj.IsActive = true;
                                }
                                else
                                {
                                    obj.IsActive = false;
                                }
                                obj.CreatedDate = DateTime.Now;
                                objOperation = await service.Save(obj);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return objOperation;
        }

        public class RequestObject
        {
            public int moduleId { get; set; }
            public int? roleId { get; set; }
            public int? userId { get; set; }
            private List<SecMenuPermission> view = null;
            public List<SecMenuPermission> list
            {
                get
                {
                    if (view == null)
                    {
                        view = new List<SecMenuPermission>();

                    }
                    return view;
                }
                set
                {
                    view = value;
                }
            }
        }
    }
}
