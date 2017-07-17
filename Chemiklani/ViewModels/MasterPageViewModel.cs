using DotVVM.Framework.Controls.Bootstrap;
using DotVVM.Framework.Hosting;
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
	    public string CurrentUserName => Context.GetAuthentication().User.Identity.Name;

	    public bool IsDialogDisplayed { get; set; }
	    [Bind(Direction.ServerToClient)]
	    public string AlertText { get; set; }

	    [Bind(Direction.ServerToClient)]
	    public AlertType AlertType { get; set; }

        public void SignOut()
	    {
	        Context.GetAuthentication().SignOut();
            Context.RedirectToRoute("SignIn");
	    }

	    protected void SetSuccess(string message)
	    {
	        IsDialogDisplayed = true;
	        AlertText = message;
	        AlertType = AlertType.Success;
        }

	    protected void SetError(string message)
	    {
	        IsDialogDisplayed = true;
	        AlertText = message;
	        AlertType = AlertType.Danger;
	    }

	    protected void SetWarning(string message)
	    {
	        IsDialogDisplayed = true;
	        AlertText = message;
	        AlertType = AlertType.Warning;
	    }

	    public virtual void DialogAction()
	    {
	        IsDialogDisplayed = false;
	    }
    }
}

