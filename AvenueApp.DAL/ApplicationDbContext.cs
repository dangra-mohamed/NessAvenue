using AvenueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AvenueApp.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Employee>().HasData(
        //        new Employee { Id = 1, Name = "Action", DateOfBirth = new DateTime(1989, 10, 28), Salary = 50000 },
        //        new Employee { Id = 2, Name = "SciFi", DateOfBirth = new DateTime(1992, 5, 15), Salary = 60000 },
        //        new Employee { Id = 3, Name = "History", DateOfBirth = new DateTime(1985, 8, 3), Salary = 55000 }
        //    );
        //}

    }
}
