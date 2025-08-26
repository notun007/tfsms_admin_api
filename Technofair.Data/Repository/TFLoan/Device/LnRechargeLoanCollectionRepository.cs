using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Lib.Utilities;
using Technofair.Model.TFLoan.Device;
using Technofair.Model.ViewModel.TFLoan;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace Technofair.Data.Repository.TFLoan.Device
{
    public interface ILnRechargeLoanCollectionRepository : IRepository<LnRechargeLoanCollection>
    {
        Int64 AddEntity(LnRechargeLoanCollection obj);
        Task<Int64> AddEntityAsync(LnRechargeLoanCollection obj);
        //List<RechargeLoanCollectionSummaryViewModel> GetRechargeLoanCollectionByLoanNo(string loanNo);
    }
    public class LnRechargeLoanCollectionRepository : AdminBaseRepository<LnRechargeLoanCollection>, ILnRechargeLoanCollectionRepository
    {
        public LnRechargeLoanCollectionRepository(IAdminDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {

        }

        public Int64 AddEntity(LnRechargeLoanCollection obj)
        {
            Int64 Id = 1;
            LnRechargeLoanCollection last = DataContext.LnRechargeLoanCollections.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int64> AddEntityAsync(LnRechargeLoanCollection obj)
        {
            Int64 Id = 1;
            LnRechargeLoanCollection last = DataContext.LnRechargeLoanCollections.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }

        //public List<RechargeLoanCollectionSummaryViewModel> GetRechargeLoanCollectionByLoanNo(string loanNo)
        //{

        //    List<RechargeLoanCollectionSummaryViewModel> list = new List<RechargeLoanCollectionSummaryViewModel>();

        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        SqlParameter[] paramsToStore = new SqlParameter[1];
        //        paramsToStore[0] = new SqlParameter("@loanNo", loanNo);

        //        dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.DeviceLoan.GetRechargeLoanCollectionByLoanNo, paramsToStore).Tables[0];

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                list.Add(((RechargeLoanCollectionSummaryViewModel)Helper.FillTo(row, typeof(RechargeLoanCollectionSummaryViewModel))));
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //    return list;
        //}
    }
}
