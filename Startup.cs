using Microsoft.Owin;
using MVCDocterPatientSystem.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCDocterPatientSystem.Startup))]
namespace MVCDocterPatientSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationDbContext.Initialize(context);
            ConfigureAuth(app);
        }
    }


}
