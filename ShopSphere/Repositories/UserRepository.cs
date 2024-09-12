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
    }
}
