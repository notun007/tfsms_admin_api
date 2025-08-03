using Technofair.Lib.Utilities;
using Technofair.Model.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Technofair.Lib;
using Technofair.Model.ViewModel.Common;
using Technofair.Model.ViewModel.Security;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.Security
{
    #region Interface
    public interface ISecMenuPermissionRepository : IRepository<SecMenuPermission>
    {
        Task<int> AddEntityAsync(SecMenuPermission obj);
        int AddEntity(SecMenuPermission obj);
        int DeleteSecRolePermission(int? roleId, int? userId, int moduleId);
        DataTable GetMenuPermissionByUserOrRoleId(int? roleId, int? userId, int moduleId);
        List<SecMenuPermission> GetByModuleAndRoleId(int moduleId, int? roleId, int? userId);
        DataTable GetSecMenuButtonPermission(string menuName, int userId, int moduleId);

        //Task<List<SecMenuViewModel>> GetRolesBaseMenu();        
        Task<bool> HasPermision(string linkurl, int roleId);
        Task<List<Modules>> ModuleMenuByRole(int roleId);
        List<Modules> ModuleMenuByRole(int roleId, string withEventPerm); //New
        //Task<SecMenuPermission> Save(SecMenuPermission obj);
    }

    #endregion
    public class SecMenuPermissionRepository : Infrastructure.TFAdmin.AdminBaseRepository<SecMenuPermission>, ISecMenuPermissionRepository
    {

        public SecMenuPermissionRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(SecMenuPermission obj)
        {
            int Id = 1;
            SecMenuPermission last = DataContext.SecMenuPermissions.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<int> AddEntityAsync(SecMenuPermission obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                SecMenuPermission last = DataContext.SecMenuPermissions.OrderByDescending(x => x.Id).FirstOrDefault();
                if (last != null)
                {
                    Id = last.Id + 1;
                }
                obj.Id = Id;
            }
            else
            {
                Id = obj.Id;
            }
            await base.AddAsync(obj);
            return Id;
        }


        //public async Task<SecMenuPermission> Save(SecMenuPermission obj)
        //{
        //    using (var trns = DataContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            int Id = 1;
        //            if (obj.Id == 0)
        //            {
        //                SecMenuPermission last = DataContext.SecMenuPermissions.OrderByDescending(x => x.Id).FirstOrDefault();
        //                if (last != null)
        //                {
        //                    Id = last.Id + 1;
        //                }
        //                obj.Id = Id;
        //                DataContext.SecMenuPermissions.Add(obj);
        //                await DataContext.SaveChangesAsync();
        //                trns.Commit();
        //            }
        //            return obj;
        //        }
        //        catch (Exception ex)
        //        {
        //            trns.Rollback();
        //            throw ex;
        //        }
        //    };
        //}


        public List<SecMenuPermission> GetByModuleAndRoleId(int moduleId, int? roleId, int? userId)
        {
            List<SecMenuPermission> list = (from rp in DataContext.SecMenuPermissions
                                            join r in DataContext.SecMenus on rp.SecMenuId equals r.Id
                                            where (r.SecModuleId == moduleId
                                            && (rp.SecRoleId == roleId || roleId == null)
                                            && (rp.SecUserId == userId || userId == null)
                                            )
                                            select rp).ToList();

            return list;
        }
        public int DeleteSecRolePermission(int? roleId, int? userId, int moduleId)
        {
            int ret = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SecRoleId", roleId);
                parameters[1] = new SqlParameter("@SecUserId", userId);
                parameters[2] = new SqlParameter("@SecModuleId", moduleId);
                ret = Helper.ExecuteNonQuery(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecMenuPermission.DeleteSecMenuPermissionByUserOrRoleId, parameters);
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMenuPermissionByUserOrRoleId(int? roleId, int? userId, int moduleId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@SecRoleId", roleId);
            paramsToStore[1] = new SqlParameter("@SecUserId", userId);
            paramsToStore[2] = new SqlParameter("@SecModuleId", moduleId);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecMenuPermission.GetSecMenuPermissionsByRoleId, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSecMenuButtonPermission(string menuName, int userId, int moduleId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@SecUserId", userId);
            paramsToStore[1] = new SqlParameter("@MenuName", menuName);
            paramsToStore[2] = new SqlParameter("@SecModuleId", moduleId);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecMenuPermission.GetSecMenuButtonPermission, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SecMenuViewModel>> GetRolesBaseMenu()
        {
            var menusList = from r in DataContext.SecMenuPermissions
                            join m in DataContext.SecMenus on r.SecMenuId equals m.Id
                            join role in DataContext.SecRoles on r.SecRoleId equals role.Id
                            select new SecMenuViewModel { Id = r.Id, Link = m.Link, Title = m.Title, RoleName = role.Name, SecMenuId = r.SecMenuId, SecRoleId = r.SecRoleId, IsActive = r.IsActive };

            return await menusList.ToListAsync();
        }



        public async Task<bool> HasPermision(string linkurl, int roleId)
        {
            var haspermision = await (from mp in DataContext.SecMenuPermissions
                                          //join userRole in DataContext.SecUserRoles on mp.SecRoleId equals userRole.Id
                                      join role in DataContext.SecRoles on mp.SecRoleId equals role.Id
                                      join m in DataContext.SecMenus.Where(x => x.IsActive == true && x.Link == linkurl) on mp.SecMenuId equals m.Id
                                      where (mp.SecRoleId == roleId && mp.IsActive == true)
                                      select new SecMenuViewModel { Link = m.Link, Title = m.Title, RoleName = role.Name, Icon = m.Icon, SecMenuId = m.ParentMenuId, ParentSerialNo = m.ParentSerialNo, Id = m.Id, ChildSerialNo = m.ChildSerialNo }).FirstOrDefaultAsync();

            if (haspermision != null) { return true; } else return false;
        }

        public async Task<List<Modules>> ModuleMenuByRole(int roleId)
        {
            
            
            try
            {
                var moduleList = new List<Modules>();
                var parentList = new List<UserMenu>();
                var childList = new List<ChildMenu>();


                var modules = await DataContext.SecModules.Where(x => x.IsActive == true).OrderBy(x => x.SerialNo).ToListAsync();
                foreach (var item in modules)
                {
                    var modulParentMenu = await DataContext.SecMenus.Where(x => x.IsParent == true && x.SecModuleId == item.Id).ToListAsync();
                    if (modulParentMenu.Count > 0)
                    {
                        foreach (var parents in modulParentMenu)
                        {
                            var childMenus = await (from m in DataContext.SecMenus
                                                    join mp in DataContext.SecMenuPermissions on m.Id equals mp.SecMenuId
                                                    join r in DataContext.SecUserRoles on mp.SecRoleId equals r.SecRoleId
                                                    where (mp.SecRoleId == roleId && m.IsActive == true && m.SecModuleId == parents.SecModuleId && m.IsParent == false && m.ParentMenuId == parents.Id && mp.IsActive == true && r.IsActive == true)
                                                    select new ChildMenu 
                                                    {
                                                        label = m.Title,
                                                        labelBn = m.TitleBn,
                                                        icon = m.Icon, 
                                                        routerLink = m.Link, 
                                                        ParentSerialNo = m.ParentSerialNo, 
                                                        ChildSerialNo = m.ChildSerialNo 
                                                    }).Distinct().OrderBy(x => x.ChildSerialNo).ToListAsync();

                            if (childMenus != null && childMenus.Count > 0)
                            {
                                var UserMenuobj = new UserMenu();
                                //New: By Asad
                                UserMenuobj.label = item.Name;
                                UserMenuobj.labelBn = item.NameBn;

                                //New: 18.01.2023
                                UserMenuobj.Icon = item.Icon;

                                //Old: Commented By Asad
                                //UserMenuobj.label = parents.Title;

                                UserMenuobj.items.AddRange(childMenus);
                                parentList.Add(UserMenuobj);
                            }
                        }
                        if (parentList.Count > 0)
                        {
                            var objduls = new Modules();
                            objduls.label = item.Name;
                            objduls.labelBn = item.NameBn;

                            //New: 18.01.2023
                            objduls.Icon = item.Icon;

                            objduls.items.AddRange(parentList);
                            moduleList.Add(objduls);
                            parentList = new List<UserMenu>();
                        }
                    }
                }
                //}
                return moduleList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //New: Asad- 05.02.2023
        public List<Modules> ModuleMenuByRole(int roleId, string withEventPerm) //New
        {
            var moduleList = new List<Modules>();
            var parentList = new List<UserMenu>();
            var childList = new List<ChildMenu>();

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@roleId", roleId);

            List<ModuleMenuViewModel> objModuleMenus = new List<ModuleMenuViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecMenu.GetModuleMenuByRoleId, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objModuleMenus.Add(((ModuleMenuViewModel)Helper.FillTo(row, typeof(ModuleMenuViewModel))));
                    }
                }

                var modules = objModuleMenus.Where(x => x.IsActive == true && x.Tier == 1).OrderBy(x => x.SerialNo).ToList();


                foreach (var item in modules)
                {
                    var modulParentMenu = objModuleMenus.Where(x => x.SecModuleId == item.SecModuleId && x.Tier == 2).ToList();
                    
                    if (modulParentMenu.Count() > 0)
                    {
                        foreach (var parents in modulParentMenu)
                        {
                           var cMenus = objModuleMenus.Where(x => x.SecModuleId == parents.SecModuleId && x.Tier == 3).ToList();

                            var childMenus = cMenus.Select(m => new ChildMenu  
                                                                {
                                                                    label = m.Name,
                                                                    labelBn = m.NameBn,
                                                                    icon = m.Icon,
                                                                    routerLink = m.Link,
                                                                    ParentSerialNo = m.ParentSerialNo,
                                                                    ChildSerialNo = m.ChildSerialNo
                                                                 }).Distinct().OrderBy(x => x.ChildSerialNo);
                                                        
                            if (childMenus != null && childMenus.Count() > 0)
                            {
                                var UserMenuobj = new UserMenu();
                                UserMenuobj.label = item.Name;
                                UserMenuobj.labelBn = item.NameBn;
                                UserMenuobj.Icon = item.Icon;
                                UserMenuobj.items.AddRange(childMenus);
                                parentList.Add(UserMenuobj);
                            }
                        }
                        if (parentList.Count > 0)
                        {
                            var objduls = new Modules();
                            objduls.label = item.Name;
                            objduls.labelBn = item.NameBn;
                            objduls.Icon = item.Icon;
                            objduls.items.AddRange(parentList);
                            moduleList.Add(objduls);
                            parentList = new List<UserMenu>();
                        }
                    }
                }














                return moduleList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
               
            //DataTable dt = new DataTable();
            //SqlParameter[] paramsToStore = new SqlParameter[1];
            //paramsToStore[0] = new SqlParameter("@roleId", roleId);

            //try
            //{
            //    dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecMenu.GetModuleMenuByRoleId, paramsToStore).Tables[0];
            //    return dt;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


    }
}
