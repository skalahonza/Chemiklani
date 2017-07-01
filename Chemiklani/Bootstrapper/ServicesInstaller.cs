using System;
using System.Reflection;
using iPodnik.BL.Services.Mailing;
using iPodnik.BL.Facades;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Services.Facades;
using Riganti.Utils.Infrastructure.Services.Mailing;

namespace iPodnik.Web.Bootstrapper
{
    public class ServicesInstaller
    {
        public static void Install(ContainerBuilder builder, IHostingEnvironment env)
        {
            builder.RegisterAssemblyTypes(typeof(AppFacadeBase).GetTypeInfo().Assembly)
                .AssignableTo<FacadeBase>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .InstancePerDependency();

            builder.Register(c => env.IsDevelopment() ? GetDevMailSender(c) : GetRealMailSender(c))
                .As<IMailSender>()
                .SingleInstance();
            
            builder.RegisterType<AppMailerService>()
                .SingleInstance();
        }

        private static IMailSender GetDevMailSender(IComponentContext c)
        {
            var options = c.Resolve<IOptions<AppConfig>>().Value;
            return new FileSystemMailSender(options.MailPickupDirectory);
        }

        private static IMailSender GetRealMailSender(IComponentContext c)
        {
            // todo:
            throw new NotImplementedException();
        }
    }
}
