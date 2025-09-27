using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TFSMS.Admin.Model.TFAdmin;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TFSMS.Admin.Model.Common;
using System.Data;
using System.Collections;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using static System.Net.Mime.MediaTypeNames;
using Technofair.Model.ViewModel.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{

    #region Interface

    public interface ITFACompanyCustomerRepository : IRepository<TFACompanyCustomer>
    {
        Task<int> AddEntityAsync(TFACompanyCustomer obj);
        Task<TFACompanyCustomer> GetCompanyCustomerByAppKey(string appKey);
        Task<TFACompanyCustomer> GetCompanyCustomerByLoaneeCode(string loaneeCode);
        Task<List<TFACompanyCustomer>> GetCompanyCustomerExceptItseltByEmail(TFACompanyCustomerViewModel obj);
        string GetLastCode();
        List<CompanyCustomerWithClientPackageViewModel> GetActiveCompanyCustomerWithClientPackage(int monthId, int yearId);
    }

    #endregion

    public class TFACompanyCustomerRepository : AdminBaseRepository<TFACompanyCustomer>, ITFACompanyCustomerRepository
    {
        public TFACompanyCustomerRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public async Task<int> AddEntityAsync(TFACompanyCustomer obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                TFACompanyCustomer? last = DataContext.TFACompanyCustomers.OrderByDescending(x => x.Id).FirstOrDefault();
                if (last != null)
                {
                    Id = last.Id + 1;
                }
                obj.Id = Id;
            }
            else
            {
                Id = obj.Id;
            }
            await base.AddAsync(obj);
            return Id;
        }

        public async Task<TFACompanyCustomer> GetCompanyCustomerByAppKey(string appKey)
        {
            return await DataContext.TFACompanyCustomers.Where(x => x.AppKey == appKey).SingleOrDefaultAsync();
        }

        public async Task<TFACompanyCustomer> GetCompanyCustomerByLoaneeCode(string loaneeCode)
        {
            return await DataContext.TFACompanyCustomers.Where(x => x.Code == loaneeCode).SingleOrDefaultAsync();
        }

        public async Task<List<TFACompanyCustomer>> GetCompanyCustomerExceptItseltByEmail(TFACompanyCustomerViewModel obj)
        {
            return await DataContext.TFACompanyCustomers.Where(x => x.Email == obj.Email && x.Id != obj.Id).ToListAsync();
        }
        public string GetLastCode()
        {
            //MSO-10001
 
            TFACompanyCustomer? obj = DataContext.TFACompanyCustomers.OrderByDescending(o => o.Id).FirstOrDefault();
           
            string initial = "10001"; 
            string prefix = "MSO";
            string nextCode = string.Empty;
                       
            if (obj == null)
            {
                nextCode = prefix + "-" + initial;
            }

            if (obj != null)
            {
                int startIndex = 4;
                int length = 5;

                string maxCode = "MSO-10001".Substring(startIndex, length);
                Console.WriteLine(maxCode);

                nextCode = prefix + "-" + (Convert.ToInt32(maxCode) + 1).ToString();
            }
          
            return nextCode;
        }

        //public DataTable GetActiveCompanyCustomerWithClientPackage(int monthId, int yearId)
        //{
        //    DataTable dt = new DataTable();

        //    SqlParameter[] paramsToStore = new SqlParameter[2];
        //    paramsToStore[0] = new SqlParameter("@monthId", monthId);
        //    paramsToStore[1] = new SqlParameter("@year", yearId);

        //    try
        //    {
        //        dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAdmin.GetActiveCompanyCustomerWithClientPackage, paramsToStore).Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return dt;
        //}

        public List<CompanyCustomerWithClientPackageViewModel> GetActiveCompanyCustomerWithClientPackage(int monthId, int yearId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@monthId", monthId);
            paramsToStore[1] = new SqlParameter("@year", yearId);

            List<CompanyCustomerWithClientPackageViewModel> list = new List<CompanyCustomerWithClientPackageViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAdmin.GetActiveCompanyCustomerWithClientPackage, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((CompanyCustomerWithClientPackageViewModel)Helper.FillTo(row, typeof(CompanyCustomerWithClientPackageViewModel))));
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return list;
        }
    }

}
