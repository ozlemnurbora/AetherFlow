using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AetherFlow.Startup))]
namespace AetherFlow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
