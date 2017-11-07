using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PICECAR.Startup))]
namespace PICECAR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
