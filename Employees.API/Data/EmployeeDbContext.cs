using Employees.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Employees.API.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=EmplyeesDb.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees", "EmployeeDb");
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey("Id");
                entity.HasIndex("CPF");
            });
        }
    }
}
