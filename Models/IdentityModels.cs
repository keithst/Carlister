using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Carlister.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        public DbSet<Car> Cars { get; set;}



        public async Task<List<string>> GetYearsDist()
        {
            return await this.Database.SqlQuery<string>("GetYearsDist").ToListAsync();
        }

        public async Task<List<string>> GetMakesDist()
        {
            return await this.Database.SqlQuery<string>("GetMakesDist").ToListAsync();
        }

        public async Task<List<string>> GetMakesByYear(string year)
        {
            var yearParm = new SqlParameter("@year", year);

            return await this.Database.SqlQuery<string>("GetMakesByYear @year", yearParm).ToListAsync();
        }

        public async Task<List<string>> GetYearsByMakeDist(string make)
        {
            var makeParm = new SqlParameter("@make", make);

            return await this.Database.SqlQuery<string>("GetYearsByMakeDist @make", makeParm).ToListAsync();
        }

        public async Task<List<string>> GetMakesByYearDist(string year)
        {
            var yearParm = new SqlParameter("@year", year);

            return await this.Database.SqlQuery<string>("GetMakesByYearDist @year", yearParm).ToListAsync();
        }

        public async Task<List<string>> GetModelsByYearMakeDist(string year, string make)
        {
            var yearParm = new SqlParameter("@year", year);
            var makeParm = new SqlParameter("@make", make);

            return await this.Database.SqlQuery<string>("GetModelsByYearMakeDist @year, @make", yearParm, makeParm).ToListAsync();
        }

        public async Task<List<string>> GetTrimsByYearMakeModelDist(string year, string make, string model)
        {
            var yearParm = new SqlParameter("@year", year);
            var makeParm = new SqlParameter("@make", make);
            var modelParm = new SqlParameter("@model", model);

            return await this.Database.SqlQuery<string>("GetTrimsByYearMakeModelDist @year, @make, @model", yearParm, makeParm, modelParm).ToListAsync();
        }

        public async Task<List<Car>> GetCarsByYear(string year)
        {
            var yearParm = new SqlParameter("@year", year);

            return await this.Database.SqlQuery<Car>("GetCarsByYear @year", yearParm).ToListAsync();
        }

        public async Task<List<Car>> GetCarsByYearMake(string year, string make)
        {
            var yearParm = new SqlParameter("@year", year);
            var makeParm = new SqlParameter("@make", make);

            return await this.Database.SqlQuery<Car>("GetCarsByYearMake @year, @make", yearParm, makeParm).ToListAsync();
        }
        
        public async Task<List<Car>> GetCarsByYearMakeModel(string year, string make, string model)
        {
            var yearParm = new SqlParameter("@year", year);
            var makeParm = new SqlParameter("@make", make);
            var modelParm = new SqlParameter("@model", model);

            return await this.Database.SqlQuery<Car>("GetCarsByYearMakeModel @year, @make, @model", yearParm, makeParm, modelParm).ToListAsync();
        }

        public async Task<List<Car>> GetCarsByYearMakeModelTrim(string year, string make, string model, string trim)
        {
            var yearParm = new SqlParameter("@year", year);
            var makeParm = new SqlParameter("@make", make);
            var modelParm = new SqlParameter("@model", model);
            var trimParm = new SqlParameter("@trim", trim);

            return await this.Database.SqlQuery<Car>("GetCarsByYearMakeModelTrim @year, @make, @model, @trim", yearParm, makeParm, modelParm, trimParm).ToListAsync();
        }

        public async Task<List<Car>> GetVariableCars(string year, string make, string model, string trim)
        {
            var yearParm = new SqlParameter("@year", year);
            var makeParm = new SqlParameter("@make", make);
            var modelParm = new SqlParameter("@model", model);
            var trimParm = new SqlParameter("@trim", trim);

            var stored = "GetVariableCars "; 
            bool notfirst = false;
            List<SqlParameter> storedparm = new List<SqlParameter>();
            if(!string.IsNullOrWhiteSpace(year))
            {
                if(notfirst)
                {
                    stored += ", ";
                }
                stored += "@year";
                storedparm.Add(yearParm);
                notfirst = true;
            }
            if (!string.IsNullOrWhiteSpace(make))
            {
                if (notfirst)
                {
                    stored += ", ";
                }
                stored += "@make";
                storedparm.Add(makeParm);
                notfirst = true;
            }
            if (!string.IsNullOrWhiteSpace(model))
            {
                if (notfirst)
                {
                    stored += ", ";
                }
                stored += "@model";
                storedparm.Add(modelParm);
                notfirst = true;
            }
            if (!string.IsNullOrWhiteSpace(trim))
            {
                if (notfirst)
                {
                    stored += ", ";
                }
                stored += "@trim";
                storedparm.Add(trimParm);
                notfirst = true;
            }

            return await this.Database.SqlQuery<Car>(stored, storedparm.ToArray()).ToListAsync();
        }

    }
}