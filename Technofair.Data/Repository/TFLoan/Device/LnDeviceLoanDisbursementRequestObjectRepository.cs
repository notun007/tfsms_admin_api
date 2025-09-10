using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Model.TFLoan.Device;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Model.Accounts;

namespace Technofair.Data.Repository.TFLoan.Device
{
    public interface ILnDeviceLoanDisbursementRequestObjectRepository : IRepository<LnDeviceLoanDisbursementRequestObject>
    {
        Int64 AddEntity(LnDeviceLoanDisbursementRequestObject obj);
        Task<Int64> AddEntityAsync(LnDeviceLoanDisbursementRequestObject obj);
    }
    public class LnDeviceLoanDisbursementRequestObjectRepository : AdminBaseRepository<LnDeviceLoanDisbursementRequestObject>, ILnDeviceLoanDisbursementRequestObjectRepository
    {
        public LnDeviceLoanDisbursementRequestObjectRepository(IAdminDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {

        }

        public Int64 AddEntity(LnDeviceLoanDisbursementRequestObject obj)
        {
            Int64 Id = 1;
            LnDeviceLoanDisbursementRequestObject last = DataContext.LnDeviceLoanDisbursementRequestObjects.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int64> AddEntityAsync(LnDeviceLoanDisbursementRequestObject obj)
        {
            Int64 Id = 1;
            LnDeviceLoanDisbursementRequestObject last = DataContext.LnDeviceLoanDisbursementRequestObjects.OrderByDescending(x => x.Id).FirstOrDefault();
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
