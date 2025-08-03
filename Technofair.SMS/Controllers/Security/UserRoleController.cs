using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Security;
using Technofair.Model.Security;
using Technofair.Service.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Technofair.Lib.Utilities;
using Technofair.Lib.Model;
using Technofair.Model.ViewModel.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Controllers.Security
{
    [Route("Security/[Controller]")]
    public class UserRoleController : ControllerBase
    {
        //

        private ISecUserRoleService service;
        //private ISecUserService serviceUser;

        public UserRoleController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new SecUserRoleService(new SecUserRoleRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            //serviceUser = new SecUserService(new SecUserRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetUserRoles")]
        public async Task<List<SecUserRoleViewModel>> GetUserRoles(int userId)
        {
            List<SecUserRoleViewModel> list = service.GetSecUserRoles(userId);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpGet("GetByRoleId")]
        public async Task<List<SecUserRole>> GetByRoleId(int roleId)
        {
            List<SecUserRole> list = service.GetByRoleId(roleId);
            return list;
        }

        //[Authorize(Policy = "Authenticated")]
        //[HttpPost("GetUserRolesByCompanyAndRoleId")]
        //public async Task<List<SecUserRoleViewModel>> GetUserRolesByCompanyAndRoleId(int companyId, int roleId)
        //{
        //    List<SecUserRoleViewModel> list = service.GetUserRolesByCompanyAndRoleId(companyId, roleId);
        //    return list;
        //}
        
        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetRoleLessUserByCompanyId")]
        public async Task<List<UserRoleViewModel>> GetRoleLessUserByCompanyId(int companyId)
        {
            List<UserRoleViewModel> list = service.GetRoleLessUserByCompanyId(companyId);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetRoleOrientedUserByCompanyRoleId")]
        public async Task<List<SecUserRoleViewModel>> GetRoleOrientedUserByCompanyRoleId(int companyId, int? roleId)
        {
            List<SecUserRoleViewModel> list = service.GetRoleOrientedUserByCompanyRoleId(companyId, roleId);
            return list;
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] RequestObject objReq)
        {
            Operation objOperation = new Operation { Success = false };
            if (objReq != null)
            {
                int roleId = objReq.roleId;
                List<SecUserRole> list = objReq.list;
                List<SecUserRole> listExist = service.GetByRoleId(roleId);

                //New: 20.05.2024
                foreach (SecUserRole obj in list)
                {
                    SecUserRole objExist = service.GetByRoleAndUserId(obj.SecUserId, obj.SecRoleId);
                                     
                    if (objExist != null)
                    {
                        if (!obj.IsActive)
                        {
                            service.Delete(obj);
                        }
                    }
                    else
                    {
                        if (obj.IsActive)
                        {
                            obj.SecRoleId = obj.SecRoleId;
                            obj.CreatedDate = DateTime.Now;
                            objOperation = await service.Save(obj);
                        }
                    }
                }



                //Old: 20.05.2024
                //if (listExist != null && listExist.Count > 0)
                //{
                //    if (list != null && list.Count > 0)
                //    {
                //        foreach (SecUserRole obj in listExist)
                //        {
                //            SecUserRole? objExist = (from SecUserRole ur in list where ur.SecUserId == obj.SecUserId && ur.SecRoleId == roleId select ur).FirstOrDefault();
                //            if (objExist == null)
                //            {
                //                service.Delete(obj);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        foreach (SecUserRole obj in listExist)
                //        {
                //            service.Delete(obj);
                //        }
                //    }
                //}
                //if (list != null && list.Count > 0)
                //{
                //    foreach (SecUserRole obj in list)
                //    {
                //        // check for single role
                //        List<SecUserRole> listRole = service.GetByUserId(obj.SecUserId);
                //        if (listRole != null && listRole.Count > 0)
                //        {
                //            foreach (SecUserRole objRole in listRole)
                //            {
                //                if (obj.SecRoleId != objRole.SecRoleId)
                //                {
                //                    service.Delete(objRole);
                //                }
                //            }
                //        }
                //        SecUserRole objExist = service.GetByRoleAndUserId(obj.SecUserId, roleId);
                //        if (objExist == null)
                //        {
                //            obj.SecRoleId = roleId;
                //            obj.CreatedDate = DateTime.Now;
                //            objOperation = await service.Save(obj);
                //        }
                //        else if (objExist != null && objExist.Id > 0)
                //        {
                //            objExist.SecRoleId = obj.SecRoleId;
                //            objExist.ModifiedDate = DateTime.Now;
                //            objOperation = service.Update(objExist);
                //        }
                //    }
                //}
                //End


            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Delete")]
        public async Task<Operation> Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            try
            {
                SecUserRole objExist = service.GetById(Id);
                if (objExist != null && objExist.Id > 0)
                {
                    objOperation = service.Delete(objExist);
                    objOperation.Success = true;
                    objOperation.Message = "Deleted Successfully";
                }
                else
                {
                    objOperation.Success = false;
                    objOperation.Message = "No Record Found to Delete";
                }
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Unable to Delete";
            }
            return objOperation;
        }

        public class RequestObject
        {
            public int roleId { get; set; }
            private List<SecUserRole> view = null;
            public List<SecUserRole> list
            {
                get
                {
                    if (view == null)
                    {
                        view = new List<SecUserRole>();
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
