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

        #endregion

        public string Username { get => this.username; set => this.RaiseAndSetIfChanged(ref this.username, value); }
        public string Password { get => this.password; set => this.RaiseAndSetIfChanged(ref this.password, value); }



    }
}
