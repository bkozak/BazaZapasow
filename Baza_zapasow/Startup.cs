using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Baza_zapasow.Startup))]
namespace Baza_zapasow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
