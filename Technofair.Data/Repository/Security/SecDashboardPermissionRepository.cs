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
using TFSMS.Admin.Data.Infrastructure;

namespace TFSMS.Admin.Data.Repository.Security
{
   
    #region Interface
    public interface ISecDashboardPermissionRepository : IRepository<SecDashboardPermission>   
    {
        long AddEntity(SecDashboardPermission objSecDashboardPermission);
        List<PermittedDashboard> GetPermittedDashBoard(int companyId, int roleId, int moduleId);
        DataTable GetDashboardPermissionByRoleId(int roleId, int moduleId);
        int Delete(int roleId, int moduleId);
    }

    #endregion
    public class SecDashboardPermissionRepository : BaseRepository<SecDashboardPermission>, ISecDashboardPermissionRepository
    {

        public SecDashboardPermissionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public long AddEntity(SecDashboardPermission objSecDashboardPermission)
        {
            int Id = 1;
            SecDashboardPermission last = DataContext.SecDashboardPermissions.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecDashboardPermission.Id = Id;
            base.Add(objSecDashboardPermission);
            return Id;

        }

        public List<PermittedDashboard> GetPermittedDashBoard(int companyId, int roleId, int moduleId)
        {
            var list = (from d in DataContext.SecDashboards
                        join dp in DataContext.SecDashboardPermissions on d.Id equals dp.SecDashboardId
                        where d.CmnCompanyId == companyId && d.SecModuleId==moduleId && dp.SecRoleId==roleId && dp.IsPermitted==true
                        select new PermittedDashboard
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Url = d.Url,
                            SecModuleId = d.SecModuleId,
                            CmnCompanyId = d.CmnCompanyId,
                            Status = d.Status,
                            Tag = d.Tag,
                            IsActive = d.IsActive                            
                        }).ToList();           

            return list;

        }

        public DataTable GetDashboardPermissionByRoleId(int roleId, int moduleId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SecRoleId", roleId);
                parameters[1] = new SqlParameter("@SecModuleId", moduleId);
                DataTable dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecDashboardPermission.GetSecDashboardPermissionByRoleId, parameters).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int roleId, int moduleId)
        {
            int ret = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SecRoleId", roleId);
                parameters[1] = new SqlParameter("@SecModuleId", moduleId);
                ret = Helper.ExecuteNonQuery(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecDashboardPermission.DeleteSecDashboardPermissionByRoleId, parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }





    }
}
