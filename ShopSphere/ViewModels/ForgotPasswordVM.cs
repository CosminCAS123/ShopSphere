using Avalonia.Xaml.Interactions.Custom;
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
   
        private string error_message;
        private const int error_duration = 2000;
        private IUserRepository userRepository;
        public string  ErrorMessage { get => this.error_message; set => this.RaiseAndSetIfChanged(ref this.error_message , value); }
      
        public ReactiveCommand<Unit , Unit> SendPasswordCommand { get; set; }
        public string  EmailAdress { get => this.email; set => this.RaiseAndSetIfChanged(ref this.email , value); }
        public string PIN { get => this.pin; set => this.RaiseAndSetIfChanged(ref this.pin, value); }
        public ForgotPasswordVM(IAuthNavigationService navigationService , IUserRepository user_repository) : base(navigationService)
        {
            this.userRepository = user_repository;
            this.SendPasswordCommand = ReactiveCommand.CreateFromTask(sendPassword_cmd);
     
            this.ErrorMessage = string.Empty;
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

            if (this.PIN.Length != 4)
            {
                this.ErrorMessage = ErrorResources.Register.PinLength;
                await Task.Delay(error_duration);
                this.ErrorMessage = string.Empty;
                return;
            }

            //both email and pin are good format


            var cancellationTokenSource = new CancellationTokenSource();
            var loadingTask = ShowLoadingAnimationAsync(cancellationTokenSource.Token);


            var user = await this.userRepository.GetUserByEmailAsync(this.EmailAdress);
            if (user is null)
            {
                this.ErrorMessage = ErrorResources.Register.EmailAdressNotFound;
                await Task.Delay(error_duration);
                this.ErrorMessage = string.Empty;
                return;
            }

            //email exists

            //compare pins 
            if (this.PIN != user.SecurityPIN)
            {
                cancellationTokenSource.Cancel();
                await loadingTask;
                this.ErrorMessage = ErrorResources.Register.PinDoesNotMatch;
                await Task.Delay(error_duration);
                this.ErrorMessage = string.Empty;
                return;
            }

            //pin and email are correct
            //TODO


            
        }
    }
}
