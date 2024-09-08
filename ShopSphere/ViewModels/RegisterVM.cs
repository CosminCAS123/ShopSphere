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
    public class RegisterVM : AuthViewModelBase
    {

        private string firstName;
        private string lastName;
        private string emailAdress;
        private string age;
        public string FirstName { get => this.firstName; set => this.RaiseAndSetIfChanged(ref this.firstName, value); }
        public string LastName { get => this.lastName; set => this.RaiseAndSetIfChanged(ref this.lastName, value); }
        public string EmailAdress { get => this.emailAdress ; set => this.RaiseAndSetIfChanged(ref this.emailAdress , value); }
        public string Age { get => this.age; set => this.RaiseAndSetIfChanged(ref this.age  , value); }

        public ReactiveCommand<Unit , Unit> NextRegisterCommand { get; set; }



        public RegisterVM(IAuthNavigationService navigationService) : base(navigationService) 
        {

            
            this.NextRegisterCommand = ReactiveCommand.CreateFromTask(goToSecondRegister);
            
        }
        
        private async Task goToSecondRegister()
        {
            //VERIFICATION FIELD ??? !!!!
            this.navigationService.RegisteredUser.FirstName = this.FirstName;
            this.navigationService.RegisteredUser.LastName = this.LastName;
            this.navigationService.RegisteredUser.EmailAdress = this.EmailAdress;
            this.navigationService.RegisteredUser.Age = 3;

            this.navigationService.AuthNavigateTo<SecondRegisterVM>();
            //VERIFICATION FIELD ??? !!!!


        }
    }
}
