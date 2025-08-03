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
using Technofair.Model.TFLoan.Device;

//using Technofair.Model.Loan.Device;

//using Technofair.Model.ViewModel.Loan.Device;
using Technofair.Model.ViewModel.Subscription;
using Technofair.Model.ViewModel.TFLoan;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFLoan.Device
{
    public interface ILnDeviceLoanCollectionRepository : IRepository<LnDeviceLoanCollection>
    {
        Int64 AddEntity(LnDeviceLoanCollection obj);
        Task<Int64> AddEntityAsync(LnDeviceLoanCollection obj);
        Task<List<LnDeviceLoanCollectionViewModel>> GetLoanCollection(LnDeviceLoanCollectionViewModel obj);
        Task AddDeviceLoanCollectionAsync(LnDeviceLoanCollection obj);
        Task AddRangeDeviceLoanCollectionAsync(List<LnDeviceLoanCollection> objList);
        DeviceLoanInfoViewModel GetDeviceLoanInfo(int lenderId, int loaneeId);
        DeviceLoanInfoViewModel GetDeviceLoanInfoByAppKey(string appKey);

    }
    public class LnDeviceLoanCollectionRepository : AdminBaseRepository<LnDeviceLoanCollection>, ILnDeviceLoanCollectionRepository
    {
        public LnDeviceLoanCollectionRepository(IAdminDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {

        }

        public Int64 AddEntity(LnDeviceLoanCollection obj)
        {
            Int64 Id = 1;
            LnDeviceLoanCollection last = DataContext.LnDeviceLoanCollections.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int64> AddEntityAsync(LnDeviceLoanCollection obj)
        {
            Int64 Id = 1;
            LnDeviceLoanCollection last = DataContext.LnDeviceLoanCollections.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {                
                Id = last.Id + 1;
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }

        public async Task AddDeviceLoanCollectionAsync(LnDeviceLoanCollection obj)
        {
            try{
                await DataContext.AddAsync(obj);
                await DataContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            
        }

        public async Task AddRangeDeviceLoanCollectionAsync(List<LnDeviceLoanCollection> objList)
        {
            await DataContext.AddRangeAsync(objList);  
        }

        public DeviceLoanInfoViewModel GetDeviceLoanInfo(int lenderId, int loaneeId)
        {
            List<DeviceLoanInfoViewModel> list = new List<DeviceLoanInfoViewModel>();

            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] param = new SqlParameter[2];

                param[0] = new SqlParameter("@lenderId", lenderId);
                param[1] = new SqlParameter("@loaneeId", loaneeId);
                
                //New
                dt = Helper.ExecuteDataReader(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.PackageSP.GetDeviceLoanInfo, param);

                //Old
                //dt = Helper.ExecuteDataReader(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.PackageSP.GetDeviceLoanInfoByLoaneeId, param);

                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<DeviceLoanInfoViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((DeviceLoanInfoViewModel)Helper.FillTo(row, typeof(DeviceLoanInfoViewModel)));
                    }
                }
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DeviceLoanInfoViewModel GetDeviceLoanInfoByAppKey(string appKey)
        {
            List<DeviceLoanInfoViewModel> list = new List<DeviceLoanInfoViewModel>();

            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter("@appKey", appKey);

                dt = Helper.ExecuteDataReader(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.PackageSP.GetDeviceLoanInfoByAppKey, param);
                               
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<DeviceLoanInfoViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((DeviceLoanInfoViewModel)Helper.FillTo(row, typeof(DeviceLoanInfoViewModel)));
                    }
                }
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LnDeviceLoanCollectionViewModel>> GetLoanCollection(LnDeviceLoanCollectionViewModel obj)
        {
            List<LnDeviceLoanCollectionViewModel> list = new List<LnDeviceLoanCollectionViewModel>();

            try
            {

                DataTable dt = new DataTable();

                SqlParameter[] paramsToStore = new SqlParameter[0];

                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SubscriberSP.GetLoanCollection, paramsToStore).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((LnDeviceLoanCollectionViewModel)Helper.FillTo(row, typeof(LnDeviceLoanCollectionViewModel))));
                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
    }
}
