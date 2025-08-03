using Technofair.Lib.Utilities;
using Technofair.Model.Common;
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
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.Security
{
    #region Interface



    public interface ISecCompanyUserRepository : IRepository<SecCompanyUser>
    {
        int AddEntity(SecCompanyUser objSecCompanyUser);
        DataTable GetCompanyUserByUserId(int userId);
        DataTable GetCompanyUserByUserIdForMapping(int userId);
    }

    #endregion


    public class SecCompanyUserRepository : AdminBaseRepository<SecCompanyUser>, ISecCompanyUserRepository
    {

        public SecCompanyUserRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(SecCompanyUser objSecCompanyUser)
        {
            int Id = 1;
            SecCompanyUser last = DataContext.SecCompanyUsers.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecCompanyUser.Id = Id;
            base.Add(objSecCompanyUser);
            return Id;

        }

        public DataTable GetCompanyUserByUserId(int userId)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecUserId", userId);
                DataTable dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecCompanyUser.GetSecCompanyUsersBySecUserId, parameters).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCompanyUserByUserIdForMapping(int userId)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecUserId", userId);
                DataTable dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecCompanyUser.GetSecCompanyUserBySecUserIdForMapping, parameters).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}
