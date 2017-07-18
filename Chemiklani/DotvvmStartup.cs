using DotVVM.Framework.Configuration;
using DotVVM.Framework.Controls.Bootstrap;


namespace Chemiklani
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.DefaultCulture = "cs-CZ";

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/Default.dothtml");
            config.RouteTable.Add("Teams", "tymy", "Views/Teams.dothtml");
            config.RouteTable.Add("Tasks", "ulohy", "Views/Tasks.dothtml");
            config.RouteTable.Add("SignIn", "sign-in", "Views/SignIn.dothtml");
            config.RouteTable.Add("Users", "users", "Views/Users.dothtml");
            config.RouteTable.Add("Score", "score", "Views/Score.dothtml");

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.AddBootstrapConfiguration(new DotvvmBootstrapOptions
            {
                BootstrapJsUrl = "~/lib/bootstrap/dist/js/bootstrap.min.js",
                BootstrapCssUrl = "~/lib/bootstrap/dist/css/bootstrap.min.css",
                IncludeBootstrapResourcesInPage = true,
                IncludeJQueryResourceInPage = true,
                JQueryUrl = "~/lib/jquery/dist/jquery.js"
            });

            // register code-only controls and markup controls
            config.Markup.AddMarkupControl("cc", "Menu", "Controls/Menu.dotcontrol");            
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {

        }
    }
}
