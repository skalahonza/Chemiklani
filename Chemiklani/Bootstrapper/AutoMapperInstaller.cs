using System.Reflection;
using iPodnik.BL.Mapping;
using Autofac;

namespace iPodnik.Web.Bootstrapper
{
    public class AutoMapperInstaller
    {
        public static void Install(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMapping).GetTypeInfo().Assembly)
                .AssignableTo<IMapping>()
                .As<IMapping>()
                .SingleInstance();
        }
    }
}