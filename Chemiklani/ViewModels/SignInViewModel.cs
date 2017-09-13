using Chemiklani.BL.DTO;
using Chemiklani.BL.Exceptions;
using Chemiklani.BL.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
	public class SignInViewModel : DotvvmViewModelBase
	{
	    public SignInDTO SignInData { get; set; } = new SignInDTO();
	    public string ErrorMessage { get; set; }

        /// <summary>
        /// Sign in button clicked, redirect when sign in successful, otherwise display errors
        /// </summary>
	    public void SignIn()
	    {
	        ErrorMessage = "";
	        var service = new UserService();
	        try
	        {
	            var claim = service.SignIn(SignInData);
	            Context.GetAuthentication().SignIn(claim);
	            Context.RedirectToRoute("Default");
	        }
	        catch (AppLogicException e)
	        {
	            ErrorMessage = e.Message;
	        }
	    }
	}
}

