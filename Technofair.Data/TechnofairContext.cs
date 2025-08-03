using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.HRM;
using TFSMS.Admin.Model.Security;

using Technofair.Utiity.Helper;

#nullable disable
namespace TFSMS.Admin.Data
{
    public class TechnofairContext : DbContext
    {               
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //New: 26.05.2025
            string connectionString = string.Empty;                        
            base.OnConfiguring(optionsBuilder);
            string c = Directory.GetCurrentDirectory();
            IConfigurationRoot configuration =
            new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();

            if(ProgramConfigaration.IsProduction)
            {
                connectionString = ProgramConfigaration.ConnectionString;
            }
            else
            {
                connectionString = configuration.GetConnectionString("SMSContext");
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserInformation>().HasIndex(x => x.MobileNo).IsUnique();

            //.HasKey(account => new { account.Id, account.Email, account.RoleId });
            //modelBuilder.Entity<ScpPackagePeriodPermission>().HasIndex(x => x.CmnCompanyId).IsUnique();//.HasKey(account
        }


        public DbSet<AnFPaymentChannel> AnFPaymentChannels { get; set; }


        public DbSet<AnFPaymentMethod> AnFPaymentMethods { get; set; }
              
        public DbSet<AnFFinancialServiceProviderType> AnFFinancialServiceProviderTypes { get; set; }
        public DbSet<AnFFinancialServiceProvider> AnFFinancialServiceProviders { get; set; } 
        public DbSet<CmnDivision> CmnDivisions { get; set; }
        public DbSet<CmnDistrict> CmnDistricts { get; set; }
        public DbSet<CmnUpazilla> CmnUpazillas { get; set; }
        public DbSet<CmnUnion> CmnUnions { get; set; }
        public DbSet<CmnCompanyType> CmnCompanyTypes { get; set; }
        public DbSet<CmnCompany> CmnCompanies { get; set; }
        //public DbSet<CmnFinancialYear> CmnFinancialYears { get; set; }
        public DbSet<CmnCountry> CmnCountries { get; set; }
        //public DbSet<SecCompanyModule> SecCompanyModules { get; set; }
        //public DbSet<SecCompanyUser> SecCompanyUsers { get; set; }
       // public DbSet<SecModule> SecModules { get; set; }
        //public DbSet<SecMenu> SecMenus { get; set; }
       // public DbSet<SecMenuPermission> SecMenuPermissions { get; set; }
        //public DbSet<SecRole> SecRoles { get; set; }
        //public DbSet<SecUserRole> SecUserRoles { get; set; }
        //public DbSet<SecUserType> SecUserTypes { get; set; }
        //public DbSet<SecUser> SecUsers { get; set; }
        public DbSet<SecDashboard> SecDashboards { get; set; }
        public DbSet<SecDashboardPermission> SecDashboardPermissions { get; set; }
        public DbSet<SecUserVisitor> SecUserVisitors { get; set; }
        public DbSet<SecWebsiteVisitor> SecWebsiteVisitors { get; set; }
    
        //public DbSet<HrmDesignation> HrmDesignations { get; set; }
        //public DbSet<HrmEmployee> HrmEmployees { get; set; }
               
        //public DbSet<ScpDeviceAssign> ScpDeviceAssigns { get; set; }
      
        //public DbSet<ScpSubscriberPackage> ScpSubscriberPackages { get; set; }        
      
        public DbSet<CmnAppSetting> CmnAppSettings { get; set; }        
    }
}
