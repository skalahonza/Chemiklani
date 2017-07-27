using System;
using Chemiklani.BL.Exceptions;
using DotVVM.Framework.Controls.Bootstrap;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.ViewModel;
using Microsoft.ApplicationInsights;

namespace Chemiklani.ViewModels
{
    [Authorize]
	public abstract class MasterPageViewModel : SafeViewModel
    {
	    public abstract string PageTitle { get; }
        public abstract string PageDescription { get; }
	    public string AppName => "Chemiklání";
	    public string CurrentRoute => Context.Route.RouteName;
	    public string CurrentUserName => Context.GetAuthentication().User.Identity.Name;
    }

    public class SafeViewModel : DotvvmViewModelBase
    {
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

        protected bool ExecuteSafe(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (DotvvmInterruptRequestExecutionException)
            {
                throw;
            }

            catch (AppLogicException e)
            {
                SetError(e.Message);
                return false;
            }

            catch (Exception ex)
            {
                var telemetry = new TelemetryClient();
                SetError("V aplikaci došlo k neznámé chybì. Chyba byla zaznamenána, ale mùžete kontaktovat správce a popsat mu jak k ní došlo.");
                telemetry.TrackException(ex);
                return false;
            }
        }

        public virtual void DialogAction()
        {
            IsDialogDisplayed = false;
        }
    }
}

