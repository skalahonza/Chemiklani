using System.Collections.Generic;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace Chemiklani.ViewModels
{
    [Authorize(Roles = new[] { "Admin" })]
    public class UsersViewModel : MasterPageViewModel
	{
	    public override string PageTitle => "U�ivatel�";
	    public override string PageDescription => "Spr�va u�ivatel�. U�ivatele lze libovoln� p�id�vat a odeb�rat.";

	    public List<UserDto> Users { get; set; } = new List<UserDto>();
	    public AddUserDTO NewUser { get; set; } = new AddUserDTO();

        private readonly UserService service = new UserService();

	    public override Task PreRender()
	    {
            Users = service.GetUsers(CurrentUserName);
	        return base.PreRender();
	    }

	    public void AddUser()
	    {
	        if (NewUser.Password == NewUser.PasswordConfirm)
	        {
	            var result = service.AddNewUser(NewUser,NewUser.Password);
	            NewUser = new AddUserDTO();
	        }
	    }

	    public void DeleteUser(int id)
	    {
	        service.DeleteUser(id);
	    }

	    public void ChangeAdmin(int id, bool isAdmin)
	    {
	        service.ChangeAdmin(id, isAdmin);
	    }
	}
}

