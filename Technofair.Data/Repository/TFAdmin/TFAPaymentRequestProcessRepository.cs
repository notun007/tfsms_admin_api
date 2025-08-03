using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Lib.Utilities;


using Technofair.Model.TFAdmin;
using System.Data.SqlClient;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;



namespace TFSMS.Admin.Data.Repository.TFAdmin
{
    
    #region Interface

    public interface ITFAPaymentRequestProcessRepository : IRepository<TFAPaymentRequestProcess>
    {
        Task<long> AddEntityAsync(TFAPaymentRequestProcess obj);
        long UpdateEntity(TFAPaymentRequestProcess obj);
        //string GetProcessIDByPaymentMethodAndDeviceNumberId(Int16 paymentMethodId, int deviceNumberId);
        string GetProcessID(int deviceNumberId);
    }

    //public interface IRepository<T>
    //{
    //}

    #endregion

    public class TFAPaymentRequestProcessRepository : AdminBaseRepository<TFAPaymentRequestProcess>, ITFAPaymentRequestProcessRepository
    {

        public TFAPaymentRequestProcessRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public async Task<long> AddEntityAsync(TFAPaymentRequestProcess obj)
        {
            long Id = 1;
            if (obj.Id == 0)
            {
                TFAPaymentRequestProcess? last = DataContext.TFAPaymentRequestProcesss.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public long UpdateEntity(TFAPaymentRequestProcess obj)
        {
            DataContext.TFAPaymentRequestProcesss.Update(obj);
            return obj.Id;
        }

        //public string GetProcessIDByPaymentMethodAndDeviceNumberId(Int16 paymentMethodId, int deviceNumberId)
        //{
        //    string processID = "";
        //    try
        //    {
        //        PrdDeviceNumber? objDevice = DataContext.PrdDeviceNumbers.Where(w => w.Id == deviceNumberId).FirstOrDefault();
        //        if (objDevice != null)
        //        {
        //            AnFPaymentRequestProcess? obj = DataContext.AnFPaymentRequestProcesses.Where(w => w.AnFPaymentMethodId == paymentMethodId && w.PrdDeviceNumberId == deviceNumberId).OrderByDescending(o => o.Id).FirstOrDefault();
        //            Int64 serial = 1;
        //            processID = objDevice.Id.ToString() + DateTime.Now.ToString("yyMMdd");
        //            if (obj != null)
        //            {
        //                if (obj.ProcessID != "" && obj.ProcessID != null)
        //                {
        //                    string sub = obj.ProcessID.Substring(processID.Length);
        //                    serial = Convert.ToInt64(sub) + 1;
        //                }
        //            }
        //            processID = processID + serial;
        //        }
        //        return processID;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string GetProcessID(int deviceNumberId)
        {
            string processID = deviceNumberId.ToString();
            try
            {
                string query = " select replace(convert(varchar, getdate(),3),'/','') + replace(convert(varchar, getdate(),14),':','') ";//total 15 digit
                //DataTable dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.Text, query, null).Tables[0];
                DataTable dt = Helper.ExecuteDataReader(DataContext.Database.GetDbConnection().ConnectionString, CommandType.Text, query, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    processID += dt.Rows[0][0] == DBNull.Value ? "" : (dt.Rows[0][0]).ToString();
                }
                return processID;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }


    }

}
