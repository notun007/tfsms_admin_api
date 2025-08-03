using Microsoft.EntityFrameworkCore;
using Technofair.Model.Accounts;

using Technofair.Model.ViewModel.Accounts;
using Technofair.Utiity.Security;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.Accounts
{
    #region Interface

    public interface IAnFPaymentMethodRepository : IRepository<AnFPaymentMethod>
    {
        Int16 AddEntity(AnFPaymentMethod obj);
        Task<List<AnFPaymentMethodViewModel>> GetList();
        AnFPaymentMethod? GetPaymentMethodByName(string name);
        Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId);
        Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId, Int16 anFPaymentChannelId);
    }

    #endregion

    public class AnFPaymentMethodRepository : AdminBaseRepository<AnFPaymentMethod>, IAnFPaymentMethodRepository
    {

        public AnFPaymentMethodRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public Int16 AddEntity(AnFPaymentMethod obj)
        {
            Int16 Id = 1;
            AnFPaymentMethod last = DataContext.AnFPaymentMethods.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id =Convert.ToInt16( last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

       public async Task<List<AnFPaymentMethodViewModel>> GetList()
        {
            var obj = await (from pm in DataContext.AnFPaymentMethods
            join fsp in DataContext.AnFFinancialServiceProviders
            on pm.AnFFinancialServiceProviderId equals fsp.Id into fspGroup
            from fsp in fspGroup.DefaultIfEmpty() 
            join pc in DataContext.AnFPaymentChannels
            on pm.AnFPaymentChannelId equals pc.Id into pcGroup
            from pc in pcGroup.DefaultIfEmpty() 
            select new AnFPaymentMethodViewModel
            {
                Id = pm.Id,
                Name = pm.Name,
                UserId = pm.UserId,
                Password = AES.GetPlainText(pm.Password),
                IsActive = pm.IsActive,
                AnFFinancialServiceProviderId = pm.AnFFinancialServiceProviderId,
                AnFPaymentChannelId = pm.AnFPaymentChannelId,
                FinancialServiceProvider = fsp != null ? fsp.Name : null,
                PaymentChannel = pc != null ? pc.Name : null,
                CreatedBy = pm.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifiedBy = pm.ModifiedBy,
                ModifiedDate = DateTime.Now
            }).ToListAsync();

            return obj;
        }

        public AnFPaymentMethod? GetPaymentMethodByName(string name)
        {
            AnFPaymentMethod? anFPaymentMethod = DataContext.AnFPaymentMethods.Where(x => x.Name == name).FirstOrDefault();

            return anFPaymentMethod;
        }

        public async Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId)
        {
            AnFPaymentMethod objAnFPaymentMethod = new AnFPaymentMethod();
            try
            {
                objAnFPaymentMethod = await (from pm in DataContext.AnFPaymentMethods
                                 where pm.UserId == userId && pm.IsActive == true
                                 select new AnFPaymentMethod
                                 {
                                     Id = pm.Id,
                                     Name = pm.Name,
                                     AnFFinancialServiceProviderId = pm.AnFFinancialServiceProviderId,
                                     AnFPaymentChannelId = pm.AnFPaymentChannelId,
                                     UserId = pm.UserId,
                                     Password = AES.GetPlainText(pm.Password),
                                     IsActive = pm.IsActive,
                                     CreatedBy = pm.CreatedBy,
                                     CreatedDate = pm.CreatedDate,
                                     ModifiedBy = pm.ModifiedBy,
                                     ModifiedDate = pm.ModifiedDate,
                                 }).SingleOrDefaultAsync();
            }
            catch(Exception exp)
            {

            }
            return objAnFPaymentMethod;
        }

        public async Task<AnFPaymentMethod> GetPaymentMethodByUserId(string userId, Int16 anFPaymentChannelId)
        {
            AnFPaymentMethod objPaymentMethod = new AnFPaymentMethod();

            try
            {
                objPaymentMethod = await (from pm in DataContext.AnFPaymentMethods
                                          where
                                             pm.UserId == userId
                                          && pm.AnFPaymentChannelId == anFPaymentChannelId
                                          && pm.IsActive == true
                                          select pm).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

            }

            return objPaymentMethod;
        }

    }    
}
