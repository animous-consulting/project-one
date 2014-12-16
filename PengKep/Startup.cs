using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PengKep.Startup))]
namespace PengKep
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
