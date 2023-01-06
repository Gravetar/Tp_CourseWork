using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Tp_CourseWork.GofComand;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:7105/";
        public async Task<ActionResult> Index()
        {
            List<Locality> LocInfo = new List<Locality>();
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
                //returning the Localities list to view
                return View(LocInfo);
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
