using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Tp_CourseWork.GofComand;
using Tp_CourseWork.Models;
using Tp_CourseWork.Models.ViewModels;

namespace Tp_CourseWork.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:7105/";
        public async Task<ActionResult> Index()
        {

            List<Locality> LocInfo = new List<Locality>();
            List<string> Mayors = new List<string>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetLocalities using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/GetLocalities");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var LocResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Localities list
                    LocInfo = JsonConvert.DeserializeObject<List<Locality>>(LocResponse);
                }

                for (int i = 0; i < LocInfo.Count; i++)
                {
                    Mayors.Add(LocInfo[i].Mayor);
                }

                Mayors = Mayors.Distinct().ToList();

                var model = new LocalitiesWithUniqMayor
                {
                    Localities = LocInfo,
                    Mayors = Mayors
                };

                //returning the Localities list to view
                return View(model);
            }
        }
    }

    //public class HomeController : Controller
    //{
    //    //Hosted web API REST Service base url
    //    string Baseurl = "https://localhost:7105/";
    //    public async Task<ActionResult> Index()
    //    {
    //        List<Locality> LocInfo = new List<Locality>();
    //        using (var client = new HttpClient())
    //        {
    //            //Passing service base url
    //            client.BaseAddress = new Uri(Baseurl);
    //            client.DefaultRequestHeaders.Clear();
    //            //Define request data format
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //            //Sending request to find web api REST service resource GetLocalities using HttpClient
    //            HttpResponseMessage Res = await client.GetAsync("api/GetLocalities");
    //            //Checking the response is successful or not which is sent using HttpClient
    //            if (Res.IsSuccessStatusCode)
    //            {
    //                //Storing the response details recieved from web api
    //                var LocResponse = Res.Content.ReadAsStringAsync().Result;
    //                //Deserializing the response recieved from web api and storing into the Localities list
    //                LocInfo = JsonConvert.DeserializeObject<List<Locality>>(LocResponse);
    //            }
    //            //returning the Localities list to view
    //            return View(LocInfo);
    //        }
    //    }
    //}
}
