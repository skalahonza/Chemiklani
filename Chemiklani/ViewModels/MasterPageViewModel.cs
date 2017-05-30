using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
	public abstract class MasterPageViewModel : DotvvmViewModelBase
	{
	    public abstract string PageTitle { get; }
	    public string AppName => "Chemiklání";
	    public string CurrentRoute => Context.Route.RouteName;
    }
}

