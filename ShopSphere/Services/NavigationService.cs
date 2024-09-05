using Microsoft.Extensions.DependencyInjection;
using ShopSphere.Models;
using ShopSphere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Services
{
    public class NavigationService : IAuthNavigationService
    {

        private readonly IServiceProvider serviceProvider;
        private AuthWindowVM authWindowVM;

        public User RegisteredUser { get => this.authWindowVM.RegisteredUser; set => this.authWindowVM.RegisteredUser = value; }

        public void AuthNavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            
            var vm =  serviceProvider.GetService<TViewModel>()!;
     
            this.authWindowVM.AuthContent = vm;
        }

        public NavigationService(IServiceProvider service_provider, AuthWindowVM authvm)
        {
            this.serviceProvider = service_provider;

           this.authWindowVM = authvm;
           
        
        }
       
    }
}
