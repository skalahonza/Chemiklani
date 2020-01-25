using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Exceptions;
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

        /// <summary>
        /// Tries to sign in a user
        /// </summary>
        /// <param name="data">Sign in data</param>
        /// <returns></returns>
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
                                throw new InvalidAppData("Password too short.");
                            }
                            userManager.Update(user);
                            dc.SaveChanges();
                        }
                        else
                        {
                            throw new AppAuthenticationException("Wrong username or password.");
                        }
                    }
                    return userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
        }

        /// <summary>
        /// Check if there are users present in the database
        /// </summary>
        /// <returns>False, when there is no user in the database</returns>
        public bool AnyUsers()
        {
            using (var dc = CreateDbContext())
            {
                using (var userManager = CreateUserManager(dc))
                {
                    return userManager.Users.Any();
                }
            }
        }

        /// <summary>
        /// Add new user into the database
        /// </summary>
        /// <param name="data">User data</param>
        public void AddNewUser(UserDto data)
        {
            AddNewUser(data, CreateRandomPassword());
        }

        /// <summary>
        /// Add new user, with a password into the database
        /// </summary>
        /// <param name="data">User data</param>
        /// <param name="password">User password</param>
        /// <returns></returns>
        public IdentityResult AddNewUser(UserDto data, string password)
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

                    return check;
                }
            }
        }

        /// <summary>
        /// Add a role to the database, used on app setup
        /// </summary>
        /// <param name="role">Role enum</param>
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

        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <param name="currentAdminUserName">This username will be skipped</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates random password ofr the user
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Delete user from the database
        /// </summary>
        /// <param name="id">Id to be deleted</param>
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

        /// <summary>
        /// Change user to the admin or back to the normal user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="isAdmin">True if the user should be changed to admin</param>
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