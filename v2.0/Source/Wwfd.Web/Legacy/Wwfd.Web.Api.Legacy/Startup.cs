using Microsoft.Owin;
using Owin;
using Wwfd.Web.Api;

[assembly: OwinStartup(typeof(Startup))]

namespace Wwfd.Web.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
