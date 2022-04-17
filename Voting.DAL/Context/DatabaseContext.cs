using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Voting.DAL.Entities;

namespace Voting.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Model> Models { get; set; }
        public DbSet<Pair> Pairs { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminEmail = "admin@gmail.com";
            string adminUsername = "admin";
            string adminPassword = "12345";


            Role adminRole = new Role() { Id = 1, Name = "Admin" };
            Role userRole = new Role() { Id = 2, Name = "User" };
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            var account = new Account()
            {
                Id = 1,
                Email = adminEmail,
                Username = adminUsername,
                Password = adminPassword,
                RoleId = adminRole.Id

            };
            modelBuilder.Entity<Account>().HasData(account);
            base.OnModelCreating(modelBuilder);
        }
    }
}
