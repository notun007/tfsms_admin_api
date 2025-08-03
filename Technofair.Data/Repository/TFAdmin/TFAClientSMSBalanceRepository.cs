using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Model.ViewModel.Common;

using Technofair.Model.TFAdmin;
using System.Data.SqlClient;
using Technofair.Model.ViewModel.TFAdmin;
using Microsoft.EntityFrameworkCore;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
  
    #region Interface
    public interface ITFAClientSMSBalanceRepository : IRepository<TFAClientSMSBalance>
    {
        int AddEntity(TFAClientSMSBalance obj);
        List<TFASMSBalanceViewModel> GetSMSBalanceByClientId(int clientId);
        Task<List<TFASMSBalanceViewModel>> GetCustomerDetails();
    }

    #endregion

    public class TFAClientSMSBalanceRepository : AdminBaseRepository<TFAClientSMSBalance>, ITFAClientSMSBalanceRepository
    {

        public TFAClientSMSBalanceRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(TFAClientSMSBalance obj)
        {
            int Id = 1;
            TFAClientSMSBalance last = DataContext.TFAClientSMSBalances.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }



        public List<TFASMSBalanceViewModel> GetSMSBalanceByClientId(int clientId)
        {
            List<TFASMSBalanceViewModel> list = (from csb in DataContext.TFAClientSMSBalances
                                                       join cc in DataContext.TFACompanyCustomers on csb.TFACompanyCustomerId equals cc.Id
                                                       where (cc.Id == clientId)
                                                       select new TFASMSBalanceViewModel()
                                                       {
                                                           Id = csb.Id,
                                                           TFACompanyCustomerId = csb.TFACompanyCustomerId,
                                                           Date = csb.Date,
                                                           NoOfMessage = csb.NoOfMessage,
                                                           Balance = csb.Balance,
                                                           Rate = csb.Rate,
                                                           IsActive = csb.IsActive,
                                                           TFACompanyCustomer = cc.Name,
                                                           CreatedBy = csb.CreatedBy,
                                                           CreatedDate = csb.CreatedDate,
                                                           ModifiedBy = csb.ModifiedBy,
                                                           ModifiedDate = csb.ModifiedDate,
                                                           Domain = cc.Web
                                                       }).Distinct().ToList();
            return list;
        }

        public async Task<List<TFASMSBalanceViewModel>> GetCustomerDetails()
        {
            var obj = from csb in DataContext.TFAClientSMSBalances
                      join cc in DataContext.TFACompanyCustomers on csb.TFACompanyCustomerId equals cc.Id
                      select new TFASMSBalanceViewModel
                      {
                          Id = csb.Id,
                          TFACompanyCustomerId = csb.TFACompanyCustomerId,
                          TFACompanyCustomer = cc.Name,
                          Date = csb.Date,
                          NoOfMessage = csb.NoOfMessage,
                          Balance = csb.Balance,
                          Rate = csb.Rate,
                          IsActive = csb.IsActive,
                          CreatedBy = csb.CreatedBy,
                          CreatedDate = csb.CreatedDate,
                          ModifiedBy = csb.ModifiedBy,
                          ModifiedDate = csb.ModifiedDate,
                      };

            return await obj.ToListAsync();
        }

    }

}
