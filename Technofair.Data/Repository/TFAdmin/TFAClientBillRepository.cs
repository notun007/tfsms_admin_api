using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using Technofair.Model.TFAdmin;
using Technofair.Model.ViewModel.Common;
using Technofair.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
    
    public interface ITFAClientBillRepository : IRepository<TFAClientPaymentDetail>
    {
        Operation GenerateClientBill(int billGenPermssionId, int? companyCustomerId, int createdBy);
        //Lib.Model.Operation GenerateClientBill(int? companyCustomerId, int? year, int? monthId, int? createdBy);
        List<TFAClientInvoiceViewModel> GetClientInvoice(int? TFACompanyCustomerId);
        List<TFAClientInvoiceViewModel> GetClientApprovedUnpaidBill(int? companyCustomerId);
        List<TFAClientInvoiceViewModel> GetClientApprovedBill(int? companyCustomerId);
        
        Operation ApproveClientBill(int tfaClientPaymentDetailId, int approveBy);

    }
    public class TFAClientBillRepository : AdminBaseRepository<TFAClientPaymentDetail>, ITFAClientBillRepository
    {
        public TFAClientBillRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        //public List<DataMigrationViewModel> GetMigrationStatistics() //New
        //{


        //    DataTable dt = new DataTable();
        //    SqlParameter[] paramsToStore = new SqlParameter[0];
        //    //   paramsToStore[0] = new SqlParameter("@companyId", CompanyId);


        //    List<DataMigrationViewModel> list = new List<DataMigrationViewModel>();

        //    try
        //    {
        //        dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Migration.FetchMigrationStatistics, paramsToStore).Tables[0];

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                list.Add(((DataMigrationViewModel)Helper.FillTo(row, typeof(DataMigrationViewModel))));
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return list;
        //}
        public Operation GenerateClientBill(int billGenPermssionId, int? companyCustomerId, int createdBy)
        //public Lib.Model.Operation GenerateClientBill(int? companyCustomerId, int? year, int? monthId, int? createdBy) //New
        {

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[3];
            
            paramsToStore[0] = new SqlParameter("@billGenPermssionId", billGenPermssionId);
            paramsToStore[1] = new SqlParameter("@companyCustomerId", companyCustomerId);
            //paramsToStore[1] = new SqlParameter("@year", year);
            //paramsToStore[2] = new SqlParameter("@monthId", monthId);
            paramsToStore[2] = new SqlParameter("@createdBy", createdBy);

            Operation objOperation = new Operation();
            List<Operation> list = new List<Operation>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAClientBill.GenerateClientInvoice, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((Operation)Helper.FillTo(row, typeof(Operation))));
                    }
                }
                objOperation = list.FirstOrDefault();

            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Failed To Generate Invoice.";
            }


            return objOperation;
        }


        public List<TFAClientInvoiceViewModel> GetClientInvoice(int? TFACompanyCustomerId) //New
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
               paramsToStore[0] = new SqlParameter("@TFACompanyCustomerId", TFACompanyCustomerId);

            List<TFAClientInvoiceViewModel> list = new List<TFAClientInvoiceViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAClientBill.GetClientInvoice, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((TFAClientInvoiceViewModel)Helper.FillTo(row, typeof(TFAClientInvoiceViewModel))));
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return list;
        }

        public List<TFAClientInvoiceViewModel> GetClientApprovedUnpaidBill(int? companyCustomerId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@companyCustomerId", companyCustomerId);

            List<TFAClientInvoiceViewModel> list = new List<TFAClientInvoiceViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAClientBill.GetClientApprovedUnpaidBill, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((TFAClientInvoiceViewModel)Helper.FillTo(row, typeof(TFAClientInvoiceViewModel))));
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return list;
        }

        
        public List<TFAClientInvoiceViewModel> GetClientApprovedBill(int? companyCustomerId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@companyCustomerId", companyCustomerId);

            List<TFAClientInvoiceViewModel> list = new List<TFAClientInvoiceViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAClientBill.GetClientApprovedBill, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((TFAClientInvoiceViewModel)Helper.FillTo(row, typeof(TFAClientInvoiceViewModel))));
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return list;
        }
        public Operation ApproveClientBill(int tfaClientPaymentDetailId,int approveBy) //New
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@tfaClientPaymentDetailId", tfaClientPaymentDetailId);
            paramsToStore[1] = new SqlParameter("@approveBy", approveBy);
            Operation obj = new Operation();

            List<Operation> list = new List<Operation>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAClientBill.ApproveClientInvoice, paramsToStore).Tables[0];


                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((Operation)Helper.FillTo(row, typeof(Operation))));
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Success = false;
                obj.Message = "Unable tp Approve";
                
            }
            return list.FirstOrDefault();
        }

    }
}
