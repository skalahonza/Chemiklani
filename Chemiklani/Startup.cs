using System.Web.Hosting;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Chemiklani.Startup))]
namespace Chemiklani
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;

            //Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/sign-in"),
                Provider = new CookieAuthenticationProvider
                {
                    OnApplyRedirect = context =>
                    {
                        DotvvmAuthenticationHelper.ApplyRedirectResponse(context.OwinContext, context.RedirectUri);
                    }
                }
            });

            //Add default users and roles
            var userService = new UserService(); 
            userService.AddRole(UserService.UserRoles.User);
            userService.AddRole(UserService.UserRoles.Admin);

            userService.AddNewUser(new RegisterNewUserDTO {UserName = "normal"},"normal");
            userService.AddNewUser(new RegisterNewUserDTO {UserName = "admin",IsAdmin = true},"password");

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, options: options =>
            {
                options.AddDefaultTempStorages("temp");
            });
#if !DEBUG
            dotvvmConfiguration.Debug = false;
#endif

            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)
            });
        }
    }
}
