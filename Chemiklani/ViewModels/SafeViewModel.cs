using System;
using Chemiklani.BL.Exceptions;
using DotVVM.Framework.Controls.Bootstrap;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Microsoft.ApplicationInsights;

namespace Chemiklani.ViewModels
{
    public class SafeViewModel : DotvvmViewModelBase
    {
        public bool IsDialogDisplayed { get; set; }
        [Bind(Direction.ServerToClient)]
        public string AlertText { get; set; }

        [Bind(Direction.ServerToClient)]
        public AlertType AlertType { get; set; }

        /// <summary>
        /// Sign out current user and redirect him to SignIn page
        /// </summary>
        public void SignOut()
        {
            Context.GetAuthentication().SignOut();
            Context.RedirectToRoute("SignIn");
        }

        /// <summary>
        /// Show success alert
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        protected void SetSuccess(string message)
        {
            IsDialogDisplayed = true;
            AlertText = message;
            AlertType = AlertType.Success;
        }

        /// <summary>
        /// Show error alert
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        protected void SetError(string message)
        {
            IsDialogDisplayed = true;
            AlertText = message;
            AlertType = AlertType.Danger;
        }

        /// <summary>
        /// Show warning alert
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        protected void SetWarning(string message)
        {
            IsDialogDisplayed = true;
            AlertText = message;
            AlertType = AlertType.Warning;
        }

        /// <summary>
        /// Tries to execute an action and if something goes wrong, it alerts the user and logs the exception
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
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
    }
}