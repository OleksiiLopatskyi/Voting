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
        public DbSet<Pair> ModelsPair { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
            .HasMany(g => g.Pairs)
            .WithOne(s => s.Profile).IsRequired()
            .HasForeignKey(s => s.ProfileId);


            string adminEmail = "admin@gmail.com";
            string adminUsername = "admin";
            string adminPassword = "12345";


            Role adminRole = new Role() { Id = 1, Name = "Admin" };
            Role userRole = new Role() { Id = 2, Name = "User" };
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            var profile = new Profile { Id = 1, Username = adminUsername, AccountId = 1 };
            var account = new Account
            {
                Id = 1,
                Email = adminEmail,
                Password = adminPassword,
                ProfileId = profile.Id,
                RoleId = adminRole.Id

            };
            modelBuilder.Entity<Profile>().HasData(profile);
            modelBuilder.Entity<Account>().HasData(account);
            base.OnModelCreating(modelBuilder);
        }
    }
}
