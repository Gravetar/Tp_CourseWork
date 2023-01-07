using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.DB
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Locality> Localities { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options = null) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Определение товаров
            Locality locality1 = new Locality
            {
                id = 1,
                Name = "Волгоград",
                Type = "City",
                NumberResidants = 100000,
                Budget = 100000,
                Mayor = "Владимир Васильевич Марченко"
            };

            modelBuilder.Entity<Locality>().HasData(locality1);
        }
    }
}
