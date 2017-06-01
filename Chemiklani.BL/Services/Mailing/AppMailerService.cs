using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Services.Mailing;

namespace iPodnik.BL.Services.Mailing
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
