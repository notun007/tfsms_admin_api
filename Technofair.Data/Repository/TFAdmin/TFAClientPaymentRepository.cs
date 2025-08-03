using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Model.TFAdmin;

using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Data;
using Technofair.Lib.Utilities;
using Technofair.Model.ViewModel.TFAdmin;
using System.Data.SqlClient;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
    #region Interface
    public interface ITFAClientPaymentRepository : IRepository<TFAClientPayment>
    {
        long AddEntity(TFAClientPayment obj);
        string GetRefNo(int clientId);
        List<TFAClientPaymentInvoiceViewModel> GetClientPaymentInvoice(int TFACompanyCustomerId, int TFAClientPaymentInvoiceId);

    }

    #endregion
    public class TFAClientPaymentRepository : AdminBaseRepository<TFAClientPayment>, ITFAClientPaymentRepository
    {
        public TFAClientPaymentRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public long AddEntity(TFAClientPayment obj)
        {
            long Id = 1;
            TFAClientPayment? last = DataContext.TFAClientPayments.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public string GetRefNo(int clientId)
        {
            TFACompanyCustomer? objClient = DataContext.TFACompanyCustomers.Where(w => w.Id == clientId).FirstOrDefault();
            Int64 serail = 1;
            TFAClientPayment? obj = DataContext.TFAClientPayments.OrderByDescending(x => x.Id).FirstOrDefault();
            string refNo = "TF-INV-";
            if (objClient != null && objClient.Code != null && objClient.Code != "")
            {
                refNo += objClient.Code + "-";
            }
            //refNo += DateTime.Now.ToString("MMyy") + "-";
            if (obj != null && obj.RefNo != null && obj.RefNo != "")
            {
                string sub = obj.RefNo.Substring(refNo.Length);
                serail = Convert.ToInt64(sub) + 1;
            }
            refNo = refNo + serail;
            return refNo;
        }

        public List<TFAClientPaymentInvoiceViewModel> GetClientPaymentInvoice(int TFACompanyCustomerId,int TFAClientPaymentInvoiceId) //New
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@TFACompanyCustomerId", TFACompanyCustomerId);
            paramsToStore[1] = new SqlParameter("@ClientPaymentInvoiceId", TFAClientPaymentInvoiceId);
            List<TFAClientPaymentInvoiceViewModel> list = new List<TFAClientPaymentInvoiceViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAClientBill.GetClientPaymentInvoice, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((TFAClientPaymentInvoiceViewModel)Helper.FillTo(row, typeof(TFAClientPaymentInvoiceViewModel))));
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
