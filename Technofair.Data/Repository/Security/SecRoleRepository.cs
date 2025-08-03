
using TFSMS.Admin.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using TFSMS.Admin.Model.ViewModel.Security;
using TFSMS.Admin.Model.Common;
using static Azure.Core.HttpHeader;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.Security
{
    #region Interface



    public interface ISecRoleRepository : IRepository<SecRole>
    {
        int AddEntity(SecRole objSecRole);
        Task<List<SecRoleViewModel>> GetAllSecRole();
        Task<List<SecRole>> GetSecRoleByCompanyId(Int16 cmnCompanyId);
        Task<List<SecRole>> GetSecRoleByCompanyTypeId(Int16? cmnCompanyTypeId);
    }

    #endregion
     

    public class SecRoleRepository : AdminBaseRepository<SecRole>, ISecRoleRepository
    {

        public SecRoleRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(SecRole obj)
        {
            int Id = 1;
            SecRole last = DataContext.SecRoles.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        public async Task<List<SecRoleViewModel>> GetAllSecRole()
        {
            List<SecRoleViewModel> objSecRoles = await (from r in DataContext.SecRoles
                                                   join ct in DataContext.CmnCompanyTypes
                                                   on r.CmnCompanyTypeId equals ct.Id
                                                   where (r.IsActive == true)
                                                   orderby r.CmnCompanyTypeId ascending
                                                   select new SecRoleViewModel { 
                                                   Id = r.Id,
                                                   Name = r.Name,
                                                   CmnCompanyTypeId = ct.Id,
                                                   IsActive = r.IsActive,
                                                   CmnCompanyTypeName = ct.Name,
                                                   CmnCompanyTypeShortName = ct.ShortName,
                                                   CreatedBy = r.CreatedBy,
                                                   CreatedDate = r.CreatedDate,
                                                   ModifiedBy = r.ModifiedBy,
                                                   ModifiedDate = r.ModifiedDate
                                                    }).ToListAsync();
            return objSecRoles;
        }

        public async Task<List<SecRole>> GetSecRoleByCompanyId(Int16 cmnCompanyId)
        {
            //.Distinct().ToList();

            var companyTypes = await (
                        from ct in DataContext.CmnCompanyTypes
                        join c in DataContext.CmnCompanies
                        on ct.Id equals c.CmnCompanyTypeId
                        where (c.Id == cmnCompanyId || c.CmnCompanyId == cmnCompanyId) 
                        select new 
                        { 
                            ct.Id
                        }
            ).Distinct().ToListAsync();

            List<SecRole> objSecRoleList = new List<SecRole>();

            foreach (var item in DataContext.SecRoles)
            {
                foreach(var data in companyTypes)
                {
                   if(item.CmnCompanyTypeId == data.Id)
                    {
                        objSecRoleList.Add(item);
                    }
                }
            }

            //var objSecRoles = DataContext.CmnCompanies.Where(CmnCompanyType => ids.Contains(id));
            //List <SecRoleViewModel> objSecRoles = DataContext.CmnCompanies. 
                                                        
            return objSecRoleList;
        }

        public async Task<List<SecRole>> GetSecRoleByCompanyTypeId(Int16? cmnCompanyTypeId)
        {
            List<SecRole> objSecRoleList = new List<SecRole>();

            try
            {
                objSecRoleList = await DataContext.SecRoles.Where(x => x.CmnCompanyTypeId == cmnCompanyTypeId).ToListAsync();
            }
            catch(Exception ex)
            {
            }

            return objSecRoleList;
        }
    }
}
