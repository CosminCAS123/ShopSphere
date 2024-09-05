using ReactiveUI;
using ShopSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels
{
    public class AuthWindowVM : ViewModelBase
    {
        private ViewModelBase authContent = null!;

        private User user;
        public User RegisteredUser
        {
            get => this.user;
            set => this.RaiseAndSetIfChanged(ref this.user, value);
        
        }
        public  ViewModelBase AuthContent
        {
            get => this.authContent;
            set => this.RaiseAndSetIfChanged(ref this.authContent, value);
        }

        public AuthWindowVM(LoginVM initial_vm)
        {
            this.AuthContent = initial_vm;
            this.RegisteredUser = new User();
        }


    }
}
