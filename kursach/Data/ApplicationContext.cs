using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Data
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Solution> SolvedIssues => Set<Solution>();
        public DbSet<Issue> Issues => Set<Issue>();
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data.db");
        }
    }
}
