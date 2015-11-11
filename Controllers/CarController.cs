using Carlister.Models;
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
    }
}
