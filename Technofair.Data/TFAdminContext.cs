using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.HRM;
using TFSMS.Admin.Model.Security;


//using TFSMS.Admin.Model.Accounts;
//using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.TFLoan.Device;
using Technofair.Utiity.Helper;
using Technofair.Model.TFLoan.Device;

namespace TFSMS.Admin.Data
{
    public class TFAdminContext: DbContext
    {
        //NEW
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //New: 26.05.2025
            string connectionString = string.Empty;
            base.OnConfiguring(optionsBuilder);
            string c = Directory.GetCurrentDirectory();
            IConfigurationRoot configuration =
            new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();

            if (ProgramConfigaration.IsProduction)
            {
                connectionString = ProgramConfigaration.ConnectionString;
            }
            else
            {
                connectionString = configuration.GetConnectionString("TFAdminContext");
            }
            optionsBuilder.UseSqlServer(connectionString);
            //End


            //Old- Asad Commented On 26052025
            //base.OnConfiguring(optionsBuilder);
            //string c = Directory.GetCurrentDirectory();
            //IConfigurationRoot configuration =
            //new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
            //string connectionStringIs = configuration.GetConnectionString("SMSContext");
            // optionsBuilder.UseSqlServer(connectionStringIs);

        }
        //Old
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{                        

        //    base.OnConfiguring(optionsBuilder);
        //    string c = Directory.GetCurrentDirectory();
        //    IConfigurationRoot configuration =
        //    new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
        //    string connectionStringIs = configuration.GetConnectionString("TFAdminContext");
        //    optionsBuilder.UseSqlServer(connectionStringIs);

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserInformation>().HasIndex(x => x.MobileNo).IsUnique();
           
        }

        public DbSet<CmnFinancialYear> CmnFinancialYears { get; set; }
        public DbSet<SecCompanyModule> SecCompanyModules { get; set; }
        public DbSet<SecModule> SecModules { get; set; }
        public DbSet<SecMenu> SecMenus { get; set; }
        public DbSet<SecRole> SecRoles { get; set; }
        public DbSet<SecUserType> SecUserTypes { get; set; }
        public DbSet<SecUser> SecUsers { get; set; }
        public DbSet<SecUserRole> SecUserRoles { get; set; }
        public DbSet<SecMenuPermission> SecMenuPermissions { get; set; }

        public DbSet<HrmFileCategory> HrmFileCategories { get; set; }
        public DbSet<HrmDesignation> HrmDesignations { get; set; }
        public DbSet<HrmEmployee> HrmEmployees { get; set; }
        public DbSet<SecCompanyUser> SecCompanyUsers { get; set; }


        public DbSet<TFACompanyCustomer> TFACompanyCustomers { get; set; }
        public DbSet<TFACompanyCustomerLog> TFACompanyCustomerLogs { get; set; }
        public DbSet<TFACompanyPackageType> TFACompanyPackageTypes { get; set; }
        public DbSet<TFACompanyPackage> TFACompanyPackages { get; set; }
        public DbSet<TFAClientPackage> TFAClientPackages { get; set; }
        public DbSet<TFAClientPayment> TFAClientPayments { get; set; }
        public DbSet<TFAClientSMSBalance> TFAClientSMSBalances { get; set; }
        public DbSet<TFAClientPaymentDetail> TFAClientPaymentDetails { get; set; }
        public DbSet<TFACompanyCollection> TFACompanyCollections { get; set; }
        public DbSet<TFAPaymentRequestProcess> TFAPaymentRequestProcesss { get; set; }
        public DbSet<TFAClientServerInfo> TFAClientServerInfos { get; set; }
        public DbSet<TFAClientPackageLog> TFAClientPackageLogs { get; set; }
        public DbSet<TFABillGenPermssion> TFABillGenPermssions { get; set; }
        public DbSet<TFAMonth> TFAMonths { get; set; }




        #region  Loan

        public DbSet<AnFFinancialServiceProviderType> AnFFinancialServiceProviderTypes { get; set; }
        public DbSet<AnFPaymentChannel> AnFPaymentChannels { get; set; }
        public DbSet<AnFFinancialServiceProvider> AnFFinancialServiceProviders { get; set; }
        public DbSet<AnFPaymentMethod> AnFPaymentMethods { get; set; }
        public DbSet<CmnAppSetting> CmnAppSettings { get; set; }
        public DbSet<CmnCompanyType> CmnCompanyTypes { get; set; }
        public DbSet<CmnCompany> CmnCompanies { get; set; }

        public DbSet<CmnCountry> CmnCountries { get; set; }
        public DbSet<CmnDivision> CmnDivisions { get; set; }
        public DbSet<CmnDistrict> CmnDistricts { get; set; }
        public DbSet<CmnUpazilla> CmnUpazillas { get; set; }
        public DbSet<CmnUnion> CmnUnions { get; set; }

        public DbSet<LnDeviceLenderLoaneePolicy> LnDeviceLenderLoaneePolicies { get; set; }
        public DbSet<LnDeviceLoanDisbursement> LnDeviceLoanDisbursements { get; set; }


        public DbSet<LnDeviceLoanCollection> LnDeviceLoanCollections { get; set; }

        public DbSet<LnRechargeLoanCollection> LnRechargeLoanCollections { get; set; }

        public DbSet<LnLoanCollectionType> LnLoanCollectionTypes { get; set; }
        public DbSet<LnDeviceLenderType> LnDeviceLenderTypes { get; set; }
        public DbSet<LnDeviceLender> LnDeviceLenders { get; set; }
        public DbSet<LnLoanModel> LnLoanModels { get; set; }
        public DbSet<LnTenure> LnTenures { get; set; }
        #endregion


        #region From SMSContext                   
        //public DbSet<SecDashboard> SecDashboards { get; set; }
        //public DbSet<SecDashboardPermission> SecDashboardPermissions { get; set; }
        //public DbSet<SecUserVisitor> SecUserVisitors { get; set; }
        //public DbSet<SecWebsiteVisitor> SecWebsiteVisitors { get; set; } 

        #endregion

    }
}
