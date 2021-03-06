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
            config.RouteTable.Add("Users", "uzivatele", "Views/Users.dothtml");
            config.RouteTable.Add("Score", "hodnoceni", "Views/Score.dothtml");
            config.RouteTable.Add("Results", "vysledky/{Room}", "Views/Results.dothtml");
            config.RouteTable.Add("AllResults", "vysledky", "Views/Results.dothtml");

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddMarkupControl("cc", "Menu", "Controls/Menu.dotcontrol");            
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.AddBootstrapConfiguration(new DotvvmBootstrapOptions
            {
                BootstrapJsUrl = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js",
                BootstrapCssUrl = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css",
                IncludeBootstrapResourcesInPage = true,
                IncludeJQueryResourceInPage = true,
                JQueryUrl = "https://code.jquery.com/jquery-3.2.1.js"
            });
        }
    }
}
