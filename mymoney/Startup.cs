using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mymoney.Startup))]
namespace mymoney
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
