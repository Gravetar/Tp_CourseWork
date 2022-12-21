using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Xml.Linq;
using Tp_CourseWork.Repositories;


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
        /// Получить всех пользователей
        /// </summary>
        [HttpGet("GetLocalities")]
        public IActionResult GetUsers()
        {
            dynamic localities = new JArray();
            dynamic response = new JObject();

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

            response.response = localities;

            response.status = "OK";

            string respStr = response.ToString();

            return Content(respStr);
        }
    }
}
