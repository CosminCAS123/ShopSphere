using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        #region PrivateVariables
        private string username;
        private string password;
        private bool showPassword = false;
       

        #endregion

        public string Username { get => this.username; set => this.RaiseAndSetIfChanged(ref this.username, value); }
        public string Password { get => this.password; set => this.RaiseAndSetIfChanged(ref this.password, value); }

        public bool ShowPassword { get => this.showPassword; set => this.RaiseAndSetIfChanged(ref this.showPassword, value); 
        
        }



    }
}
