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
            // Определение локации
            Locality locality1 = new Locality
            {
                id = 1,
                Name = "Москва",
                Type = "City",
                NumberResidantsTh = 12615.28,
                BudgetMlrd = 3150.0,
                Mayor = "Сергей Семёнович Собянин"
            };
            Locality locality2 = new Locality
            {
                id = 2,
                Name = "Санкт-Петербург",
                Type = "City",
                NumberResidantsTh = 3150.0,
                BudgetMlrd = 5383.89,
                Mayor = "Александр Дмитриевич Беглов"
            };
            Locality locality3 = new Locality
            {
                id = 3,
                Name = "Севастополь",
                Type = "City",
                NumberResidantsTh = 443.21,
                BudgetMlrd = 61.7,
                Mayor = "Сергей Семёнович Собянин"
            };
            Locality locality4 = new Locality
            {
                id = 4,
                Name = "Краснодарский край",
                Type = "Region",
                NumberResidantsTh = 5648.24,
                BudgetMlrd = 292.7,
                Mayor = "Вениамин Иванович Кондратьев"
            };
            Locality locality5 = new Locality
            {
                id = 5,
                Name = "Свердловская область",
                Type = "Region",
                NumberResidantsTh = 4315.70,
                BudgetMlrd = 296.6,
                Mayor = "Сергей Семёнович Собянин"
            };

            modelBuilder.Entity<Locality>().HasData(locality1);
            modelBuilder.Entity<Locality>().HasData(locality2);
            modelBuilder.Entity<Locality>().HasData(locality3);
            modelBuilder.Entity<Locality>().HasData(locality4);
            modelBuilder.Entity<Locality>().HasData(locality5);
        }
    }
}
