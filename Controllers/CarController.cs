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

        public class apiparm
        {
            public string year { get; set; }
            public string make { get; set; }
            public string model { get; set; }
            public string trim { get; set; }
            public bool paging { get; set; }
            public int page { get; set; }
            public int perPage { get; set; }
        }

        public string Get()
        {
            return "Hello World";
        }

        ///<summary>
        ///Get Distinct list of years
        ///</summary>
        [HttpPost]
        [Route("GetYearsDist")]
        public async Task<List<string>> GetYearsDist()
        {
            return await db.GetYearsDist();
        }

        [HttpPost]
        [Route("GetMakesDist")]
        public async Task<List<string>> GetMakesDist()
        {
            return await db.GetMakesDist();
        }

        [HttpPost]
        [Route("GetYearsByMakeDist")]
        public async Task<List<string>> GetYearsByMakeDist(apiparm input)
        {
            return await db.GetYearsByMakeDist(input.make);
        }

        [HttpPost]
        [Route("GetMakesByYear")]
        public async Task<List<string>> GetMakesByYear(apiparm input)
        {
            return await db.GetMakesByYear(input.year);
        }

        [HttpPost]
        [Route("GetMakesByYearDist")]
        public async Task<List<string>> GetMakesByYearDist(apiparm input)
        {
            return await db.GetMakesByYearDist(input.year);
        }

        [HttpPost]
        [Route("GetModelsByYearMakeDist")]
        public async Task<List<string>> GetModelsByYearMakeDist(apiparm input)
        {
            return await db.GetModelsByYearMakeDist(input.year, input.make);
        }

        [HttpPost]
        [Route("GetTrimsByYearMakeModelDist")]
        public async Task<List<string>> GetTrimsByYearMakeModelDist(apiparm input)
        {
            return await db.GetTrimsByYearMakeModelDist(input.year, input.make, input.model);
        }


        [HttpPost]
        [Route("GetCarsByYear")]
        public async Task<List<Car>> GetCarsByYear(apiparm input)
        {
            return await db.GetCarsByYear(input.year);
        }

        [HttpPost]
        [Route("GetCarsByYearMake")]
        public async Task<List<Car>> GetCarsByYearMake(apiparm input)
        {
            return await db.GetCarsByYearMake(input.year, input.make);
        }

        [HttpPost]
        [Route("GetCarsByYearMakeModel")]
        public async Task<List<Car>> GetCarsByYearMakeModel(apiparm input)
        {
            return await db.GetCarsByYearMakeModel(input.year, input.make, input.model);
        }

        [HttpPost]
        [Route("GetCarsByYearMakeModelTrim")]
        public async Task<List<Car>> GetCarsByYearMakeModelTrim(apiparm input)
        {
            return await db.GetCarsByYearMakeModelTrim(input.year, input.make, input.model, input.trim);
        }

        [HttpPost]
        [Route("GetVariableCars")]
        public async Task<List<Car>> GetVariableCars(apiparm input)
        {
            List<Car> data = new List<Car>();
            if (!string.IsNullOrWhiteSpace(input.year))
            {
                data = await db.GetVariableCars(input.year, input.make, input.model, input.trim, input.paging, input.page, input.perPage);
            }
            else
            {
                if(!string.IsNullOrWhiteSpace(input.make) && string.IsNullOrWhiteSpace(input.model) && string.IsNullOrWhiteSpace(input.trim))
                {
                    data = await db.GetCarsByMake(input.make, input.paging, input.page, input.perPage);
                }
            }
            return data;
        }

        [HttpPost]
        [Route("GetCount")]
        public async Task<int> GetCount(apiparm input)
        {
            return await db.GetCount(input.year, input.make, input.model, input.trim);
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
