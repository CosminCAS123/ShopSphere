using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Helpers
{
    public static class ErrorResources
    {
        public static class Register
        {
            public const string GoodField = "This field is good to go.";
            public const string EmptyField = "This field can't be empty.";
            public const string UppercaseStart = "This field must start with uppercase.";
            public const string OnlyLetters = "This field must only contain letters.";
            public const string RealNameLength = "This field must have at least 3 characters.";
            public const string InvalidEmailAdress = "Invalid adress entered.";
            public const string NotUniqueEmailAdress = "This email adress is already in use.";
            public const string NotUniqueUsername = "This username is already in use.";
            public const string AgeLimitBottom = "You must be at least 18.";
            public const string UsernameAlreadyExists = "Username already exists.";
            public const string RegisteredSuccessfully = "User registered successfully!";
            public const string InvalidPhoneNumber = "Phone number is not valid.";
            public const string PasswordLength = "Password must be at least 6 characters long.";
            public const string PasswordSpecialCharacters = "Password must contain a special character.";
            public const string PasswordNoNumbers = "Password must contain at least a number.";
            public const string UpperCaseLetter = " This field must have at least one uppercase letter";
            public const string OnlyDigits = "This field must only contain digits";
            public const string AtLeastOneDigit = "This field must contain at least one digit";
            public const string LowerCaseLetter = "This field must contain at least one lowercase letter";
            public const string PinLength = "Security PIN must be 4-digit.";
            public const string EmailAdressNotFound = "This email adress was not registered yet.";
            public const string PinDoesNotMatch = "The security pin is incorrect.";

        
        }


    }
}
