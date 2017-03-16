using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataTrans.Startup))]
namespace DataTrans
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
