using ShopSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<bool> IsPhoneNumberRegisteredAsync(string phoneNumber);
    }
}
