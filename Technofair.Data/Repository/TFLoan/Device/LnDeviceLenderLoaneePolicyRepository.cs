using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Technofair.Model.TFAdmin;
using Technofair.Model.TFLoan.Device;
using Technofair.Model.ViewModel.TFLoan;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace Technofair.Data.Repository.TFLoan.Device
{
    public interface ILnDeviceLenderLoaneePolicyRepository : IRepository<LnDeviceLenderLoaneePolicy>
    {
        Int16 AddEntity(LnDeviceLenderLoaneePolicy obj);
        Task<Int16> AddEntityAsync(LnDeviceLenderLoaneePolicy obj);
        Task<List<LnDeviceLenderLoaneePolicyViewModel>> GetDeviceLenderLoaneePolicy();
    }
    public class LnDeviceLenderLoaneePolicyRepository : AdminBaseRepository<LnDeviceLenderLoaneePolicy>, ILnDeviceLenderLoaneePolicyRepository
    {
        public LnDeviceLenderLoaneePolicyRepository(IAdminDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {

        }

        public Int16 AddEntity(LnDeviceLenderLoaneePolicy obj)
        {
            Int16 Id = 1;
            LnDeviceLenderLoaneePolicy last = DataContext.LnDeviceLenderLoaneePolicies.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int16> AddEntityAsync(LnDeviceLenderLoaneePolicy obj)
        {
            Int16 Id = 1;
            LnDeviceLenderLoaneePolicy last = DataContext.LnDeviceLenderLoaneePolicies.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }
        public async Task<List<LnDeviceLenderLoaneePolicyViewModel>> GetDeviceLenderLoaneePolicy()
        {
            //New
            var result = from dd in DataContext.LnDeviceLenderLoaneePolicies
                         join lender in DataContext.CmnCompanies on dd.LenderId equals lender.Id
                         join loanee in DataContext.TFACompanyCustomers on dd.LoaneeId equals loanee.Id
                         select new LnDeviceLenderLoaneePolicyViewModel
                         {
                             Id = dd.Id,
                             LenderId = dd.LenderId,
                             LoaneeId = dd.LoaneeId,
                             MonthlyInstallmentAmount = dd.MonthlyInstallmentAmount,
                             PerRechargeInstallmentAmount = dd.PerRechargeInstallmentAmount,
                             IsActive = dd.IsActive,
                             LenderName = lender.Name,
                             LoaneeName = loanee.Name
                         };

            //Old
            //var result = from dd in DataContext.LnDeviceLenderLoaneePolicies
            //             join lender in DataContext.CmnCompanies on dd.LenderId equals lender.Id
            //             join loanee in DataContext.CmnCompanies on dd.LoaneeId equals loanee.Id
            //             select new LnDeviceLenderLoaneePolicyViewModel
            //             {
            //                 Id = dd.Id,
            //                 LenderId = dd.LenderId,
            //                 LoaneeId = dd.LoaneeId,
            //                 MonthlyInstallmentAmount = dd.MonthlyInstallmentAmount,
            //                 PerRechargeInstallmentAmount = dd.PerRechargeInstallmentAmount,
            //                 IsActive = dd.IsActive,
            //                 LenderName = lender.Name,
            //                 LoaneeName = loanee.Name
            //             };

            return await result.ToListAsync();
        }
    }
}
