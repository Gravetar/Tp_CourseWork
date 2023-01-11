using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_CourseWork.Controllers;
using Tp_CourseWork.DB;
using Tp_CourseWork.Models;
using Tp_CourseWork.Repositories;
using Xunit;

namespace TP_CourseWork.Tests
{
    public class BaseRepositoryTests
    {
        private readonly Mock<ILogger<BaseRepository>> _mock = new();
        private readonly IServiceProvider _serviceProvider;

        public BaseRepositoryTests()
        {
            _serviceProvider = DependencyInjection.InitilizeServices().BuildServiceProvider();
        }

        [Fact]
        public async Task Get_ReturnsListLocalities_WithLocalities()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = repo.GetLocalities();

            Assert.IsType<List<Locality>>(result);
        }

        [Fact]
        public async Task Get_ReturnsLocality_WithLocalities()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = repo.GetLocalityById(1);

            Assert.IsType<Locality>(result);
        }

        [Fact]
        public async Task Get_ReturnsNull_WithLocalitiesIncorrectId()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = repo.GetLocalityById(20);

            Assert.Null(result);
        }

        private List<Locality> GetTestLocalities()
        {
            var localities = new List<Locality>
            {
                new Locality { Name="Волгоград", Budget=100000, Mayor = "Владимир Васильевич Марченко", NumberResidants = 1004763, Type = "City"},
                new Locality { Name="Волжский", Budget=200000, Mayor = "Игорь Николаевич Воронин", NumberResidants = 321479, Type = "City"},
                new Locality { Name="Москва", Budget=300000, Mayor = "Сергей Семёнович Собянин", NumberResidants = 13010112, Type = "City"}
            };
            return localities;
        }
    }
}
