using ECommerceCS.DAL.Entities;
using ECommerceCS.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerceCS.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);

        Task LogOutAsync();
    }
}
