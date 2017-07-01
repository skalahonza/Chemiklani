using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
	public abstract class MasterPageViewModel : DotvvmViewModelBase
	{
	    public abstract string PageTitle { get; }

        public abstract string PageDescription { get; }
	    public string AppName => "Chemikl�n�";
	    public string CurrentRoute => Context.Route.RouteName;
    }
}

