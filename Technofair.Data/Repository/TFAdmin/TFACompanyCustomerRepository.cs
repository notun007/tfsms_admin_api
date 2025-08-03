using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Model.TFAdmin;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Technofair.Model.Common;
using System.Data;
using System.Collections;
using Technofair.Lib.Utilities;
using Technofair.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{

    #region Interface

    public interface ITFACompanyCustomerRepository : IRepository<TFACompanyCustomer>
    {
        Task<int> AddEntityAsync(TFACompanyCustomer obj);
        Task<TFACompanyCustomer> GetCompanyCustomerByAppKey(string appKey);
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

        public string GetLastCode()
        {
            //string refNo = objType.ShortName + "-" + objType.SerialNo;
            string refNo = "";
            TFACompanyCustomer? obj = DataContext.TFACompanyCustomers.OrderByDescending(o => o.Id).FirstOrDefault();//.Where(w=>w.Code.Contains(refNo))
            //int serial = 1;            
            if (obj != null && obj.Code != null && obj.Code != "")
            {
                //string sub = obj.Code.ToString().Substring(refNo.Length);
                //serial = Convert.ToInt32(sub) + 1;
                //if (serial.ToString().Length == 1)
                //{
                //    refNo += "000";
                //}
                //else if (serial.ToString().Length == 2)
                //{
                //    refNo += "00";
                //}
                //else if (serial.ToString().Length == 3)
                //{
                //    refNo += "0";
                //}
                //refNo = refNo + serial;
                refNo = Convert.ToInt32(obj.Code).ToString();
            }
            else
            {
                //refNo = refNo + "000" + serial;
                refNo = refNo + "1001";
            }
            return refNo;
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
