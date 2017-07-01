using System.Reflection;
using Autofac;
using Autofac.Core;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using iPodnik.BL.Services.Web;
using DotVVM.Framework.Configuration;
using Microsoft.Extensions.Options;

namespace iPodnik.Web.Bootstrapper {
    public class WebInstaller {
        public static void Install(ContainerBuilder builder) {
            builder.RegisterAssemblyTypes(typeof(WebInstaller).GetTypeInfo().Assembly)
                .AssignableTo<DotvvmViewModelBase>()
                .PropertiesAutowired(new DefaultPropertySelector(preserveSetValues: true))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(WebInstaller).GetTypeInfo().Assembly)
                .AssignableTo<IDotvvmPresenter>()
                .InstancePerDependency();

            builder.Register(c =>
            {
                var options = c.Resolve<IOptions<AppConfig>>().Value;
                return new WebRouteBuilder(options.BaseUrl, c.Resolve<DotvvmConfiguration>());
            })
                .As<IWebRouteBuilder>()
                .SingleInstance();

            // note: uncomment if using identity
            //builder.RegisterType<CurrentUserProvider>()
            //    .As<ICurrentUserProvider<int>>()
            //    .SingleInstance();

        }
    }
}