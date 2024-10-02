using Avalonia.Media;
using Avalonia.Xaml.Interactions.Custom;
using Microsoft.Extensions.DependencyModel.Resolution;
using ReactiveUI;
using ShopSphere.Helpers;
using ShopSphere.Repositories;
using ShopSphere.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels
{
    public class ForgotPasswordVM : AuthViewModelBase

    {
        private string email;
        private string pin;
        private const string PASS_CHANGED_SUCCESSFULLY = "Password was changed successfully!";
        private IEmailSenderService emailSenderService;
        private string error_message;
        private const int error_duration = 2000;
        private IUserRepository userRepository;
        private bool reveal_password;
        private bool show_pass_textbox;
        private string new_password;
        private bool DONE = false;
        private bool show_initial;
        private IPasswordHashService passwordHashService;
        private Models.User current_user;
        private ISolidColorBrush errorcolor;
        private string button_text;
        public string ButtonText { get => this.button_text; set => this.RaiseAndSetIfChanged(ref this.button_text, value); }
        public ISolidColorBrush ErrorColor { get => this.errorcolor; set => this.RaiseAndSetIfChanged(ref this.errorcolor, value); }
        public bool ShowInitialTextbox { get => this.show_initial; set => this.RaiseAndSetIfChanged(ref this.show_initial, value); }
        public string NewPassword { get => this.new_password; set => this.RaiseAndSetIfChanged(ref this.new_password , value); }
        public bool RevealPassword { get => this.reveal_password; set => this.RaiseAndSetIfChanged(ref this.reveal_password, value); }
        public bool ShowPasswordTextbox { get => this.show_pass_textbox; set => this.RaiseAndSetIfChanged(ref this.show_pass_textbox , value); }

        public string  ErrorMessage { get => this.error_message; set => this.RaiseAndSetIfChanged(ref this.error_message , value); }
      
        public ReactiveCommand<Unit , Unit> SendPasswordCommand { get; set; }
        public string  EmailAdress { get => this.email; set => this.RaiseAndSetIfChanged(ref this.email , value); }
        public string PIN { get => this.pin; set => this.RaiseAndSetIfChanged(ref this.pin, value); }
        public ForgotPasswordVM(IAuthNavigationService navigationService , IUserRepository user_repository  , IEmailSenderService emailsenderservice , IPasswordHashService hashservice) : base(navigationService)
        {
            this.userRepository = user_repository;
            this.SendPasswordCommand = ReactiveCommand.CreateFromTask(sendPassword_cmd);
            this.emailSenderService = emailsenderservice;
            this.ErrorMessage = string.Empty;
            this.ShowPasswordTextbox = false;
            this.ShowInitialTextbox = true;
            this.passwordHashService = hashservice;
            this.ButtonText = "Verify account";
            this.ErrorColor = Brushes.Red;
            

        }

        private async Task ShowLoadingAnimationAsync(CancellationToken cancellationToken)
        {
            var loadingStates = new[] { "Loading .", "Loading ..", "Loading ...", "Loading" };
            int index = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                this.ErrorMessage = loadingStates[index];
                index = (index + 1) % loadingStates.Length;

                // Wait for 500 milliseconds between state changes
                await Task.Delay(500);

                if (cancellationToken.IsCancellationRequested)
                {
                    this.ErrorMessage = string.Empty; // Clear the loading message when cancelled
                }
            }
        }


        private async Task sendPassword_cmd()
        {

            if (this.DONE) return;
            
            if (!this.ShowPasswordTextbox)
            {
                /////FIRST STEP
                ///

                var validation = FieldVerification.EmailAdressFormat(this.EmailAdress);
                //email errors
                if (!(validation == ErrorResources.Register.GoodField))
                {
                    this.ErrorMessage = validation;


                    //set timer 

                    await Task.Delay(error_duration);

                    this.ErrorMessage = String.Empty;

                    return;
                }

                //email is good 

                validation = FieldVerification.PinFormat(this.PIN);
                if (validation != ErrorResources.Register.GoodField)
                {
                    this.ErrorMessage = validation;
                    await Task.Delay(error_duration);
                    this.ErrorMessage = string.Empty;
                    return;
                }

                //both email and pin are good format


                var cancellationTokenSource = new CancellationTokenSource();
                var loadingTask = ShowLoadingAnimationAsync(cancellationTokenSource.Token);


                this.current_user =  await this.userRepository.GetUserByEmailAsync(this.EmailAdress);
                if (current_user is null)
                {
                    this.ErrorMessage = ErrorResources.Register.EmailAdressNotFound;
                    await Task.Delay(error_duration);
                    this.ErrorMessage = string.Empty;
                    return;
                }

                //email exists

                //compare pins and stop loading if doesnt match
                cancellationTokenSource.Cancel();
                await loadingTask;
                if (this.PIN != current_user.SecurityPIN)
                {
                    this.ErrorMessage = ErrorResources.Register.PinDoesNotMatch;
                    await Task.Delay(error_duration);
                    this.ErrorMessage = string.Empty;
                    return;
                }

                //pin and email are correct
                //TODO

                this.ShowInitialTextbox = false;
                this.ShowPasswordTextbox = true;
                this.ButtonText = "Change password";


            }
            else
            {
                ///SECOND STEP
                ///

                ///password verification
                ///

                var pass = this.NewPassword;
                var validation = FieldVerification.PasswordFormat(pass);
               if (validation != ErrorResources.Register.GoodField)
                {
                    //show error
                    this.ErrorMessage = validation;
                    await Task.Delay(error_duration);
                    this.ErrorMessage = string.Empty;
                    return;
                }

                //good, change password
                //start loading
                var cancellationTokenSource = new CancellationTokenSource();
                var loadingTask = ShowLoadingAnimationAsync(cancellationTokenSource.Token);

                var hash = await this.passwordHashService.GetPasswordHashAsync(pass);

                await this.userRepository.ChangeUserPassword(current_user, hash);

                //disable interactivity

                cancellationTokenSource.Cancel();
                await loadingTask;
                this.ErrorMessage = PASS_CHANGED_SUCCESSFULLY;
                this.ErrorColor = Brushes.Green;
                this.ShowInitialTextbox = false;
                this.ShowPasswordTextbox = false;
                    this.DONE = true;
                
                
                





            }
        }
    }
}
