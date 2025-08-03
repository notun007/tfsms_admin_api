using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace Technofair.Data.Repository.TFLoan.Device
{ 
    public interface IAnFPaymentChannelRepository : IRepository<AnFPaymentChannel>
    {
        Int16 AddEntity(AnFPaymentChannel obj);
    }
    public class AnFPaymentChannelRepository : AdminBaseRepository<AnFPaymentChannel>, IAnFPaymentChannelRepository
    {
        public AnFPaymentChannelRepository(IAdminDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {

        }

        public Int16 AddEntity(AnFPaymentChannel obj)
        {
            Int16 Id = 1;
            AnFPaymentChannel last = DataContext.AnFPaymentChannels.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }
    }

}
