using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class ApiControllerTests
    {
        private readonly Mock<ILogger<ApiController>> _mock = new();
        private readonly IServiceProvider _serviceProvider;

        public ApiControllerTests()
        {
            _serviceProvider = DependencyInjection.InitilizeServices().BuildServiceProvider();
        }

        [Fact]
        public async Task Get_ContentResult_WithLocalities()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db);

            var controller = new ApiController(repo, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = controller.GetLocalities();

            Assert.IsType<ContentResult>(result);
        }

        [Fact]
        public async Task Delete_ContentResult_WithDeleteLocality()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db);

            var controller = new ApiController(repo, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = controller.DeleteLocality(1);

            Assert.IsType<ContentResult>(result);
        }

        [Fact]
        public async Task Delete_NotFound_WithDeleteLocality()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db);

            var controller = new ApiController(repo, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = controller.DeleteLocality(20);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ContentResult_WithUpdateLocality()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db);

            var controller = new ApiController(repo, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            Locality Tstlocality = new Locality { Name = "Локация", Budget = 100000, Mayor = "Иванов Иван Иванович", NumberResidants = 1000000, Type = "City" };

            db.Localities.Add(Tstlocality);

            await db.SaveChangesAsync();
            Tstlocality.Name = "Город";

            var result = controller.UpdateLocality(Tstlocality);

            await db.SaveChangesAsync();

            Assert.IsType<ContentResult>(result);
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
