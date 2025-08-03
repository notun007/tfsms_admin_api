using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Model.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
   
    #region Interface
    public interface ITFACompanyPackageTypeRepository : IRepository<TFACompanyPackageType>
    {
        int AddEntity(TFACompanyPackageType obj);
        List<TFACompanyPackageType> GetCompanyPackageTypeByAllowPackage(bool allowPackage);
    }

    #endregion
    public class TFACompanyPackageTypeRepository : AdminBaseRepository<TFACompanyPackageType>, ITFACompanyPackageTypeRepository
    {
        public TFACompanyPackageTypeRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(TFACompanyPackageType obj)
        {
            int Id = 1;
            TFACompanyPackageType last = DataContext.TFACompanyPackageTypes.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public List<TFACompanyPackageType> GetCompanyPackageTypeByAllowPackage(bool allowPackage)
        {
            List<TFACompanyPackageType> list = DataContext.TFACompanyPackageTypes.Where(x => x.AllowPackage == allowPackage).ToList();
            return list;
        }
    }
}
