using Bing;
using Carlister.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Carlister.Controllers
{
    [RoutePrefix("api/Car")]
    public class CarController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public string Get()
        {
            return "Hello World";
        }

        [Route("GetYearsDist")]
        public async Task<List<string>> GetYearsDist()
        {
            return await db.GetYearsDist();
        }

        [Route("GetMakesDist")]
        public async Task<List<string>> GetMakesDist()
        {
            return await db.GetMakesDist();
        }

        [Route("GetMakesByYear")]
        public async Task<List<string>> GetMakesByYear(string year)
        {
            return await db.GetMakesByYear(year);
        }

        [Route("GetMakesByYearDist")]
        public async Task<List<string>> GetMakesByYearDist(string year)
        {
            return await db.GetMakesByYearDist(year);
        }

        [Route("GetCarsByYear")]
        public async Task<List<Car>> GetCarsByYear(string year)
        {
            return await db.GetCarsByYear(year);
        }

        [Route("GetCarsByYearMake")]
        public async Task<List<Car>> GetCarsByYearMake(string year, string make)
        {
            return await db.GetCarsByYearMake(year, make);
        }

        [Route("GetCarsByYearMakeModel")]
        public async Task<List<Car>> GetCarsByYearMakeModel(string year, string make, string model)
        {
            return await db.GetCarsByYearMakeModel(year, make, model);
        }

        [Route("GetCarsByYearMakeModelTrim")]
        public async Task<List<Car>> GetCarsByYearMakeModelTrim(string year, string make, string model, string trim)
        {
            return await db.GetCarsByYearMakeModelTrim(year, make, model, trim);
        }

        [Route("GetCar")]
        public async Task<IHttpActionResult> GetCar(int id)
        {
            var car = db.Cars.Find(id);

            if (car == null)
            {
                return await Task.FromResult(NotFound());
            }

            HttpResponseMessage response;

            var client = new BingSearchContainer(
        	new Uri("https://api.datamarket.azure.com/Bing/search/")
            	);
            client.Credentials = new NetworkCredential("accountKey", "baDOTqrNCHJ38GxTzgc8o7rpbstAS9VG5gYE8kfkHro");
            var marketData = client.Composite(
	        "image",
	        car.model_year + " " + car.make + " " + car.model_name + " " + car.model_trim,
	        null,
	        null,
	        null,
	        null,
	        null,
	        null,
	        null,
	        null,
	        null,
	        null,
	        null,
	        null,
	        null
	        ).Execute();

            var result = marketData.FirstOrDefault();
            var image = result != null ? result.Image : null;
            var firstImage = image != null ? image.FirstOrDefault() : null;
            var mediaUrl = firstImage != null ? firstImage.MediaUrl : null;

            dynamic recalls;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://www.nhtsa.gov/");

                try
                {
                    response = await httpClient.GetAsync("webapi/api/Recalls/vehicle/modelyear/"+car.model_year+"/make/"+car.make+"/model/"+car.model_name+"?format=json");
                    recalls = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }

            return Ok(new { car, mediaUrl, recalls });
        }
    }
}
