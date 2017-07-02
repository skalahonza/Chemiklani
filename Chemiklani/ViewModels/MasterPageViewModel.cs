using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
    [Authorize]
	public abstract class MasterPageViewModel : DotvvmViewModelBase
	{
	    public abstract string PageTitle { get; }

        public abstract string PageDescription { get; }
	    public string AppName => "Chemiklání";
	    public string CurrentRoute => Context.Route.RouteName;
    }
}

