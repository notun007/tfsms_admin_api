using Microsoft.EntityFrameworkCore;
using Technofair.Lib;

using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Model.ViewModel.Security;
using TFSMS.Admin.Model.ViewModel.Subscription;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.Security
{

    public interface ISecMenuRepository : IRepository<SecMenu>
    {
        int AddEntity(SecMenu obj);
        Task<SecMenu> Save(SecMenu obj);
        Task<SecMenu> Update(SecMenu obj);
        Task<List<DTO>> GetChildMenuName();
        Task<List<SecMenu>> GetMenus();
        Task<List<DTO>> GetParentsMenu();
        Task<List<DTO>> GetRoleName();
        Task<List<SecMenu>> GetMenuList();
    }

    public class SecMenuRepository : AdminBaseRepository<SecMenu>, ISecMenuRepository
    {
        public SecMenuRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(SecMenu obj)
        {
            int Id = 1;
            SecMenu last = DataContext.SecMenus.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }
      
        public async Task<SecMenu> Save(SecMenu obj)
        {
            using (var transaction = DataContext.Database.BeginTransaction())
            {
                try
                {
                    obj.Id = AddEntity(obj);
                    var existmenu = await DataContext.SecMenus.FirstOrDefaultAsync(x => x.Id == obj.Id);
                    if (existmenu == null)
                    {
                        existmenu.Id = obj.Id;
                        existmenu.TitleBn = obj.TitleBn;
                        existmenu.Title = obj.Title;
                        existmenu.Link = existmenu.Link;
                        existmenu.ParentMenuId = obj.ParentMenuId;
                        existmenu.SecModuleId = obj.SecModuleId;

                        existmenu.ParentSerialNo = obj.ParentSerialNo;
                        existmenu.ChildSerialNo = obj.ChildSerialNo;
                        existmenu.LevelNo = existmenu.LevelNo;
                        existmenu.IsParent = obj.IsParent;
                        existmenu.Icon = obj.Icon;
                        existmenu.IsActive = obj.IsActive;
                        existmenu.CreatedBy = obj.CreatedBy;
                        existmenu.CreatedDate = obj.CreatedDate;
                        existmenu.IsModule = obj.IsModule;
                        DataContext.SecMenus.Add(obj);
                        DataContext.SaveChanges();
                        transaction.Commit();
                    }
                    return obj;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        //public async Task<SecMenu> Save(SecMenu obj)
        //{
        //    string msg = string.Empty;
        //    using (var transaction = DataContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            if (obj.Id == 0)
        //            {
        //                int parentsSeq = 0;
        //                int childSeq = 0;
        //                if (obj.IsParent == true)
        //                {
        //                    obj.LevelNo = 1;
        //                    var dd = await DataContext.SecMenus.OrderByDescending(x => x.ParentSerialNo).FirstOrDefaultAsync(x => x.SecModuleId == obj.SecModuleId);
        //                    if (dd != null)
        //                    {
        //                        parentsSeq = Convert.ToInt32(dd.ParentSerialNo) + 1;
        //                    }
        //                    else
        //                    {
        //                        parentsSeq = 1;
        //                    }
        //                }
        //                else
        //                {
        //                    obj.LevelNo = 2;
        //                    var dd = await DataContext.SecMenus.OrderByDescending(x => x.ChildSerialNo).FirstOrDefaultAsync(x => x.ParentMenuId == obj.ParentMenuId && x.SecModuleId == obj.SecModuleId);
        //                    if (dd != null)
        //                    {
        //                        childSeq = Convert.ToInt32(dd.ChildSerialNo) + 1;
        //                    }
        //                    else
        //                    {
        //                        childSeq = 1;
        //                    }
        //                }
        //                obj.ParentSerialNo = parentsSeq;
        //                obj.ChildSerialNo = childSeq;
        //                obj.IsActive = true;
        //                obj.ParentMenuId = obj.Link == "#" ? obj.ParentMenuId == null ? 0 : obj.ParentMenuId : obj.IsParent == true ? 0 : obj.ParentMenuId;
        //                obj.CreatedDate = DateTime.Now;
        //                DataContext.SecMenus.Add(obj);
        //                DataContext.SaveChanges();
        //            }
        //            transaction.Commit();
        //            return obj;
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
      
        public async Task<SecMenu> Update(SecMenu obj)
        {
            using (var transaction = DataContext.Database.BeginTransaction())
            {
                try
                {
                    var existmenu = await DataContext.SecMenus.FirstOrDefaultAsync(x => x.Id == obj.Id);
                    if (existmenu != null)
                    {
                        existmenu.TitleBn = obj.TitleBn;
                        existmenu.Title = obj.Title;
                        existmenu.Link = obj.Link;
                        existmenu.ParentMenuId = obj.ParentMenuId;
                        existmenu.SecModuleId = obj.SecModuleId;

                        existmenu.ParentSerialNo = obj.ParentSerialNo;
                        existmenu.ChildSerialNo = obj.ChildSerialNo;
                        existmenu.LevelNo = existmenu.LevelNo;
                        existmenu.IsParent = obj.IsParent;
                        existmenu.Icon = obj.Icon;
                        existmenu.IsActive = obj.IsActive;
                        existmenu.CreatedBy = obj.CreatedBy;
                        existmenu.CreatedDate = obj.CreatedDate;
                        existmenu.IsModule = obj.IsModule;
                        //existmenu.ParentMenuId = obj.IsParent == true ? 0 : obj.ParentMenuId;
                        //existmenu.ParentSerialNo = parentsSeq;
                        //existmenu.ChildSerialNo = childSeq;

                        DataContext.Entry(existmenu).State = EntityState.Modified;
                        DataContext.SaveChanges();
                        transaction.Commit();
                    }
                    return obj;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
       
        //public async Task<SecMenu> Update(SecMenu obj)
        //{
        //    using (var transaction = DataContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var existmenu = await DataContext.SecMenus.FirstOrDefaultAsync(x => x.Id == obj.Id);
        //            if (existmenu != null)
        //            {
        //                //int parentsSeq = 0;
        //                //int childSeq = 0;
        //                //if (obj.IsParent == FixedId.Yes)
        //                //{
        //                //    obj.LevelNo = 1;
        //                //    var dd = await DataContext.Menu.OrderByDescending(x => x.ParentSerialNo).FirstOrDefaultAsync();
        //                //    if (dd != null)
        //                //    {
        //                //        parentsSeq = Convert.ToInt32(dd.ParentSerialNo) + 1;
        //                //    }
        //                //    else
        //                //    {
        //                //        parentsSeq = 1;
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    obj.LevelNo = 2;
        //                //    var dd = await DataContext.Menu.OrderByDescending(x => x.ChildSerialNo).FirstOrDefaultAsync(x => x.SecSecMenuId == obj.SecSecMenuId);
        //                //    if (dd != null)
        //                //    {
        //                //        childSeq = Convert.ToInt32(dd.ChildSerialNo) + 1;
        //                //    }
        //                //    else
        //                //    {
        //                //        childSeq = 1;
        //                //    }
        //                //}

        //                existmenu.Title = obj.Title;
        //                existmenu.Link = existmenu.Link;
        //                existmenu.IsParent = obj.IsParent;
        //                existmenu.ParentMenuId = obj.IsParent == true ? 0 : obj.ParentMenuId;
        //                //existmenu.ParentSerialNo = parentsSeq;
        //                //existmenu.ChildSerialNo = childSeq;
        //                existmenu.IsActive = obj.IsActive;
        //                DataContext.Entry(existmenu).State = EntityState.Modified;
        //                DataContext.SaveChanges();
        //                transaction.Commit();
        //            }
        //            return obj;
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}


        public async Task<List<DTO>> GetChildMenuName()
        {
            return await DataContext.SecMenus.Where(x => x.IsActive == true).Select(c => new DTO { Text = c.Title, Value = c.Id }).ToListAsync();
        }

        public async Task<List<SecMenu>> GetMenus()
        {
            try
            {
                return await DataContext.SecMenus.OrderBy(x => x.ParentMenuId).ThenBy(x => x.ChildSerialNo).ThenBy(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<DTO>> GetParentsMenu()
        {
            var menuList = await DataContext.SecMenus.Where(x => x.IsActive == true && x.IsParent == true).Select(c => new DTO { Value = c.Id, Text = c.Title }).ToListAsync();
            return menuList;
        }

        public async Task<List<DTO>> GetRoleName()
        {
            return await DataContext.SecRoles.Where(x => x.IsActive == true).Select(c => new DTO { Text = c.Name, Value = c.Id }).ToListAsync();
        }
        public async Task<List<SecMenu>> GetMenuList()
        {
            return await DataContext.SecMenus.ToListAsync();
        }

        

        public async Task<List<SecMenuViewModel>> GetMenusByRole(int roleId)
        {
            var menusList = from r in DataContext.SecMenuPermissions.Where(x => x.SecRoleId == roleId)//&& x.IsActive == true
                            join role in DataContext.SecRoles on r.SecRoleId equals role.Id
                            join m in DataContext.SecMenus.Where(x => x.IsActive == true) on r.SecMenuId equals m.Id
                            orderby m.ParentSerialNo
                            orderby m.ChildSerialNo
                            select new SecMenuViewModel { Link = m.Link, Title = m.Title, RoleName = role.Name, Icon = m.Icon, SecMenuId = m.ParentMenuId, ParentSerialNo = m.ParentSerialNo, Id = m.Id, ChildSerialNo = m.ChildSerialNo };

            var rs = await menusList.ToListAsync();
            return rs;
        }

    }
}

