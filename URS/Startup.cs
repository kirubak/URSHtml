using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(URS.Startup))]
namespace URS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
