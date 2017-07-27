using System;
using Chemiklani.BL.Exceptions;
using DotVVM.Framework.Controls.Bootstrap;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
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

#if !DEBUG            
            catch (Exception ex)
            {
                var telemetry = new TelemetryClient();
                SetError("V aplikaci došlo k neznámé chybì. Chyba byla zaznamenána, ale mùžete kontaktovat správce a popsat mu jak k ní došlo.");
                telemetry.TrackException(ex);
                return false;
            }
#endif
        }

        public virtual void DialogAction()
        {
            IsDialogDisplayed = false;
        }
    }
}