using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Technofair.Data.Infrastructure;
using Technofair.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;

namespace Technofair.Data.Repository.Accounts
{
    public interface IAnFPaymentChannelRepository : IRepository<AnFPaymentChannel>
    {
        Int16 AddEntity(AnFPaymentChannel obj);
    }
    public class AnFPaymentChannelRepository : BaseRepository<AnFPaymentChannel>, IAnFPaymentChannelRepository
    {
        public AnFPaymentChannelRepository(IDatabaseFactory databaseFactory)
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
