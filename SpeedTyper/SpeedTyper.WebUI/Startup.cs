using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartupAttribute(typeof(SpeedTyper.WebUI.Startup))]
namespace SpeedTyper.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Add DI for TestHub
            GlobalHost.DependencyResolver.Register(typeof(Hubs.TestHub),() => new Hubs.TestHub(new LogicLayer.TestManager(),
                                                                                               new LogicLayer.UserManager(), 
                                                                                               new LogicLayer.LevelManager()
                                                                                               ));
            app.MapSignalR();
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;
        }
    }
}
