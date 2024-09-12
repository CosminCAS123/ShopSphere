using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Services
{
     public interface IPasswordHashService
    {
        Task<string> GetPasswordHashAsync(string password);
        bool CompareHashes(string hash1, string hash2);
    }
}
