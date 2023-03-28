using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalProjectNorthYork.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
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

        public DbSet<Appointment> Appointments { get; set; }
        //Create a table in the database called Appointments
        public DbSet<Department> Departments { get; set; }
        //Create a table in the database called Departments
        public DbSet<Feedbacks> Feedbacks { get; set; }
        //Create a table in the database called Feedbacks
        public DbSet<Doctors> Doctors { get; set; }
        //Create a table in the database called Doctors
        public DbSet<Visit> Visits { get; set; }
        //Create table named visits in db
        public DbSet<FAQ> FAQS { get; set; }
        //Create table named FAQS in db
        public DbSet<Location> Locations { get; set; }
        //Create table named Locations in db

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}