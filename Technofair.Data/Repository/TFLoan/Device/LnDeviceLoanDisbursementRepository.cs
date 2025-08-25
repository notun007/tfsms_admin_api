using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;


namespace TFSMS.Admin.Data.Repository.TFLoan.Device
{
    public interface ILnDeviceLoanDisbursementRepository : IRepository<LnDeviceLoanDisbursement>
    {
        Int64 AddEntity(LnDeviceLoanDisbursement obj);
        Task<LnDeviceLoanDisbursement> AddEntityAsync(LnDeviceLoanDisbursement obj);
        Task<List<LnDeviceLoanDisbursementViewModel>> GetDeviceLoanDisbursement();
        DataTable NextLoanNo();
        Task<List<LnDeviceLoanDisbursement>> GetLoanDisbursementByLoaneeId(int loaneeId);
        Task<List<string>> GetLoanNoByLoaneeId(int loaneeId);

    }
    public class LnDeviceLoanDisbursementRepository : AdminBaseRepository<LnDeviceLoanDisbursement>, ILnDeviceLoanDisbursementRepository
    {
        public LnDeviceLoanDisbursementRepository(IAdminDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {

        }

        public Int64 AddEntity(LnDeviceLoanDisbursement obj)
        {
            Int64 Id = 1;
            LnDeviceLoanDisbursement last = DataContext.LnDeviceLoanDisbursements.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<LnDeviceLoanDisbursement> AddEntityAsync(LnDeviceLoanDisbursement obj)
        {
            Int64 Id = 1;
            LnDeviceLoanDisbursement last = DataContext.LnDeviceLoanDisbursements.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return obj;
        }

        public async Task<List<LnDeviceLoanDisbursementViewModel>> GetDeviceLoanDisbursement()
        {
            var result = await (from dd in DataContext.LnDeviceLoanDisbursements
                                join lender in DataContext.CmnCompanies on dd.LenderId equals lender.Id
                                join loanee in DataContext.TFACompanyCustomers on dd.LoaneeId equals loanee.Id
                                select new LnDeviceLoanDisbursementViewModel
                                {
                                    Id = dd.Id,
                                    LoanNo = dd.LoanNo,
                                    LenderId = lender.Id,
                                    LoaneeId = loanee.Id,
                                    NumberOfDevice = dd.NumberOfDevice,
                                    Rate = dd.Rate,
                                    TotalAmount = dd.TotalAmount,
                                    DownPaymentAmount = dd.DownPaymentAmount,
                                    LoanAmount = dd.LoanAmount,
                                    Remarks = dd.Remarks,
                                    LenderName = lender.Name,
                                    LoaneeName = loanee.Name,
                                    CreatedDate = dd.CreatedDate,
                                    InstallmentStartDate = dd.InstallmentStartDate
                                    
                                }).ToListAsync();

            return result;
        }

        public DataTable NextLoanNo()
        {

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[0];
            //paramsToStore[0] = new SqlParameter("@cmnCompanyId", cmnCompanyId);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SubscriberSP.GetNextLoanNo, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Asad
        public async Task<List<LnDeviceLoanDisbursement>> GetLoanDisbursementByLoaneeId(int loaneeId)
        {

            var loans = await DataContext.LnDeviceLoanDisbursements
                .Where(d => d.LoaneeId == loaneeId)
                .ToListAsync();

            return loans;
        }

        //Farida
        public async Task<List<string>> GetLoanNoByLoaneeId(int loaneeId)
        {
            var loanNos = await DataContext.LnDeviceLoanDisbursements
                .Where(d => d.LoaneeId == loaneeId && d.LoanNo != null)
                .Select(d => d.LoanNo!)
                .ToListAsync();

            return loanNos;
        }


        //public async Task<List<LnDeviceLoanDisbursementViewModel>> GetLoanNoByLoaneeId(int loaneeId)
        //{
        //    var result = from dd in DataContext.LnDeviceLoanDisbursements
        //                 join lender in DataContext.CmnCompanies on dd.LenderId equals lender.Id
        //                 join loanee in DataContext.TFACompanyCustomers on dd.LoaneeId equals loanee.Id
        //                 where dd.LoaneeId == loaneeId
        //                 select new LnDeviceLoanDisbursementViewModel
        //                 {
        //                     LenderId = lender.Id,
        //                     LoaneeId = loanee.Id,
        //                     NumberOfDevice = dd.NumberOfDevice,
        //                     Rate = dd.Rate,
        //                     LoanAmount = dd.LoanAmount,
        //                     Remarks = dd.Remarks,
        //                     LenderName = lender.Name,
        //                     LoaneeName = loanee.Name
        //                 };

        //    return await result.ToListAsync();
        //}


    }
}
