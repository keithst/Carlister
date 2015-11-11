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

        [Route("GetYears")]
        public async Task<List<string>> GetYears()
        {
            return await db.GetYears();
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
    }
}
