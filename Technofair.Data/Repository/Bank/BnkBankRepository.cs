using Technofair.Data.Infrastructure;


using Technofair.Model.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace Technofair.Data.Repository.Bank
{
    #region Interface
    public interface IBnkBankRepository : IRepository<BnkBank>
    {
        int AddEntity(BnkBank obj);
    }

    #endregion


    public class BnkBankRepository : AdminBaseRepository<BnkBank>, IBnkBankRepository
    {

        public BnkBankRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(BnkBank obj)
        {
            int Id = 1;
            BnkBank last = DataContext.BnkBanks.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }
        


    }
}
