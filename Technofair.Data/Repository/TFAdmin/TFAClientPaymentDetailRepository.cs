using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Lib.Utilities;

using TFSMS.Admin.Model.TFAdmin;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using Technofair.Model.ViewModel.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
    
    #region Interface
    public interface ITFAClientPaymentDetailRepository : IRepository<TFAClientPaymentDetail>
    {
        long AddEntity(TFAClientPaymentDetail obj);
        TFAClientPaymentDetail GetByYearAndMonthId(int yearId, short monthId);
        int InsertPaymentDetail(TFAClientPaymentDetail obj, string domain);
        int UpdatePaymentDetail(TFAClientPaymentDetail obj, string domain);
        Task<TFAClientPaymentDetail> GetClientPaymentDetailByAppKey(string appKey);
        int DeleteByIdAndDomain(long Id, string domain);
        DataTable GetDetailByYearAndMonthId(int yearId, Int16 monthId, string domain);
        DataTable GetDetailByPaymentIdAndDomain(long paymentId, string domain);
        Task<TFAClientPaymentDetail> clientPaymentDetails(int companyCustomerId, int monthId, int year);
        CompanyCustomerWithClientPackageViewModel GetClientBillByClientPaymentDetailId(int tfaCompanyCustomerId, Int64 tfaClientPaymentDetailId);
        ClientPaymentViewModel GetClientPackageExpireDate(string appKey);
        TFAClientPaymentDetail GetByClientPaymentDetailId(long clientPaymentDetailId);
    }

    #endregion
    public class TFAClientPaymentDetailRepository : AdminBaseRepository<TFAClientPaymentDetail>, ITFAClientPaymentDetailRepository
    {
        public TFAClientPaymentDetailRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public long AddEntity(TFAClientPaymentDetail obj)
        {
            long Id = 1;
            if (obj.Id == 0)
            {
                TFAClientPaymentDetail? last = DataContext.TFAClientPaymentDetails.OrderByDescending(x => x.Id).FirstOrDefault();
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
            base.Add(obj);
            return Id;
        }

        public TFAClientPaymentDetail GetByClientPaymentDetailId(long clientPaymentDetailId)
        {

            var objPaymentDetail = DataContext.TFAClientPaymentDetails
                                .Where(a=> a.Id == clientPaymentDetailId)
                                .Select(a => new TFAClientPaymentDetail
                                {                                    
                                    Id = a.Id,
                                    TFAClientPaymentId = a.TFAClientPaymentId,
                                    Year = a.Year,
                                    TFAMonthId = a.TFAMonthId,
                                    AnFCompanyServiceTypeId = a.AnFCompanyServiceTypeId,
                                    NumberOfAssignedDevice = a.NumberOfAssignedDevice,
                                    NumberOfLivePackage = a.NumberOfLivePackage,
                                    Rate = a.Rate,
                                    Discount = a.Discount,
                                    Amount = a.Amount,
                                    TotalAmount = a.TotalAmount,
                                    ExpireDate = a.ExpireDate,
                                    IsApproved = a.IsApproved,
                                    ApproveBy = a.ApproveBy,
                                    ApproveDate = a.ApproveDate,
                                    CreatedBy = a.CreatedBy,
                                    CreatedDate = a.CreatedDate,
                                    ModifiedBy = a.ModifiedBy,
                                    ModifiedDate = a.ModifiedDate
                                })
                                .FirstOrDefault();

            return objPaymentDetail;
        }

        public TFAClientPaymentDetail GetByYearAndMonthId(int yearId, short monthId)
        {
            TFAClientPaymentDetail? obj = (from m in DataContext.TFAClientPayments
                                           join d in DataContext.TFAClientPaymentDetails on m.Id equals d.TFAClientPaymentId
                                           where (d.TFAMonthId == monthId && m.TFAFinancialYearId == yearId && d.IsCancelled == false)
                                           select d).FirstOrDefault();
            return obj;
        }
        public async Task<TFAClientPaymentDetail> clientPaymentDetails( int companyCustomerId,int monthId,int year)
        {
            var clientPaymentDetail = await (from cp in DataContext.TFAClientPayments
                        join cpd in DataContext.TFAClientPaymentDetails
                        on cp.Id equals cpd.TFAClientPaymentId
                        where cp.TFACompanyCustomerId == companyCustomerId
                        && cpd.TFAMonthId == monthId
                        && cpd.Year == year
                        select new TFAClientPaymentDetail
                        {
                            Id = cpd.Id,
                            TFAClientPaymentId = cpd.TFAClientPaymentId,
                            Year = cpd.Year,
                            TFAMonthId = cpd.TFAMonthId,
                            AnFCompanyServiceTypeId = cpd.AnFCompanyServiceTypeId,
                            NumberOfAssignedDevice = cpd.NumberOfAssignedDevice,
                            NumberOfLivePackage = cpd.NumberOfLivePackage,
                            Rate = cpd.Rate,
                            Discount = cpd.Discount,
                            Amount = cpd.Amount,
                            ExpireDate = cpd.ExpireDate,
                            CreatedBy = cpd.CreatedBy,
                            CreatedDate = cpd.CreatedDate

                        }).FirstOrDefaultAsync();

            return clientPaymentDetail;
        }
        public int InsertPaymentDetail(TFAClientPaymentDetail obj, string domain)
        {
            int ret = 0;
            string connectionString = Helper.GetCompanyConnectionString(domain);
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[7];
                paramsToStore[0] = new SqlParameter("@AnFClientPaymentId", obj.TFAClientPaymentId);
                paramsToStore[1] = new SqlParameter("@TFAMonthId", obj.TFAMonthId);
                paramsToStore[2] = new SqlParameter("@AnFCompanyServiceTypeId", obj.AnFCompanyServiceTypeId);
                paramsToStore[3] = new SqlParameter("@Quantity", obj.NumberOfAssignedDevice);
                paramsToStore[4] = new SqlParameter("@Rate", obj.Rate);
                paramsToStore[5] = new SqlParameter("@Discount", obj.Discount);
                paramsToStore[6] = new SqlParameter("@Amount", obj.Amount);

                //ret = Helper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, SPList.AnFClientPayment.InsertAnFClientPaymentDetails, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            return ret;
        }
        public int UpdatePaymentDetail(TFAClientPaymentDetail obj, string domain)
        {
            int ret = 0;
            string connectionString = Helper.GetCompanyConnectionString(domain);
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[6];
                paramsToStore[0] = new SqlParameter("@Id", obj.Id);
                paramsToStore[1] = new SqlParameter("@AnFCompanyServiceTypeId", obj.AnFCompanyServiceTypeId);
                paramsToStore[2] = new SqlParameter("@Quantity", obj.NumberOfAssignedDevice);
                paramsToStore[3] = new SqlParameter("@Rate", obj.Rate);
                paramsToStore[4] = new SqlParameter("@Discount", obj.Discount);
                paramsToStore[5] = new SqlParameter("@Amount", obj.Amount);
                //ret = Helper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, SPList.AnFClientPayment.UpdateAnFClientPaymentDetails, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            return ret;
        }

        public async Task<TFAClientPaymentDetail> GetClientPaymentDetailByAppKey(string appKey)
        {
            TFAClientPaymentDetail? objClientPaymentDetail = null;
            try
            {
             objClientPaymentDetail = await (from cp in DataContext.TFAClientPayments
                                    join cpd in DataContext.TFAClientPaymentDetails
                                    on cp.Id equals cpd.TFAClientPaymentId
                                    join cc in DataContext.TFACompanyCustomers
                                    on cp.TFACompanyCustomerId equals cc.Id
                                    where cc.AppKey == appKey
                                          && cpd.IsPaid == true
                                          && cpd.IsCancelled == false
                                          && cc.IsActive == true
                                    orderby cpd.ExpireDate descending
                                             select new TFAClientPaymentDetail
                                             {
                                                 Id = cpd.Id,
                                                 TFAClientPaymentId = cpd.TFAClientPaymentId,
                                                 Year = cpd.Year,
                                                 TFAMonthId = cpd.TFAMonthId,
                                                 AnFCompanyServiceTypeId = cpd.AnFCompanyServiceTypeId,
                                                 NumberOfAssignedDevice = cpd.NumberOfAssignedDevice,
                                                 NumberOfLivePackage = cpd.NumberOfLivePackage,
                                                 Rate = cpd.Rate,
                                                 Discount = cpd.Discount,
                                                 Amount = cpd.Amount,
                                                 ExpireDate = cpd.ExpireDate,
                                                 CreatedBy = cpd.CreatedBy,
                                                 CreatedDate = cpd.CreatedDate

                                             })
                          .Take(1)
                          .FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {

            }

            return objClientPaymentDetail;
        }

        public DataTable GetDetailByYearAndMonthId(int yearId, Int16 monthId, string domain)
        {
            string connectionString = Helper.GetCompanyConnectionString(domain);
            string query = " SELECT AnFClientPaymentDetails.* FROM AnFClientPayments INNER JOIN ";
            query += " AnFClientPaymentDetails ON AnFClientPayments.Id=AnFClientPaymentDetails.AnFClientPaymentId  ";
            query += " WHERE AnFClientPayments.CmnFinancialYearId=" + yearId + " AND";
            query += " AnFClientPaymentDetails.CmnMonthId=" + monthId + " AND";
            query += " AnFClientPayments.IsCancelled=0";
            DataTable dt = new DataTable();
            try
            {
                dt = Helper.ExecuteDataset(connectionString, CommandType.Text, query, null).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
        public DataTable GetDetailByPaymentIdAndDomain(long paymentId, string domain)
        {
            string connectionString = Helper.GetCompanyConnectionString(domain);
            string query = " SELECT * FROM AnFClientPaymentDetails";
            query += " WHERE AnFClientPaymentId=" + paymentId;
            DataTable dt = new DataTable();
            try
            {
                dt = Helper.ExecuteDataset(connectionString, CommandType.Text, query, null).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }

        public int DeleteByIdAndDomain(long Id, string domain)
        {
            int ret = 0;
            string connectionString = Helper.GetCompanyConnectionString(domain);
            string query = " DELETE FROM AnFClientPaymentDetails";
            query += " WHERE Id=" + Id;
            DataTable dt = new DataTable();
            try
            {
                ret = Helper.ExecuteNonQuery(connectionString, CommandType.Text, query, null);
                return ret;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }

        public CompanyCustomerWithClientPackageViewModel GetClientBillByClientPaymentDetailId(int tfaCompanyCustomerId, Int64 tfaClientPaymentDetailId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[2];
            
            paramsToStore[0] = new SqlParameter("@tfaCompanyCustomerId", tfaCompanyCustomerId);
            paramsToStore[1] = new SqlParameter("@tfaClientPaymentDetailId", tfaClientPaymentDetailId);


            List<CompanyCustomerWithClientPackageViewModel> list = new List<CompanyCustomerWithClientPackageViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAdmin.GetClientBillByClientPaymentDetailId, paramsToStore).Tables[0];

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
            return list.FirstOrDefault();
        }

        public ClientPaymentViewModel GetClientPackageExpireDate(string appKey)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];

            paramsToStore[0] = new SqlParameter("@appKey", appKey);
           


            List<ClientPaymentViewModel> list = new List<ClientPaymentViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAdmin.GetClientPackageExpireDate, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((ClientPaymentViewModel)Helper.FillTo(row, typeof(ClientPaymentViewModel))));
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return list.FirstOrDefault();
        }
    }

}
