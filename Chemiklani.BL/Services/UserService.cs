using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
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
                            var result = userManager.ResetPassword(user.Id,
                                userManager.GeneratePasswordResetToken(user.Id), data.Password);
                            if (!result.Succeeded)
                            {
                                throw new DataException("Heslo je příliš krátké.");
                            }
                            userManager.Update(user);
                            dc.SaveChanges();
                        }
                        else
                        {
                            throw new AuthenticationException("Špatné uživatelské jméno nebo heslo.");
                        }
                    }
                    return userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
        }

        public void AddNewUser(UserDto data)
        {
            AddNewUser(data, CreateRandomPassword());
        }

        public void AddNewUser(UserDto data, string password)
        {
            using (var dc = CreateDbContext())
            {
                using (var userManager = CreateUserManager(dc))
                {
                    var user = new AppUser
                    {
                        UserName = data.UserName,
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
                        roleManager.Create(new AppRole {Name = role.ToString()});
                    }
                    dc.SaveChanges();
                }
            }
        }

        public List<UserDto> GetUsers(string currentAdminUserName)
        {
            using (var dc = CreateDbContext())
            {
                using (var userManager = CreateUserManager(dc))
                {
                    var result = userManager.Users.Select(u => new UserDto
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                        })
                        .Where(u => u.UserName != currentAdminUserName)
                        .ToList();
                    result.ForEach(dto => dto.IsAdmin = userManager.IsInRole(dto.Id, UserRoles.Admin.ToString()));
                    return result;
                }
            }
        }

        private static string CreateRandomPassword()
        {
            var _allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789-";
            var randNum = new Random((int) DateTime.Now.Ticks);
            var chars = new char[8];

            for (var i = 0; i < 8; i++)
            {
                chars[i] = _allowedChars[randNum.Next(_allowedChars.Length)];
            }
            return new string(chars);
        }

        private UserManager<AppUser, int> CreateUserManager(AppDbContext dc)
        {
            return new UserManager<AppUser, int>(
                new UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>(dc))
            {
                UserTokenProvider = new TotpSecurityStampBasedTokenProvider<AppUser, int>()
            };
        }

        private RoleManager<AppRole, int> CreateRoleManager(AppDbContext dc)
        {
            return new RoleManager<AppRole, int>(new RoleStore<AppRole, int, AppUserRole>(dc));
        }

        public void DeleteUser(int id)
        {
            using (var dc = CreateDbContext())
            {
                using (var userManager = CreateUserManager(dc))
                {
                    userManager.Delete(userManager.FindById(id));
                    dc.SaveChanges();
                }
            }
        }

        public void ChangeAdmin(int id, bool isAdmin)
        {
            using (var dc = CreateDbContext())
            {
                using (var userManager = CreateUserManager(dc))
                {
                    if (isAdmin)
                    {
                        userManager.AddToRole(id, UserRoles.Admin.ToString());
                    }
                    else
                    {
                        userManager.RemoveFromRole(id, UserRoles.Admin.ToString());
                    }
                    dc.SaveChanges();
                }
            }
        }
    }
}