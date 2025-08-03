
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
using Technofair.Model.ViewModel.Subscription;
using Technofair.Model.ViewModel.Security;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.Security
{
    public interface ISecUserRoleRepository : IRepository<SecUserRole>
    {
        Task<int> AddEntityAsync(SecUserRole obj);
        DataTable GetSecUserRoles(int userId);
        int DeleteByUserId(int userId);
        //DataTable GetUserRolesByCompanyAndRoleId(int companyId, int roleId);
        DataTable GetRoleLessUserByCompanyId(int companyId);
        DataTable GetRoleOrientedUserByCompanyRoleId(int companyId, int? roleId);
    }

    public class SecUserRoleRepository : AdminBaseRepository<SecUserRole>, ISecUserRoleRepository
    {
        public SecUserRoleRepository(IAdminDatabaseFactory databasefactory)
            : base(databasefactory)
        {
        }
        

        public async Task<int> AddEntityAsync(SecUserRole obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                SecUserRole last = DataContext.SecUserRoles.OrderByDescending(x => x.Id).FirstOrDefault();
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

        //public List<SecUserRoleViewModel> GetUserRoleByRoleId(int roleId)
        //{
        //    string query = " SELECT DISTINCT ISNULL(SecUserRoles.Id,0) Id,SecUsers.Id SecUserId,ISNULL(SecRoleId,0)SecRoleId,ISNULL(SecUserRoles.IsActive,0)IsActive,LoginID,Name EmployeeName FROM SecUsers LEFT JOIN ";
        //    query += " HrmEmployees ON SecUsers.HrmEmployeeId=HrmEmployees.Id LEFT JOIN ";
        //    query += " SecUserRoles ON SecUsers.Id=SecUserRoles.SecUserId AND SecUserRoles.SecRoleId=" + roleId;
        //    try
        //    {
        //        List<SecUserRoleViewModel> list = null;
        //        DataTable dt = new DataTable();
        //        dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.Text, query, null).Tables[0];
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            list = new List<SecUserRoleViewModel>();
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                list.Add((SecUserRoleViewModel)Helper.FillTo(row, typeof(SecUserRoleViewModel)));
        //            }
        //        }
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex; ;
        //    }
        //}

        List<CmnCompany> childs = new List<CmnCompany>();
        public void FindChild(int parentId, List<CmnCompany> list)
        {
            CmnCompany com = new CmnCompany();
            for (int i = 0; i < list.Count; i++)
            {
                com = list[i];
                if (com.CmnCompanyId == parentId)
                {
                    childs.Add(com);
                    int innerParent = com.Id;
                    FindChild(innerParent, list);
                }
            }
        }
        //public DataTable GetUserRolesByCompanyAndRoleId(int companyId,int roleId)
        //{            
        //    DataTable dt = new DataTable();
        //    SqlParameter[] paramsToStore = new SqlParameter[2];
        //    paramsToStore[0] = new SqlParameter("@companyId", companyId);
        //    paramsToStore[1] = new SqlParameter("@roleId", roleId);
        //    try
        //    {
        //        dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetSecUserRolesByCompanyAndRoleId, paramsToStore).Tables[0];
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //New: 04.11.2024
        //
        public DataTable GetRoleLessUserByCompanyId(int companyId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@companyId", companyId);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetRoleLessUserByCompanyId, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRoleOrientedUserByCompanyRoleId(int companyId, int? roleId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@companyId", companyId);
            paramsToStore[1] = new SqlParameter("@roleId", roleId);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetRoleOrientedUserByCompanyRoleId, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetSecUserRoles(int userId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecUserId", userId);
                DataTable dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUserRole.GetSecUserRoleByUserId, parameters).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteByUserId(int userId)
        {
            int ret = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecUserId", userId);
                ret = Helper.ExecuteNonQuery(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUserRole.DeleteSecUserRolesByUserId, parameters);
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
