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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        public DbSet<Car> Cars { get; set;}



        public async Task<List<string>> GetYears()
        {
            return await this.Database.SqlQuery<string>("GetYears").ToListAsync();
        }

        public async Task<List<string>> GetMakesByYearDist(string year)
        {
            var yearParm = new SqlParameter("@year", year);

            return await this.Database.SqlQuery<string>("GetMakesByYearDist @year", yearParm).ToListAsync();
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
    }
}