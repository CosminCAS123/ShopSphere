using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShopSphere.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels
{
    public class LoginVM : AuthViewModelBase
    {
        #region PrivateVariables
        private string username;
        private string password;
        private bool showPassword = false;
       
 
        #endregion
        

        public string Username { get => this.username; set => this.RaiseAndSetIfChanged(ref this.username, value); }
        public string Password { get => this.password; set => this.RaiseAndSetIfChanged(ref this.password, value); }

        public bool ShowPassword { get => this.showPassword; set => this.RaiseAndSetIfChanged(ref this.showPassword, value); }
       
        public ReactiveCommand<Unit, Unit> GoToFirstRegisterCommand { get; set; }


        public LoginVM(IAuthNavigationService navigationService) : base(navigationService) { 

          
            this.GoToFirstRegisterCommand = ReactiveCommand.CreateFromTask(goToFirstRegister);
        }
        private async Task goToFirstRegister() =>  navigationService.AuthNavigateTo<SecondRegisterVM>();
        
        
                            


    }
}
