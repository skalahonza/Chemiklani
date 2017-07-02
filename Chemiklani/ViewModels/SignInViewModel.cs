using System;
using System.Data;
using System.Security.Authentication;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
	public class SignInViewModel : DotvvmViewModelBase
	{
	    public SignInDTO SignInData { get; set; } = new SignInDTO();
	    public string ErrorMessage { get; set; }

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
	        catch (AuthenticationException e)
	        {
	            ErrorMessage = e.Message;
	        }

	        catch (DataException e)
	        {
	            ErrorMessage = e.Message;
            }
	    }
	}
}

