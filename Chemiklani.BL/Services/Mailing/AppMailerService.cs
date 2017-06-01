using Chemiklani.BL.Services.Web;
using Riganti.Utils.Infrastructure.Services.Mailing;

namespace Chemiklani.BL.Services.Mailing
{
    public class AppMailerService : MailerService
    {
        private readonly IWebRouteBuilder routeBuilder;

        public AppMailerService(IMailSender sender, IWebRouteBuilder routeBuilder) : base(sender)
        {
            this.routeBuilder = routeBuilder;
        }
    }
}
