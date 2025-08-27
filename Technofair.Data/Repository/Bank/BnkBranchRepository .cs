using Technofair.Data.Infrastructure;
using Technofair.Lib.Utilities;


using Technofair.Model.Bank;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Technofair.Model.ViewModel.Bank;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace Technofair.Data.Repository.Bank
{
    #region Interface



    public interface IBnkBranchRepository : IRepository<BnkBranch>
    {
        int AddEntity(BnkBranch obj);
        List<BnkBranchViewModel> GetBranchByBankId(int bankId);
    }

    #endregion


    public class  BnkBranchRepository : AdminBaseRepository<BnkBranch>, IBnkBranchRepository
    {

        public BnkBranchRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(BnkBranch obj)
        {
            int Id = 1;
            BnkBranch last = DataContext.BnkBranches.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        public List<BnkBranchViewModel> GetBranchByBankId(int bankId)
        {
            try
            {
                List<BnkBranchViewModel> list = (from br in DataContext.BnkBranches
                                                 join b in DataContext.BnkBanks on br.BnkBankId equals b.Id
                                                 where (b.Id == bankId)
                                                 select new BnkBranchViewModel { Id = br.Id, BnkBankId = br.BnkBankId, Name = br.Name, Location = br.Location, Phone = br.Phone, Fax = br.Fax, Email = br.Email, Remarks = br.Remarks, Status = br.Status, CreatedBy = br.CreatedBy, CreatedDate = br.CreatedDate, ModifiedBy = br.ModifiedBy, ModifiedDate = br.ModifiedDate, BankName = b.Name }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
