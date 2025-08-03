using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.TFAdmin
{   
    public interface ITFAClientServerInfoRepository : IRepository<TFAClientServerInfo>
    {
        int AddEntity(TFAClientServerInfo objClientServerInfo);
        List<TFAClientServerInfo> GetAllCmnClientServerInfo();
        //Task<TFAClientServerInfo> GetStoredClientServerInfoByAppKey(string appKey);

    }
    public class TFAClientServerInfoRepository : AdminBaseRepository<TFAClientServerInfo>, ITFAClientServerInfoRepository
    {

        public TFAClientServerInfoRepository(IAdminDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {

        }

        public int AddEntity(TFAClientServerInfo objClientServerInfo)
        {
            int Id = 1;
            TFAClientServerInfo? last = DataContext.TFAClientServerInfos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objClientServerInfo.Id = Id;
            base.Add(objClientServerInfo);
            return Id;
        }

        public List<TFAClientServerInfo> GetAllCmnClientServerInfo()
        {
            List<TFAClientServerInfo> list = (from p in DataContext.TFAClientServerInfos
                                              join b in DataContext.TFACompanyCustomers on p.TFACompanyCustomerId equals b.Id
                                              select p).ToList();
            return list;
        }


        //public async Task<TFAClientServerInfo> GetStoredClientServerInfoByAppKey(string appKey)
        //{
        //    return await DataContext.TFAClientServerInfos.Where(x => x.TFACompanyCustomerId == 1).SingleOrDefaultAsync();
        //}

        //public async Task<TFAClientServerInfo> GetClientServerInfoByCustomerCompanyId(int Id)
        //    return await DataContext.TFAClientServerInfos.Where(x => x.TFACompanyCustomerId == Id).SingleOrDefaultAsync();
        //}

        //public async Task<bool> IsValidServer(TFAClientServerInfo obj)
        //{
        //    List<TFAClientServerInfo> objTFAClientServerInfo = new List<TFAClientServerInfo>();
        //    if (obj != null)
        //    {
        //        objTFAClientServerInfo = await DataContext.TFAClientServerInfos.Where(x => 
        //                                                     x.ServerIP == obj.ServerIP 
        //                                                  && x.MotherBoardId == obj.MotherBoardId
        //                                                  && x.NetworkAdapterId == obj.NetworkAdapterId)
        //                                                      .ToListAsync();
        //    }

        //    if(objTFAClientServerInfo.Count>0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //    return false; 
        //    }
        //}



    }
}
