using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
	public class TasksViewModel : MasterPageViewModel
	{
	    public override string PageTitle => "Úlohy";
	    public override string PageDescription => "Správa úloh a jejich bodového ohodnocení.";
	}
}

