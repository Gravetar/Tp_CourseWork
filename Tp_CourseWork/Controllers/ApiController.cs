using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Xml.Linq;
using Tp_CourseWork.Repositories;

using MathNet.Numerics.Statistics;
using static Azure.Core.HttpHeader;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class ApiController: ControllerBase
    {
        private BaseRepository _repo;
        private readonly ILogger<ApiController> _logger;
        private readonly IConfiguration _config;

        public ApiController(ILogger<ApiController> logger, IConfiguration config, BaseRepository Repo)
        {
            _logger = logger; 
            _config = config;
            _repo = Repo;
        }

        //-----------------------------ЗАПРОСЫ-----------------------------
        /// <summary>
        /// Получить все локации
        /// </summary>
        [HttpGet("GetLocalities")]
        public IActionResult GetLocalities()
        {
            dynamic localities = new JArray();

            var ForeachLocalities = _repo.GetLocalities();
            foreach (var l in ForeachLocalities)
            {
                localities.Add(new JObject(
                    new JProperty("id", l.id),
                    new JProperty("name", l.Name),
                    new JProperty("type", l.Type),
                    new JProperty("numberresidants", l.NumberResidants),
                    new JProperty("budget", l.Budget),
                    new JProperty("mayor", l.Mayor)
                    ));
            }

            string respStr = localities.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Получить статистику по бюджетам
        /// </summary>
        [HttpGet("GetStatisticBudgets")]
        public IActionResult GetLStatisticBudgets()
        {
            var StatisticFromResult = _repo.GetLStatisticBudgets(_repo.GetBudgets());
            var statistic = new JObject(
                new JProperty("Median", StatisticFromResult.Median),
                new JProperty("Mean", StatisticFromResult.Mean),
                new JProperty("Max", StatisticFromResult.Max),
                new JProperty("Min", StatisticFromResult.Min)
                );

            string respStr = statistic.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Получить все локации
        /// </summary>
        [HttpGet("GetLocalitiyById")]
        public IActionResult GetLocalitiyById([FromQuery(Name = "id")] int id )
        {
            dynamic resp = new JObject();

            var l = _repo.GetLocalityById(id);
            if (l != null)
            {
                resp = new JObject(
                    new JProperty("id", l.id),
                    new JProperty("name", l.Name),
                    new JProperty("type", l.Type),
                    new JProperty("numberresidants", l.NumberResidants),
                    new JProperty("budget", l.Budget),
                    new JProperty("mayor", l.Mayor)
                    );
            }

            string respStr = resp.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Создать локацию
        /// </summary>
        [HttpPost("CreateLocality")]
        public IActionResult CreateLocality([FromBody] Locality locality)
        {
            string name = locality.Name;
            string type = locality.Type;
            int numberresidants = locality.NumberResidants;
            int budget = locality.Budget;
            string mayor = locality.Mayor;

            Locality loc = new Locality();

            loc.Name = name;
            loc.Type = type;
            loc.NumberResidants = numberresidants;
            loc.Budget = budget;
            loc.Mayor = mayor;

            bool res = _repo.CreateLocality(loc);

            string respStr = res.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Редактировать локацию
        /// </summary>
        [HttpPost("UpdateLocality")]
        public IActionResult UpdateLocality([FromBody] Locality locality)
        {

            bool res = _repo.UpdateLocality(locality);

            string respStr = res.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Удалить локацию по айди
        /// </summary>
        [HttpPost("DeleteLocality")]
        public IActionResult DeleteLocality([FromBody] int id)
        {

            bool res = _repo.DeleteLocality(id);

            string respStr = res.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Получить все локации по фио мера
        /// </summary>
        [HttpGet("GetLocalitiesByMayor")]
        public IActionResult GetLocalitiesByMajor([FromQuery(Name = "mayor")] string mayor)
        {
            dynamic localities = new JArray();

            var ForeachLocalities = _repo.GetLocalitiesByMayor(mayor);
            foreach (var l in ForeachLocalities)
            {
                localities.Add(new JObject(
                    new JProperty("id", l.id),
                    new JProperty("name", l.Name),
                    new JProperty("type", l.Type),
                    new JProperty("number_residants", l.NumberResidants),
                    new JProperty("budget", l.Budget),
                    new JProperty("mayor", l.Mayor)
                    ));
            }

            string respStr = localities.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Получить медиану бюджета
        /// </summary>
        [HttpGet("GetMedianBudget")]
        public IActionResult GetMedianBudget()
        {
            var ForeachLocalities = _repo.GetBudgets();

            var str = Statistics.Median(ForeachLocalities);

            string respStr = str.ToString();

            return Content(respStr);
        }

        // <summary>
        /// Получить среднее бюджета
        /// </summary>
        [HttpGet("GetMeanBudget")]
        public IActionResult GetMeanBudget()
        {
            var ForeachLocalities = _repo.GetBudgets();

            var str = Statistics.Mean(ForeachLocalities);

            string respStr = str.ToString();

            return Content(respStr);
        }

        // <summary>
        /// Получить Максимальный бюджета
        /// </summary>
        [HttpGet("GetMaxBudget")]
        public IActionResult GetMaxBudget()
        {
            var ForeachLocalities = _repo.GetBudgets();

            var str = Statistics.Maximum(ForeachLocalities);

            string respStr = str.ToString();

            return Content(respStr);
        }

        // <summary>
        /// Получить минимум бюджета
        /// </summary>
        [HttpGet("GetMinBudget")]
        public IActionResult GetMinBudget()
        {
            var ForeachLocalities = _repo.GetBudgets();

            var str = Statistics.Minimum(ForeachLocalities);

            string respStr = str.ToString();

            return Content(respStr);
        }
    }
}
