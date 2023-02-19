using Microsoft.EntityFrameworkCore;
using WebHR.Models;

namespace WebHR.Context
{
    public class WebMyContext : DbContext
    {
        public WebMyContext(DbContextOptions<WebMyContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Departements> Departements { get; set; }
        public DbSet<AccountRoles> AccountRoles { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccountRoles>().HasKey(ar => new { ar.RoleId, ar.AccountNIK });
            //builder.Entity<AccountRoles>()
            //    .HasOne(a => a.Accounts)
            //    .WithMany(ar => ar.AccountRoles)
            //    .HasForeignKey(a => a.AccountNIK);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
