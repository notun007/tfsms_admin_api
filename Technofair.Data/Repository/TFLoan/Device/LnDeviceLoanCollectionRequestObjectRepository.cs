using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using Technofair.Model.TFLoan.Device;

namespace Technofair.Data.Repository.TFLoan.Device
{
    
    public interface ILnDeviceLoanCollectionRequestObjectRepository : IRepository<LnDeviceLoanCollectionRequestObject>
    {
        Int64 AddEntity(LnDeviceLoanCollectionRequestObject obj);
        Task<Int64> AddEntityAsync(LnDeviceLoanCollectionRequestObject obj);
    }
    public class LnDeviceLoanCollectionRequestObjectRepository : AdminBaseRepository<LnDeviceLoanCollectionRequestObject>, ILnDeviceLoanCollectionRequestObjectRepository
    {
        public LnDeviceLoanCollectionRequestObjectRepository(IAdminDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {

        }

        public Int64 AddEntity(LnDeviceLoanCollectionRequestObject obj)
        {
            Int64 Id = 1;
            LnDeviceLoanCollectionRequestObject last = DataContext.LnDeviceLoanCollectionRequestObjects.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int64> AddEntityAsync(LnDeviceLoanCollectionRequestObject obj)
        {
            Int64 Id = 1;
            LnDeviceLoanCollectionRequestObject last = DataContext.LnDeviceLoanCollectionRequestObjects.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }
    }

}
