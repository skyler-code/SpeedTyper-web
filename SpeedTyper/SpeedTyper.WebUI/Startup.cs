using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpeedTyper.WebUI.Startup))]
namespace SpeedTyper.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
