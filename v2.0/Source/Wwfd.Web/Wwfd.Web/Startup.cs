using Microsoft.Owin;
using Owin;
using Wwfd.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Wwfd.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

			//here is my changes
        }
    }
}
