using System.Reflection;
using iPodnik.BL.Queries;
using iPodnik.BL.Queries.FirstLevel;
using iPodnik.BL.Repositories;
using iPodnik.DAL;
using Autofac;
using Microsoft.AspNetCore.Http;
using Riganti.Utils.Infrastructure.EntityFramework;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.AutoMapper;
using Riganti.Utils.Infrastructure.Services.Facades;
using System.Data.Entity;
using iPodnik.BL;
using iPodnik.BL.Queries.BasicQueries;
using iPodnik.BL.Queries.FilteredQueries;
using iPodnik.DAL.Entities;
using Microsoft.AspNet.Identity;


namespace iPodnik.Web.Bootstrapper
{
    public class DataAccessInstaller
    {
        public static void Install(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            builder.Register(c =>
                {
                    var options = c.Resolve<IOptions<AppConfig>>().Value;
                    return new AppDbContext(options.SqlConnectionString);
                })
                .As<DbContext>()
                .As<AppDbContext>()
                .InstancePerDependency();

            builder.Register(c => new AspNetCoreUnitOfWorkRegistry(c.Resolve<IHttpContextAccessor>(), new AsyncLocalUnitOfWorkRegistry()))
                .As<IUnitOfWorkRegistry>()
                .SingleInstance();

            builder.RegisterType<EntityFrameworkUnitOfWorkProvider>()
                .As<IUnitOfWorkProvider>()
                .SingleInstance();

            builder.RegisterGeneric(typeof(AppRepository<,>))
                .As(typeof(IRepository<,>))
                .SingleInstance();

            builder.RegisterType<UtcDateTimeProvider>()
                .As<IDateTimeProvider>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(AppRepository<,>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AppRepository<,>))
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(AppQueryBase<>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AppQueryBase<>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(AppFirstLevelQueryBase<,>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AppFirstLevelQueryBase<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(AppFilterQueryBase<,,>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AppFilterQueryBase<,,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(AppBasicQueryBase<,>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AppBasicQueryBase<,>))
                .InstancePerDependency();
            
            builder.RegisterGeneric(typeof(EntityDTOMapper<,>))
                .As(typeof(IEntityDTOMapper<,>))
                .SingleInstance();

            //User manager
            builder.RegisterType<AppUserManager>()
                .SingleInstance();

            builder.RegisterType<AppUserStore>()
                .As(typeof(IUserStore<AppUser, int>))
                .SingleInstance();
        }
    }
}
