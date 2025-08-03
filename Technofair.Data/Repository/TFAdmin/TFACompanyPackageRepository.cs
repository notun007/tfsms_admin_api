using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
   
    #region Interface
    public interface ITFACompanyPackageRepository : IRepository<TFACompanyPackage>
    {
        int AddEntity(TFACompanyPackage obj);
        // string GetLastRefNo();
        List<TFACompanyPackageViewModel> GetCurrentPackage();
        List<TFACompanyPackageViewModel> GetAllCompanyPackage();
        List<TFACompanyPackageViewModel> GetCompanyPackageByPackageType(int anFCompanyPackageTypeId);
        TFACompanyPackage? GetTFACompanyPackageByPackageTypeIdMinMaxSub(int TFACompanyPackageTypeId, int minSub, int maxSub);
        List<TFACompanyPackage> GetCompanyPackageExceptItSelf(TFACompanyPackageViewModel objTFACompanyPackage);
    }

    #endregion
    public class TFACompanyPackageRepository : AdminBaseRepository<TFACompanyPackage>, ITFACompanyPackageRepository
    {
        public TFACompanyPackageRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(TFACompanyPackage obj)
        {
            int Id = 1;
            TFACompanyPackage? last = DataContext.TFACompanyPackages.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        //public string GetLastRefNo()
        //{
        //    try
        //    {
        //        AnFCompanyPackage? obj = (from p in DataContext.AnFCompanyPackages
        //                                      //where (p.CmnUserProfileId == profileUserId)
        //                                  select p).OrderByDescending(o => o.Id).FirstOrDefault();
        //        Int64 serial = 1;
        //        string refNo = DateTime.Now.ToString("yyMM");
        //        if (obj != null && obj.RefNo != null )
        //        {
        //            string sub = obj.RefNo.Substring(refNo.Length);
        //            if (sub != "")
        //            {
        //                serial = Convert.ToInt64(sub) + 1;
        //            }
        //        }
        //        refNo = refNo + serial;
        //        return refNo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public TFACompanyPackage? GetTFACompanyPackageByPackageTypeIdMinMaxSub(int TFACompanyPackageTypeId, int minSub,int maxSub)
        {
            TFACompanyPackage? tFACompanyPackage = DataContext.TFACompanyPackages.Where(x=>x.TFACompanyPackageTypeId == TFACompanyPackageTypeId && x.MinSubscriber == minSub && x.MaxSubscriber == maxSub).FirstOrDefault();

            return tFACompanyPackage;
        }
        public List<TFACompanyPackageViewModel> GetAllCompanyPackage()
        {
            DateTime dt = DateTime.Now.Date;
            List<TFACompanyPackageViewModel> list = (from p in DataContext.TFACompanyPackages
                                                     join pt in DataContext.TFACompanyPackageTypes on p.TFACompanyPackageTypeId equals pt.Id
                                                     where (p.IsActive)
                                                     select new TFACompanyPackageViewModel()
                                                     {
                                                         Id = p.Id,
                                                         TFACompanyPackageTypeName = pt.Title,
                                                         TFACompanyPackageTypeId = p.TFACompanyPackageTypeId,
                                                         MinSubscriber = p.MinSubscriber,
                                                         MaxSubscriber = p.MaxSubscriber,
                                                         Rate = p.Rate,
                                                         Price = p.Price,
                                                         Remarks = p.Remarks,
                                                         IsActive = p.IsActive,
                                                         CreatedBy = p.CreatedBy,
                                                         CreatedDate = p.CreatedDate,
                                                         ModifiedBy = p.ModifiedBy,
                                                         ModifiedDate = p.ModifiedDate,
                                                         MinMaxSubscriber = p.MinSubscriber.ToString() + " - " + p.MaxSubscriber.ToString()

                                                     }).OrderBy(o => o.TFACompanyPackageTypeName).ThenBy(o => o.MinSubscriber).ToList();
            return list;
        }
        public List<TFACompanyPackageViewModel> GetCompanyPackageByPackageType(int anFCompanyPackageTypeId)
        {
            DateTime dt = DateTime.Now.Date;
            List<TFACompanyPackageViewModel> list = (from p in DataContext.TFACompanyPackages
                                                     join pt in DataContext.TFACompanyPackageTypes on p.TFACompanyPackageTypeId equals pt.Id
                                                     where (p.TFACompanyPackageTypeId == anFCompanyPackageTypeId && p.IsActive)
                                                     select new TFACompanyPackageViewModel()
                                                     {
                                                         Id = p.Id,
                                                         TFACompanyPackageTypeName = pt.Title,
                                                         TFACompanyPackageTypeId = p.TFACompanyPackageTypeId,
                                                         MinSubscriber = p.MinSubscriber,
                                                         MaxSubscriber = p.MaxSubscriber,
                                                         Rate = p.Rate,
                                                         Price = p.Price,
                                                         Remarks = p.Remarks,
                                                         IsActive = p.IsActive,
                                                         CreatedBy = p.CreatedBy,
                                                         CreatedDate = p.CreatedDate,
                                                         ModifiedBy = p.ModifiedBy,
                                                         ModifiedDate = p.ModifiedDate,
                                                         MinMaxSubscriber = p.MinSubscriber.ToString() + " - " + p.MaxSubscriber.ToString()

                                                     }).OrderBy(o => o.MinSubscriber).ToList();
            return list;
        }
        public List<TFACompanyPackageViewModel> GetCurrentPackage()
        {
            DateTime dt = DateTime.Now.Date;
            List<TFACompanyPackageViewModel> list = (from p in DataContext.TFACompanyPackages
                                                     join pt in DataContext.TFACompanyPackageTypes on p.TFACompanyPackageTypeId equals pt.Id
                                                     where (p.IsActive)
                                                     select new TFACompanyPackageViewModel()
                                                     {
                                                         Id = p.Id,
                                                         TFACompanyPackageTypeId = p.TFACompanyPackageTypeId,
                                                         MinSubscriber = p.MinSubscriber,
                                                         MaxSubscriber = p.MaxSubscriber,
                                                         Price = p.Price,
                                                         Remarks = p.Remarks,
                                                         IsActive = p.IsActive,
                                                         CreatedBy = p.CreatedBy,
                                                         CreatedDate = p.CreatedDate,
                                                         ModifiedBy = p.ModifiedBy,
                                                         ModifiedDate = p.ModifiedDate,
                                                         MinMaxSubscriber = p.MinSubscriber.ToString() + " - " + p.MaxSubscriber.ToString()
                                                     }).OrderBy(o => o.CreatedDate).ToList();
            return list;
        }
        public List<TFACompanyPackage> GetCompanyPackageExceptItSelf(TFACompanyPackageViewModel objTFACompanyPackage)
        {
            List<TFACompanyPackage> list = DataContext.TFACompanyPackages.Where(x => x.Id != objTFACompanyPackage.Id && (x.TFACompanyPackageTypeId == objTFACompanyPackage.TFACompanyPackageTypeId && x.MinSubscriber == objTFACompanyPackage.MinSubscriber && x.MaxSubscriber== objTFACompanyPackage.MaxSubscriber)).ToList();
            return list;
        }

    }

}
