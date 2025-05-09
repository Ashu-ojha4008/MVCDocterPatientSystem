using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCDocterPatientSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool IsBlocked {  get; set; }
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
            : base("CleanDoctor", throwIfV1Schema: false)
        {
        }
        public DbSet<PatientInfo> patientInfos { get; set; }
        public DbSet<Appointment> appointments { get; set; }
     
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public static void Initialize(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Ensure roles are created
            if (!roleManager.RoleExists("doctor"))
            {
                roleManager.Create(new IdentityRole("doctor"));
            }

            if (!roleManager.RoleExists("patient"))
            {
                roleManager.Create(new IdentityRole("patient"));
            }

            // Ensure default doctor user is created
            var doctorEmail = "doctor@demo.com";
            var doctorPassword = "123456";
            var doctorUser = userManager.FindByEmail(doctorEmail);

            if (doctorUser == null)
            {
                doctorUser = new ApplicationUser { UserName = doctorEmail, Email = doctorEmail };
                var result = userManager.Create(doctorUser, doctorPassword);

                if (result.Succeeded)
                {
                    userManager.AddToRole(doctorUser.Id, "doctor");
                }
            }
        }

        public System.Data.Entity.DbSet<MVCDocterPatientSystem.Models.RegisterViewModel> RegisterViewModels { get; set; }

        //public System.Data.Entity.DbSet<MVCDocterPatientSystem.Models.appointment> appointments { get; set; }

        //public System.Data.Entity.DbSet<MVCDocterPatientSystem.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}