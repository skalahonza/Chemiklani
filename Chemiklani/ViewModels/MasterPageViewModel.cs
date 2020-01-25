using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;

namespace Chemiklani.ViewModels
{
    [Authorize]
	public abstract class MasterPageViewModel : SafeViewModel
    {
	    public abstract string PageTitle { get; }
        public abstract string PageDescription { get; }
	    public string AppName => "Chemistryrace";
	    public string CurrentRoute => Context.Route.RouteName;
	    public string CurrentUserName => Context.GetAuthentication().User.Identity.Name;
    }
}

