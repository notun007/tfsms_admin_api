using Technofair.Lib.Utilities;
using Technofair.Model.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.Security
{
    #region Interface

    public interface ISecModuleRepository : IRepository<SecModule>
    {
        IList<SecModule> GetByCompanyId(int companyId);
        int AddEntity(SecModule objSecModule);
        SecModule GetSecModuleById(int nullable);
        DataTable GetSecPermittedModuleByUserId(int userId, int companyId);
        DataTable GetModuleByCompanyId(int companyId);
        SecModule? GetModuleByName(string name, string nameBn);
    }

    #endregion Interface

    public class SecModuleRepository : AdminBaseRepository<SecModule>, ISecModuleRepository
    {
        public SecModuleRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public int AddEntity(SecModule objSecModule)
        {
            int Id = 1;
            SecModule last = DataContext.SecModules.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecModule.Id = Id;
            base.Add(objSecModule);
            return Id;

        }
        public DataTable GetSecPermittedModuleByUserId(int userId, int companyId)
        {

            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SecUserId", userId);
                parameters[1] = new SqlParameter("@CmnCompanyId", companyId);

                dt = Helper.GetFromStoredProcedure(DataContext.Database.GetDbConnection(), SPList.SecModule.GetSecPermittedModuleByUserId, parameters, true);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetModuleByCompanyId(int companyId)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@CompanyId", companyId);
                dt = Helper.GetFromStoredProcedure(DataContext.Database.GetDbConnection(), SPList.SecModule.GetSecModulesByCompanyId, parameters, true);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecModule GetSecModuleById(int nullable)
        {
            return DataContext.SecModules.Where(x => x.Id == nullable).FirstOrDefault();
        }
        public IList<SecModule> GetByCompanyId(int companyId)
        {
            List<SecModule> list = (from m in DataContext.SecModules
                                    join cm in DataContext.SecCompanyModules on m.Id equals cm.SecModuleId
                                    where (cm.CmnCompanyId == companyId)
                                    select m).ToList();
            return list;
        }

        public SecModule? GetModuleByName(string name, string nameBn)
        {
            SecModule? secModule = DataContext.SecModules.Where(x => x.Name == name && x.NameBn == nameBn).FirstOrDefault();

            return secModule;
        }


    }
}