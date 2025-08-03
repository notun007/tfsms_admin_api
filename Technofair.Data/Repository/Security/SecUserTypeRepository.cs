using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Lib.Utilities;
using Technofair.Model.Security;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.Security
{
    //public class SecUserTypeRepository
    //{
    //}

    #region Interface

    public interface ISecUserTypeRepository : IRepository<SecUserType>
    {
        Task<List<SecUserType>> GetAllSecUserType();

        //IList<SecModule> GetByCompanyId(int companyId);
        //int AddEntity(SecModule objSecModule);
        //SecModule GetSecModuleById(int nullable);
        //DataTable GetSecPermittedModuleByUserId(int userId, int companyId);
        //DataTable GetModuleByCompanyId(int companyId);
    }

    #endregion Interface

    public class SecUserTypeRepository : AdminBaseRepository<SecUserType>, ISecUserTypeRepository
    {
        public SecUserTypeRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public async Task<List<SecUserType>> GetAllSecUserType()
        {
            List<SecUserType> list = await DataContext.SecUserTypes.ToListAsync();
            return list;
        }


    }
}
