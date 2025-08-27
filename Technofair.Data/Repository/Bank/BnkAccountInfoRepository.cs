using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data;
using Technofair.Data.Infrastructure;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Technofair.Lib.Utilities;
using Technofair.Lib.Model;
using Technofair.Model.Bank;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Model.ViewModel.Bank;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;



namespace Technofair.Data.Repository.Bank
{

    #region Interface
    public interface IBnkAccountInfoRepository : IRepository<BnkAccountInfo>
    {
        //IList<BnkAccountType> GetById(int Id);
        int AddEntity(BnkAccountInfo obj);
       // List<BnkAccountInfoViewModel> BankAccountInfoByCompanyId(int companyId);
    }

    #endregion

    public class BnkAccountInfoRepository : AdminBaseRepository<BnkAccountInfo>, IBnkAccountInfoRepository
    {
        public BnkAccountInfoRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(BnkAccountInfo obj)
        {
            int Id = 1;
            BnkAccountInfo last = DataContext.BnkAccountInfos.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id+ 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        //public List<BnkAccountViewModel> BankAccountInfoByCompanyId(int companyId)
        //{
        //    List<BnkAccountViewModel> list = (from a in DataContext.BnkAccounts
        //                                      join bn in DataContext.BnkBranches on a.BnkBranchId equals bn.Id
        //                                      join b in DataContext.BnkBanks on bn.BnkBankId equals b.Id
        //                                      where (a.CmnCompanyId == companyId)
        //                                      select new BnkAccountViewModel()
        //                                      {
        //                                          Id = a.Id,
        //                                          BnkBranchId = a.BnkBranchId,
        //                                          BnkAccountTypeId = a.BnkAccountTypeId,
        //                                          AccountName = a.AccountName,
        //                                          AccountNo = a.AccountNo,
        //                                          CmnCurrencyId=a.CmnCurrencyId,
        //                                          OpeningDate = a.OpeningDate,
        //                                          ClosingDate = a.ClosingDate,
        //                                          MaturityDuration = a.MaturityDuration,
        //                                          MinimumBalance = a.MinimumBalance,
        //                                          IsDeposited = a.IsDeposited,
        //                                          IsFunded = a.IsFunded,
        //                                          BnkAccountId = a.BnkAccountId,

        //                                          FxdAssetId = a.FxdAssetId,
        //                                          NoofYears = a.NoofYears,
        //                                          InstallmentPerYear = a.InstallmentPerYear,
        //                                          InstallmentSize = a.InstallmentSize,
        //                                          TotalInstallment = a.TotalInstallment,
        //                                          CmnProjectId = a.CmnProjectId,
        //                                          AnFChartOfAccountId = a.AnFChartOfAccountId,
        //                                          CmnCompanyId = a.CmnCompanyId,
        //                                          CreatedBy = a.CreatedBy,
        //                                          CreatedDate = a.CreatedDate,
        //                                          ModifiedBy = a.ModifiedBy,
        //                                          ModifiedDate = a.ModifiedDate,
        //                                          BranchName = bn.Name,
        //                                          BnkBankId=b.Id,
        //                                          BankName = b.Name,

        //                                      }).ToList();
        //    return list;
        //}
		
    }

}
