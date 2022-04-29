using System.Diagnostics.CodeAnalysis;

using DBMonitor.BLL;
using DBMonitor.DAL.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace DBMonitor.DAL
{
    public class ApplicationDbContext : DbContext
    {
        [MaybeNull]
        public DbSet<Rule> Rules { get; set; }
        [MaybeNull]
        public DbSet<LaunchHistory> History { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile(@Directory.GetCurrentDirectory() + "\\appsettings.json")
                   .Build();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LaunchHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new RuleConfiguration());

        }

    }
}