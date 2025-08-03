using Technofair.Lib.Utilities;
using Technofair.Model.HRM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.HRM
{
    #region Interface
    public interface IHrmDesignationRepository : TFSMS.Admin.Data.Infrastructure.IRepository<HrmDesignation>
    {
        int AddEntity(HrmDesignation obj);
        HrmDesignation? GetDesignationByNameAndShortName(string name, string shortName);
    }

    #endregion
    public class HrmDesignationRepository : AdminBaseRepository<HrmDesignation>, IHrmDesignationRepository
    {
        public HrmDesignationRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(HrmDesignation obj)
        {
            int Id = 1;
            HrmDesignation last = DataContext.HrmDesignations.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public HrmDesignation? GetDesignationByNameAndShortName(string name, string shortName)
        {
            HrmDesignation? hrmDesignation = DataContext.HrmDesignations.Where(x => x.Name == name && x.ShortName == shortName).FirstOrDefault();

            return hrmDesignation;
        }


    }
}
