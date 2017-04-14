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

            // Add DI for TestHub
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver.Register(typeof(Hubs.TestHub),() => new Hubs.TestHub(new LogicLayer.TestManager()));
            app.MapSignalR();
        }
    }
}
