using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;
using ShopSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ShopSphereContext context;
        public UserRepository(ShopSphereContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
           var user =  await this.context.Users.FirstOrDefaultAsync(x => x.Username == username);
           return user;
        }

        public async Task AddUserAsync(User user)
        {
            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            bool is_registered = await this.context.Users.AnyAsync(user => user.EmailAdress == email);
            return is_registered;
        }

        public async Task<bool> IsPhoneNumberRegisteredAsync(string phoneNumber)
        {
            bool is_registered = await this.context.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
            return is_registered;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.EmailAdress == email);
            return user;
        }

        public async Task ChangeUserPassword(User user, string hash)
        {
            user.PasswordHash = hash;
            await this.context.SaveChangesAsync();
        }
    }
}
