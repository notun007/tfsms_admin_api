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

        public DbSet<SecDashboard> SecDashboards { get; set; }
        public DbSet<SecDashboardPermission> SecDashboardPermissions { get; set; }
        public DbSet<SecUserVisitor> SecUserVisitors { get; set; }
        public DbSet<SecWebsiteVisitor> SecWebsiteVisitors { get; set; }

    }
}
