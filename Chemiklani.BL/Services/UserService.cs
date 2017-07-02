using System;
using System.Security.Claims;
using Chemiklani.BL.DTO;
using Chemiklani.DAL;
using Chemiklani.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.BL.Services
{
    public class UserService : BaseService
    {
        public enum UserRoles
        {
            User,
            Admin
        }

        public ClaimsIdentity SignIn(SignInDTO data)
        {
            using (var dc = CreateDbContext())
            {
                using (var userManager = CreateUserManager(dc))
                {
                    var user = userManager.Find(data.UserName, data.Password);
                    if (user == null)
                    {
                        // if the user doesn't have the password and signs in for the first time, save it
                        user = userManager.FindByName(data.UserName);
                        if (user != null && !userManager.HasPassword(user.Id))
                        {
                            var result = userManager.ResetPassword(user.Id, userManager.GeneratePasswordResetToken(user.Id), data.Password);
                            if (!result.Succeeded)
                            {
                                throw new Exception("The password is too short!");
                            }
                            userManager.Update(user);
                            dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Invalid user name or password!");
                        }
                    }
                    return userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
        }

        public void AddNewUser(RegisterNewUserDTO data)
        {
            AddNewUser(data, CreateRandomPassword());
        }

        public void AddNewUser(RegisterNewUserDTO data, string password)
        {
            using (var dc = CreateDbContext())
            {
                using (var userManager = CreateUserManager(dc))
                {
                    var user = new AppUser
                    {
                        UserName = data.UserName,  
                        Email = data.Email
                    };
                    
                    var check = userManager.Create(user, password);

                    //User created
                    if (check.Succeeded)
                    {
                        userManager.AddToRole(user.Id,
                            data.IsAdmin ? UserRoles.Admin.ToString() : UserRoles.User.ToString());
                    }
                }
            }
        }

        public void AddRole(UserRoles role)
        {
            using (var dc = CreateDbContext())
            {
                using (var roleManager = CreateRoleManager(dc))
                {
                    if (!roleManager.RoleExists(role.ToString()))
                    {
                        roleManager.Create(new AppRole { Name = role.ToString() });
                    }
                    dc.SaveChanges();
                }
            }
        }

        private static string CreateRandomPassword()  //If you are always going to want 8 characters then there is no need to pass a length argument
        {
            var _allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789-";
            var randNum = new Random((int)DateTime.Now.Ticks);
            var chars = new char[8];

            for (var i = 0; i < 8; i++)
            {
                chars[i] = _allowedChars[randNum.Next(_allowedChars.Length)];
            }
            return new string(chars);
        }

        private UserManager<AppUser, int> CreateUserManager(AppDbContext dc)
        {
            return new UserManager<AppUser, int>(new UserStore<AppUser,AppRole,int,AppUserLogin,AppUserRole,AppUserClaim>(dc))
            {
                UserTokenProvider = new TotpSecurityStampBasedTokenProvider<AppUser, int>()
            };
        }

        private RoleManager<AppRole, int> CreateRoleManager(AppDbContext dc)
        {
            return new RoleManager<AppRole, int>(new RoleStore<AppRole,int,AppUserRole>(dc));
        }
    }
}