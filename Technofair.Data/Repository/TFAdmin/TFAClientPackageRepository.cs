using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Lib.Utilities;
using Technofair.Model.Common;

using Technofair.Model.TFAdmin;
using Technofair.Model.ViewModel.Accounts;
using Technofair.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
    #region Interface
    public interface ITFAClientPackageRepository : IRepository<TFAClientPackage>
    {
        int AddEntity(TFAClientPackage obj);
        Task<int> AddEntityAsync(TFAClientPackage obj);
        int UpdateEntity(TFAClientPackage obj);
        List<TFAClientPackageViewModel> GetAllClientPackage();
        TFAClientPackage GetClientPackageByCustomerId(int TFACompanyCustomerId);
        List<TFAClientPackage> GetActiveClientPackageByCustomerId(int? companyCustomerId);
        List<TFAClientPackage> GetCompanyPackageExceptItSelf(TFAClientPackageViewModel objTFAClientPackage);
        //DataTable GetActiveCompanyCustomerWithClientPackage(int monthId, int year);
    }

    #endregion

    public class TFAClientPackageRepository : AdminBaseRepository<TFAClientPackage>, ITFAClientPackageRepository
    {

        public TFAClientPackageRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(TFAClientPackage obj)
        {
            int Id = 1;
            TFAClientPackage last = DataContext.TFAClientPackages.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }
        public async Task<int> AddEntityAsync(TFAClientPackage obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                TFAClientPackage? last = DataContext.TFAClientPackages.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public int UpdateEntity(TFAClientPackage obj)
        {
            DataContext.TFAClientPackages.Update(obj);
            return obj.Id;
        }
        public List<TFAClientPackageViewModel> GetAllClientPackage()
        {
            DateTime dt = DateTime.Now.Date;
            List<TFAClientPackageViewModel> list = (from p in DataContext.TFAClientPackages
                                                    join cc in DataContext.TFACompanyCustomers on p.TFACompanyCustomerId equals cc.Id
                                                    join pt in DataContext.TFACompanyPackageTypes on p.TFACompanyPackageTypeId equals pt.Id
                                                    join cp in DataContext.TFACompanyPackages on p.TFACompanyPackageId equals cp.Id into joinedCompanyPackage
                                                    from cpd in joinedCompanyPackage.DefaultIfEmpty()
                                                    where (p.IsActive)
                                                    //  group p by p.CmnCompanyCustomerId into g
                                                    select new TFAClientPackageViewModel()
                                                    {
                                                        Id = p.Id,
                                                        TFACompanyCustomerId = p.TFACompanyCustomerId,
                                                        TFACompanyPackageTypeId = p.TFACompanyPackageTypeId,
                                                        TFACompanyPackageId = p.TFACompanyPackageId,
                                                        Date = p.Date,
                                                        Rate = p.Rate,
                                                        Discount = p.Discount,
                                                        IsActive = p.IsActive,
                                                        Amount = p.Amount,
                                                        CompanyCustomerName = cc.Name,
                                                        CompanyPackageTypeName = pt.Title,
                                                        CreatedBy = p.CreatedBy,
                                                        CreatedDate = p.CreatedDate,
                                                        ModifiedBy = p.ModifiedBy,
                                                        ModifiedDate = p.ModifiedDate,
                                                        MinMaxSubscriber = cpd.MinSubscriber.ToString() + " - " + cpd.MaxSubscriber.ToString()
                                                    }).ToList();
            return list;
        }
        public TFAClientPackage GetClientPackageByCustomerId(int companyCustomerId)
        {
            TFAClientPackage anFClientPackage = new TFAClientPackage();
            anFClientPackage = DataContext.TFAClientPackages.Where(x => x.TFACompanyCustomerId == companyCustomerId).FirstOrDefault();
            return anFClientPackage;

        }

        
        public List<TFAClientPackage> GetCompanyPackageExceptItSelf(TFAClientPackageViewModel objTFAClientPackage)
        {
            List<TFAClientPackage> list = DataContext.TFAClientPackages.Where(x => x.Id != objTFAClientPackage.Id && (x.TFACompanyCustomerId == objTFAClientPackage.TFACompanyCustomerId && x.TFACompanyPackageTypeId == objTFAClientPackage.TFACompanyPackageTypeId && x.TFACompanyPackageId == objTFAClientPackage.TFACompanyPackageId)).ToList();
            return list;
        }

        public List<TFAClientPackage> GetActiveClientPackageByCustomerId(int? companyCustomerId)
        {
                        
            var clientPackages = (from cp in DataContext.TFAClientPackages
                          join cc in DataContext.TFACompanyCustomers
                          on cp.TFACompanyCustomerId equals cc.Id
                          where 
                              (cp.TFACompanyCustomerId == companyCustomerId || companyCustomerId == null)
                          && (cp.IsActive == true)
                          && (cc.IsActive == true)
                          select cp).ToList();

            return clientPackages;

        }

        //New
        //public DataTable GetActiveCompanyCustomerWithClientPackage(int monthId, int yearId)
        //{
        //    DataTable dt = new DataTable();

        //    SqlParameter[] paramsToStore = new SqlParameter[2];
        //    paramsToStore[0] = new SqlParameter("@monthId", monthId);
        //    paramsToStore[1] = new SqlParameter("@year", yearId);
            
        //    try
        //    {
        //        dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.TFAdmin.GetActiveCompanyCustomerWithClientPackage, paramsToStore).Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return dt;
        //}
        //Old
        //public async Task<List<CompanyCustomerWithClientPackage>> GetActiveCompanyCustomerWithClientPackage(int monthId, int year)
        //{
        //    //New
        //    var list = await (
        //                        from cp in DataContext.TFAClientPackages
        //                        join cc in DataContext.TFACompanyCustomers
        //                            on cp.TFACompanyCustomerId equals cc.Id
        //                        join cpt in DataContext.TFACompanyPackageTypes
        //                            on cp.TFACompanyPackageTypeId equals cpt.Id
        //                        where cp.IsActive == true && cc.IsActive == true
        //                        select new CompanyCustomerWithClientPackage
        //                        {
        //                            TFACompanyCustomerId = cc.Id,
        //                            CompanyCustomerName = cc.Name,
        //                            CompanyCustomerAddress = cc.Address,
        //                            SmsApiBaseUrl = cc.SmsApiBaseUrl,
        //                            TFAClientPackageId = cp.Id,
        //                            TFACompanyPackageTypeId = cp.TFACompanyPackageTypeId,
        //                            TFACompanyPackageId = cp.TFACompanyPackageId,
        //                            Discount = cp.Discount,
        //                            Rate = cp.Rate,
        //                            Amount = cp.Amount,
        //                            CompanyPackageTypeName = cpt.Title,
        //                            BillExists = DataContext.TFAClientPayments
        //                                .Join(DataContext.TFAClientPaymentDetails,
        //                                      cp_inner => cp_inner.Id,
        //                                      cpd_inner => cpd_inner.TFAClientPaymentId,
        //                                      (cp_inner, cpd_inner) => new { cp_inner, cpd_inner })
        //                                .Count(x => x.cpd_inner.TFAMonthId == monthId && x.cpd_inner.Year == year) > 0
        //                                ? true
        //                                : false
        //                        }).ToListAsync();

        //    return list;
        //}
    }
}
