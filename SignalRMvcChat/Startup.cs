using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRMvcChat.Startup))]

namespace SignalRMvcChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new MyIdProvider());
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}
