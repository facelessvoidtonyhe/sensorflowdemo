using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SensorflowDemo.Startup))]
namespace SensorflowDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
