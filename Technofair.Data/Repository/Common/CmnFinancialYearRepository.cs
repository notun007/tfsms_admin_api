//using Technofair.Data.Infrastructure;
using Technofair.Model.Common;
using Technofair.Model.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Data.Infrastructure;

namespace TFSMS.Admin.Data.Repository.Common
{

    #region Interface
    public interface ICmnFinancialYearRepository : IRepository<CmnFinancialYear>
    {
        int AddEntity(CmnFinancialYear obj);
    }

    #endregion

    public class CmnFinancialYearRepository : BaseRepository<CmnFinancialYear>, ICmnFinancialYearRepository
    {
        public CmnFinancialYearRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(CmnFinancialYear obj)
        {
            int Id = 1;
            CmnFinancialYear? last = DataContext.CmnFinancialYears.OrderByDescending(x => x.Id).FirstOrDefault();
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
