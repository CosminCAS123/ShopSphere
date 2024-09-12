using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public bool CompareHashes(string hash1, string hash2)
        {
            return hash1.Equals(hash2);
         }

        public async Task<string> GetPasswordHashAsync(string password)
        {
            return await Task.Run(() =>
            {
                // Create a SHA256 object
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Compute the hash as a byte array
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Convert the byte array to a string
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    return builder.ToString();
                }
            });
        }
    }
}
