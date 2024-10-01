using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShopSphere.Helpers
{
    public static  class FieldVerification
    {
        /// <summary>
        /// Verifies email format
        /// </summary>
        /// <param name="email"> </param>
        /// <returns>returns the corresponding ErrorResources validation string.</returns>
        public static string EmailAdressFormat(string email)
        {
            
            if (string.IsNullOrEmpty(email)) return ErrorResources.Register.EmptyField;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, pattern)) return ErrorResources.Register.InvalidEmailAdress;

            return ErrorResources.Register.GoodField;
       }
        /// <summary>
        /// Verifies age format
        /// </summary>
        /// <param name="age"></param>
        /// <returns>returns corresponding ErrorResources validation string.</returns>
        public static string AgeFormat(string age)
        {
            if (string.IsNullOrEmpty(age))  return ErrorResources.Register.EmptyField;
            
            if (!age.All(char.IsDigit)) return ErrorResources.Register.OnlyDigits; 

            var c = int.Parse(age);
            if (c < 18) return ErrorResources.Register.AgeLimitBottom;

            return ErrorResources.Register.GoodField;
        }
        /// <summary>
        /// Verifies password format
        /// </summary>
        /// <param name="pass"></param>
        /// <returns>returns corresponding ErrorResrouces validation string.</returns>
        public static string PasswordFormat(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return ErrorResources.Register.EmptyField;

            if (pass.Length < 6) return ErrorResources.Register.PasswordLength;

            if (!pass.Any(char.IsUpper)) return ErrorResources.Register.UpperCaseLetter;

            if (!pass.Any(char.IsLower)) return ErrorResources.Register.LowerCaseLetter;

            if (!pass.Any(char.IsDigit)) return ErrorResources.Register.AtLeastOneDigit;

            string specialCharPattern = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""',.<>?/]";
            bool containsSpecialChar = Regex.IsMatch(pass, specialCharPattern);
            if (!Regex.IsMatch(pass, specialCharPattern)) return ErrorResources.Register.PasswordSpecialCharacters;

            return ErrorResources.Register.GoodField;
        }
    }
}
