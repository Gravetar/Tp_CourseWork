using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Xml.Linq;
using Tp_CourseWork.Repositories;

using MathNet.Numerics.Statistics;


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
    }
}
