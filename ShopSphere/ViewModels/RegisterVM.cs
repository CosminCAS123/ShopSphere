using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShopSphere.Helpers;
using ShopSphere.Repositories;
using ShopSphere.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels
{
    public class RegisterVM : AuthViewModelBase
    {

        private string firstName;
        private string lastName;
        private string emailAdress;
        private string age;
        private bool first_mark_enabled = false;
        private bool last_mark_enabled = false;
        private bool email_mark_enabled = false;
        private bool age_mark_enabled = false;
        private string first_tool_tip;
        private string last_tool_tip;
        private string email_tool_tip;
        private string age_tool_tip;
        private string password;
        private bool pass_mark_enabled = false;
        private string password_tool_tip;
        private bool is_email_error_visible = false;
        private IUserRepository user_repository;
        private IPasswordHashService password_hash_service;
        public string PasswordToolTip { get => this.password_tool_tip; set => this.RaiseAndSetIfChanged(ref this.password_tool_tip, value); }
        public string FirstToolTip { get => this.first_tool_tip; set => this.RaiseAndSetIfChanged(ref this.first_tool_tip, value); }
        public string LastToolTip { get => this.last_tool_tip; set => this.RaiseAndSetIfChanged(ref this.last_tool_tip, value); }
        public string EmailToolTip { get => this.email_tool_tip; set => this.RaiseAndSetIfChanged(ref this.email_tool_tip, value); }
        public string AgeToolTip { get => this.age_tool_tip; set => this.RaiseAndSetIfChanged(ref this.age_tool_tip, value); }
        public bool PasswordMarkEnabled { get => this.pass_mark_enabled; set => this.RaiseAndSetIfChanged(ref this.pass_mark_enabled, value); } 
        public bool FirstMarkEnabled { get => this.first_mark_enabled; set => this.RaiseAndSetIfChanged(ref this.first_mark_enabled, value); }
        public bool LastMarkEnabled { get => this.last_mark_enabled; set => this.RaiseAndSetIfChanged(ref this.last_mark_enabled, value); }
        public bool IsEmailErrorVisible { get => this.is_email_error_visible; set => this.RaiseAndSetIfChanged(ref this.is_email_error_visible, value); }

        public bool EmailMarkEnabled { get => this.email_mark_enabled; set => this.RaiseAndSetIfChanged(ref this.email_mark_enabled, value); }

        public bool AgeMarkEnabled { get => this.age_mark_enabled; set => this.RaiseAndSetIfChanged(ref this.age_mark_enabled, value); }
        public string FirstName { get => this.firstName; set => this.RaiseAndSetIfChanged(ref this.firstName, value); }
        public string LastName { get => this.lastName; set => this.RaiseAndSetIfChanged(ref this.lastName, value); }
        public string Password { get => this.password; set => this.RaiseAndSetIfChanged(ref this.password, value); }
        public string EmailAdress { get => this.emailAdress ; set => this.RaiseAndSetIfChanged(ref this.emailAdress , value); }
        public string Age { get => this.age; set => this.RaiseAndSetIfChanged(ref this.age  , value); }

        public ReactiveCommand<Unit , Unit> NextRegisterCommand { get; set; }



        public RegisterVM(IAuthNavigationService navigationService , IUserRepository userRepository , IPasswordHashService passwordHashService) : base(navigationService) 
        {
            this.password_hash_service = passwordHashService;
            this.NextRegisterCommand = ReactiveCommand.CreateFromTask(goToSecondRegister, Observable.Return(true)) ;
            this.user_repository = userRepository;
            this.IsEmailErrorVisible = false;
            SetFieldObservables();
            for (int i = 1; i < 6; i++) DisableMark(i);
        }
        private void EnableMark(int index , string errorMessage)
        {
            switch(index)
            {
                case 1: 
                    this.FirstToolTip = errorMessage;
                    this.FirstMarkEnabled = true;
                    break;
                case 2:
                    this.LastToolTip = errorMessage;
                    this.LastMarkEnabled = true;
                    break;
                case 3:
                    this.EmailToolTip = errorMessage;
                    this.EmailMarkEnabled = true;
                    break;
                case 4:
                    this.AgeToolTip = errorMessage;
                    this.AgeMarkEnabled = true;
                    break;
                case 5:
                    this.PasswordToolTip = errorMessage;
                    this.PasswordMarkEnabled = true;
                    break;
                default:
                    throw new NotSupportedException();


            }
        }
        private void DisableMark(int index)
        {
            switch (index)
            {
                case 1:
                    this.FirstMarkEnabled = false;
                    break;
                case 2:
                    this.LastMarkEnabled = false;
                    break;
                case 3:
                    this.EmailMarkEnabled = false;
                    break;
                case 4:
                    this.AgeMarkEnabled = false;
                    break;
                case 5:
                    this.PasswordMarkEnabled = false;
                    break;

                 default: throw new NotSupportedException();
            }
        }
        private void SetFieldObservables()
        {


            var firstnameobservable = this.WhenAnyValue(x => x.FirstName).
                Subscribe(x =>
                {
                var firstname = x;
              

                if (string.IsNullOrEmpty(firstname))
                {
                        DisableMark(1);
                        return;
                }
                if (firstname.Length < 3) 
                {

                        EnableMark(1, ErrorResources.Register.RealNameLength);
                        return;
                }
                if (!firstname.All(char.IsLetter))
                {
                        EnableMark(1, ErrorResources.Register.OnlyLetters);
                        return;
                }
                if (!char.IsUpper(firstname, 0))
                {
                        EnableMark(1, ErrorResources.Register.UppercaseStart);
                        return;
                }
                 DisableMark(1);
                });
            var lastnameobservable = this.WhenAnyValue(x => x.LastName).
                Subscribe(x => 
                {
                    var lastname = x;
                    if (string.IsNullOrEmpty(lastname))
                    {
                        DisableMark(2);
                        return;
                    }

                    if (lastname.Length < 3)
                    {
                        EnableMark(2, ErrorResources.Register.RealNameLength);return;
                    }

                    if (!lastname.All(char.IsLetter))
                    {
                        EnableMark(2, ErrorResources.Register.OnlyLetters);return;
                    }
                    if (!char.IsUpper(lastname , 0))
                    {
                        EnableMark(2 , ErrorResources.Register.UppercaseStart);return;
                    }

                    DisableMark(2);

                }
                );

            var emailadressobservable = this.WhenAnyValue(x => x.EmailAdress).
                Subscribe(x =>
                {
                    var emailadress = x;
                    if (string.IsNullOrEmpty(emailadress)) { DisableMark(3); return; }

                    string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    if (!Regex.IsMatch(emailadress, pattern)) { EnableMark(3, ErrorResources.Register.InvalidEmailAdress); return; }

                    DisableMark(3);


                });

            var ageobservable = this.WhenAnyValue(x => x.Age).
                Subscribe(x =>
                {
                    
                    if (string.IsNullOrEmpty(x)) {  DisableMark(4); return; }
                    if (!x.All(char.IsDigit)) { EnableMark(4, ErrorResources.Register.OnlyDigits);return; }

                    var age = int.Parse(x);

                    if (age < 18) { EnableMark(4, ErrorResources.Register.AgeLimitBottom); return; }


                    DisableMark(4);
                    

                });

            var passwordobservable = this.WhenAnyValue(x => x.Password).
                Subscribe(x =>
                {
                    if (string.IsNullOrEmpty(x)) { DisableMark(5);return; }
                    /*  -one uppercase letter
                     *  -min length
                     *  -one lowercase letter
                     *  -one special character
                     *  -one digit
                     
                     */

                    var pass = x;
                    if (pass.Length < 6) { EnableMark(5, ErrorResources.Register.PasswordLength); return; }
                    if (!pass.Any(char.IsUpper)) { EnableMark(5, ErrorResources.Register.UpperCaseLetter); return; }
                    if (!pass.Any(char.IsLower)) { EnableMark(5, ErrorResources.Register.LowerCaseLetter);return; }
                    if (!pass.Any(char.IsDigit)) { EnableMark(5 , ErrorResources.Register.AtLeastOneDigit); return; }
                    string specialCharPattern = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""',.<>?/]";

                    bool containsSpecialChar = Regex.IsMatch(pass, specialCharPattern);

                    if (!Regex.IsMatch(pass, specialCharPattern)) { EnableMark(5 , ErrorResources.Register.PasswordSpecialCharacters); return; }
                    DisableMark(5);
                    
                });
        }

        private async Task goToSecondRegister()
        {
           
            if (this.FirstMarkEnabled || this.LastMarkEnabled || this.EmailMarkEnabled || this.AgeMarkEnabled || this.PasswordMarkEnabled) return;

            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(EmailAdress) || string.IsNullOrEmpty(Age) || string.IsNullOrEmpty(Password))return;

            if (IsEmailErrorVisible) return;
            //verify if email is already in db

            var exists = await this.user_repository.IsEmailRegisteredAsync(this.EmailAdress);
            if (exists)//make email error visible for 3 seconds
            {
                this.IsEmailErrorVisible = true;

               
                _ = Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    this.IsEmailErrorVisible = false;
                });

                return; 
            }


            this.navigationService.RegisteredUser.FirstName = this.FirstName;
            this.navigationService.RegisteredUser.LastName = this.LastName;
            this.navigationService.RegisteredUser.EmailAdress = this.EmailAdress;
            this.navigationService.RegisteredUser.Age = int.Parse(this.Age);
            this.navigationService.RegisteredUser.PasswordHash = await password_hash_service.GetPasswordHashAsync(this.Password);
            this.navigationService.AuthNavigateTo<SecondRegisterVM>();
            
        }
        
            


        }
    }

