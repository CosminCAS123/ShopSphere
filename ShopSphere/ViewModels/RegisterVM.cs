using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels
{
    public class RegisterVM : ViewModelBase
    {

        private string firstName;
        private string lastName;
        private string emailAdress;
        private string age;
        public string FirstName { get => this.FirstName; set => this.RaiseAndSetIfChanged(ref this.firstName, value); }
        public string LastName { get => this.LastName; set => this.RaiseAndSetIfChanged(ref this.lastName, value); }
        public string EmailAdress { get => this.EmailAdress ; set => this.RaiseAndSetIfChanged(ref this.emailAdress , value); }
        public string Age { get => this.Age; set => this.RaiseAndSetIfChanged(ref this.age  , value); }

        public ReactiveCommand<Unit , Unit> NextRegisterCommand { get; set; }

        private ViewModelBase second_register;

        public RegisterVM(SecondRegisterVM secondRegisterVM)
        {
            this.second_register = secondRegisterVM;
            this.NextRegisterCommand = ReactiveCommand.Create();
        }
    }
}
