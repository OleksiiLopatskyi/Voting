using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Entities;

namespace Voting.DAL.Context
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
            Database.EnsureCreated();       
        }
        public DbSet<Model> Models { get; set; }
        public DbSet<Pair> ModelsPair { get; set; }
    }
}
