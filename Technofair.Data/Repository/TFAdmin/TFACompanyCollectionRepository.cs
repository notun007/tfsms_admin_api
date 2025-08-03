using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Lib.Utilities;

using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
   
    #region Interface
    public interface ITFACompanyCollectionRepository : IRepository<TFACompanyCollection>
    {
        long AddEntity(TFACompanyCollection obj);
        DataTable GetPackageByClientId(int clientId, string filePath);
        DataTable GetCompanyPaymentByPaymentId(long paymentId, string domain);
        DataTable GetCollectionByDateAndClientId(DateTime dateFrom, DateTime dateTo, int clientId, int? paymentMethodId, string filePath);
        DataTable GetCompanyPaymentByYearAndMonthId(int yearId, Int16 monthId, string domain, int packageId, Int16? status);
        DataTable GetCollectionByPaymentAndClientId(long paymentId, int clientId, string filePath);
        DataTable GetPaymentDetailByYearAndMonthId(int yearId, short monthId, string domain);
    }

    #endregion
    public class TFACompanyCollectionRepository : AdminBaseRepository<TFACompanyCollection>, ITFACompanyCollectionRepository
    {
        public TFACompanyCollectionRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public long AddEntity(TFACompanyCollection obj)
        {
            long Id = 1;
            TFACompanyCollection? last = DataContext.TFACompanyCollections.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public DataTable GetPackageByClientId(int clientId, string filePath)
        {
            string connectionString = Helper.GetCompanyConnectionString(filePath);
            string query = " SELECT TFAClientPackages.*,TFACompanyPackageTypes.Title AS PackageType FROM TFAClientPackages LEFT JOIN ";
            query += " TFACompanyPackages ON TFAClientPackages.TFACompanyPackageId=TFACompanyPackages.Id LEFT JOIN ";
            query += " TFACompanyPackageTypes ON TFACompanyPackages.TFACompanyPackageTypeId=TFACompanyPackageTypes.Id ";
            query += " WHERE CmnClientId=" + clientId;
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
        public DataTable GetCompanyPaymentByPaymentId(long paymentId, string domain)
        {
            string connectionString = Helper.GetCompanyConnectionString(domain);
            string query = " SELECT DISTINCT * FROM TFAClientPayments WHERE Id=" + paymentId;
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

        public DataTable GetPaymentDetailByYearAndMonthId(int yearId, short monthId, string domain)
        {
            string connectionString = Helper.GetCompanyConnectionString(domain);
            string query = " SELECT DISTINCT TFAClientPaymentDetails.* FROM TFAClientPayments INNER JOIN ";
            query += " TFAClientPaymentDetails ON TFAClientPayments.Id=TFAClientPaymentDetails.TFAClientPaymentId  ";
            query += "WHERE TFAClientPayments.IsCancelled=0 AND";
            query += " TFAClientPayments.CmnFinancialYearId=" + yearId + " AND";
            query += " TFAClientPaymentDetails.CmnMonthId=" + monthId;
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

        public DataTable GetCollectionByDateAndClientId(DateTime dateFrom, DateTime dateTo, int clientId, int? paymentMethodId, string filePath)
        {
            string connectionString = Helper.GetCompanyConnectionString(filePath);
            string query = " SELECT TFACompanyCollections.* ,AnFPaymentMethods.Name AS PaymentMethod,CmnCompanyCustomer.Name AS ClientName,CmnCompanyCustomer.Address,CmnCompanyCustomer.ContactNo,CmnCompanyCustomer.Web FROM AnFCompanyCollections INNER JOIN ";
            query += " CmnCompanyCustomer ON AnFCompanyCollections.CmnClientId=CmnCompanyCustomer.Id INNER JOIN ";
            query += " AnFPaymentMethods ON AnFCompanyCollections.AnFPaymentMethodId=AnFPaymentMethods.Id  ";
            query += " WHERE CmnCompanyCustomer.Id=" + clientId + " AND";
            query += " CONVERT (Date,Date,103) BETWEEN CONVERT (Date,'" + dateFrom + "'" + ",103) AND CONVERT (Date,'" + dateTo + "'" + ",103) ";
            if (paymentMethodId != null && paymentMethodId > 0)
            {
                query += " AND AnFCompanyCollections.AnFPaymentMethodId=" + paymentMethodId;
            }
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

        public DataTable GetCompanyPaymentByYearAndMonthId(int yearId, Int16 monthId, string domain, int packageId, Int16? status)
        {
            string connectionString = Helper.GetCompanyConnectionString(domain);
            string query = " SELECT DISTINCT AnFClientPayments.* FROM AnFClientPayments INNER JOIN ";
            query += " AnFClientPaymentDetails ON AnFClientPayments.Id=AnFClientPaymentDetails.AnFClientPaymentId  ";
            query += " WHERE AnFClientPackageId=" + packageId + " AND";
            query += " AnFClientPayments.IsCancelled=0 AND";
            query += " AnFClientPayments.CmnFinancialYearId=" + yearId + " AND";
            query += " AnFClientPaymentDetails.CmnMonthId=" + monthId;
            if (status != null && status == 4)//isCollected = true;
            {
                query += " AND AnFClientPayments.IsCollected=1";
            }
            else if (status != null && status == 3)//Paid = true;
            {
                query += " AND AnFClientPayments.IsCollected=0";
                query += " AND ISNULL(AnFClientPayments.AnFPaymentMethodId,0)>0";
            }
            else if (status != null && status == 2)//Unpaid = true;
            {
                query += " AND AnFClientPayments.IsCancelled=0";
                query += " AND AnFClientPayments.IsCollected=0";
                query += " AND ISNULL(AnFClientPayments.AnFPaymentMethodId,0)=0";
            }
            else if (status != null && status == 1)//Not Generate = true;
            {
                query += " AND AnFClientPayments.IsCancelled=0";
                query += " AND AnFClientPayments.IsCollected=0";
                query += " AND ISNULL(AnFClientPayments.AnFPaymentMethodId,0)=0";
            }

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

        public DataTable GetCollectionByPaymentAndClientId(long paymentId, int clientId, string filePath)
        {
            string connectionString = Helper.GetCompanyConnectionString(filePath);
            string query = " SELECT AnFCompanyCollections.* ,CmnCompanyCustomer.Name AS ClientName,CmnCompanyCustomer.Address,CmnCompanyCustomer.ContactNo,CmnCompanyCustomer.Web FROM AnFCompanyCollections INNER JOIN ";
            query += " CmnCompanyCustomer ON AnFCompanyCollections.CmnClientId=CmnCompanyCustomer.Id  ";
            query += " WHERE CmnCompanyCustomer.Id=" + clientId + " AND";
            query += "  AnFCompanyCollections.AnFClientPaymentId=" + paymentId;
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


    }

}
