using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Model.Accounts;
using Technofair.Model.ViewModel.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Model.Utility;
//using static Technofair.Lib.Utilities.SPList;

namespace Technofair.Data.Repository.Accounts
{
    public interface IAnFPaymentMethodCredentialRepository : IRepository<AnFPaymentMethodCredential>
    {
        Int16 AddEntity(AnFPaymentMethodCredential obj);
        //Task<List<AnFPaymentMethodCredentialViewModel>> GetPaymentMethod();
        AnFPaymentMethodCredential? GetByAnFPaymentMethodId(int AnFPaymentMethodId);
        AnFPaymentMethodCredential GetByUserId(string userId);
        //Task<AnFPaymentMethodCredential> GetPaymentMethodCredentialByUserId(string userId);
        //Task<AnFPaymentMethodCredential> GetPayBillCredentialByUserId(string userId);
      
        //Task<List<AnFPaymentMethodCredential>> GetPaymentMethodCredentialByCompanies(List<Company> Companies, Int16 anfPaymentMethodId);
        AnFPaymentMethodCredential? GetPaymentMethodByCompanyIdPaymentMethod(int? companyId, int paymentMethodId);
    }

    public class AnFPaymentMethodCredentialRepository : BaseRepository<AnFPaymentMethodCredential>, IAnFPaymentMethodCredentialRepository
    {
        public AnFPaymentMethodCredentialRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public Int16 AddEntity(AnFPaymentMethodCredential obj)
        {
            Int16 Id = 1;
            AnFPaymentMethodCredential last = DataContext.AnFPaymentMethodCredentials.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        //new
        //public async Task<List<AnFPaymentMethodCredentialViewModel>> GetPaymentMethod()
        //{
        //    var obj = await (from pmd in DataContext.AnFPaymentMethodCredentials
        //                     join ct in DataContext.CmnCompanyTypes on pmd.CmnCompanyTypeId equals ct.Id
        //                     join cc in DataContext.CmnCompanies on pmd.CmnCompanyId equals cc.Id into ccJoin
        //                     from cc in ccJoin.DefaultIfEmpty()
        //                     join pm in DataContext.AnFPaymentMethods on pmd.AnFPaymentMethodId equals pm.Id                             
        //                     orderby ct.SerialNo, pmd.CmnCompanyId
        //                     select new AnFPaymentMethodCredentialViewModel
        //                     {
        //                         Id = pmd.Id,
        //                         CmnCompanyTypeId = pmd.CmnCompanyTypeId,
        //                         CmnCompanyId = pmd.CmnCompanyId,
        //                         AnFPaymentMethodId = pmd.AnFPaymentMethodId,                                 
        //                         UserID = pmd.UserID,
        //                         AuthorizationCode = pmd.AuthorizationCode,
        //                         AccountNo = pmd.AccountNo,
        //                         IsActive = pmd.IsActive,
                                 
        //                         PaymentUrl = pmd.PaymentUrl,
        //                         PaymentMethod = pm.Name,
        //                         CompanyTypeName = ct.ShortName,
        //                         CompanyName = cc != null ? cc.Name : "N/A",
                                 
        //                         CreatedBy = pmd.CreatedBy,
        //                         CreatedDate = DateTime.Now,
        //                         ModifiedBy = pmd.ModifiedBy,
        //                         ModifiedDate = DateTime.Now
        //                     }).ToListAsync();

        //    return obj;
        //}

        public AnFPaymentMethodCredential? GetByAnFPaymentMethodId(int AnFPaymentMethodId)
        {
            AnFPaymentMethodCredential? paymentMethodDetail = DataContext.AnFPaymentMethodCredentials.Where(x => x.AnFPaymentMethodId == AnFPaymentMethodId).FirstOrDefault();

            return paymentMethodDetail;

        }

        public AnFPaymentMethodCredential GetByUserId(string userId)
        {
            return DataContext.AnFPaymentMethodCredentials.FirstOrDefault(x => x.UserID == userId);
        }


        //public async Task<AnFPaymentMethodCredential> GetPaymentMethodCredentialByUserId(string userId)
        //{
        //    var obj = await (from pm in DataContext.AnFPaymentMethods
        //                     join pmd in DataContext.AnFPaymentMethodCredentials
        //                     on pm.Id equals pmd.AnFPaymentMethodId
        //                     where pmd.UserID == userId && pm.IsActive == true
        //                     select pmd).SingleOrDefaultAsync();

        //    return obj;
        //}

        //public async Task<AnFPaymentMethodCredential> GetPayBillCredentialByUserId(string userId)
        //{
        //    AnFPaymentMethodCredential objCredential = new AnFPaymentMethodCredential();

        //    try
        //    {
        //        objCredential = await (from pm in DataContext.AnFPaymentMethods
        //                               join pmd in DataContext.AnFPaymentMethodCredentials
        //                               on pm.Id equals pmd.AnFPaymentMethodId
        //                               where
        //                                  pm.UserId == userId
        //                               && pm.AnFPaymentChannelId == (Int16)Technofair.Utiity.Enums.Common.AnFPaymentChannelsEnum.BillPay
        //                               && pm.IsActive == true
        //                               select pmd).FirstOrDefaultAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return objCredential;
        //}


        

       
        //public async Task<List<AnFPaymentMethodCredential>> GetPaymentMethodCredentialByCompanies(List<Company> Companies, Int16 anfPaymentMethodId)
        //{
        //    var ids = (from c in Companies
        //               select c.Id).ToList();

        //    var obj = await (from pm in DataContext.AnFPaymentMethods
        //                     join pmd in DataContext.AnFPaymentMethodCredentials
        //                     on pm.Id equals pmd.AnFPaymentMethodId
        //                     where
        //                     ids.Contains(pmd.CmnCompanyId) &&
        //                     pm.Id == anfPaymentMethodId &&
        //                     pm.IsActive == true
        //                     select pmd).ToListAsync();

        //    return obj;
        //}
        public AnFPaymentMethodCredential? GetPaymentMethodByCompanyIdPaymentMethod(int? companyId, int paymentMethodId)
        {
            AnFPaymentMethodCredential? paymentMethodCredential = DataContext.AnFPaymentMethodCredentials.Where(x => x.CmnCompanyId == companyId && x.AnFPaymentMethodId == paymentMethodId).SingleOrDefault();

            return paymentMethodCredential;
        }
    }
}
