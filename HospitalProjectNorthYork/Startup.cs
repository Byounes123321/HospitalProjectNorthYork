using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalProjectNorthYork.Startup))]
namespace HospitalProjectNorthYork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
