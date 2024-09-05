using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShopSphere.Models;
using ShopSphere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Services
{
    public class NavigationService : ReactiveObject,  IAuthNavigationService
    {

        private readonly IServiceProvider serviceProvider;

        private User registered_user;
        private ViewModelBase auth_content;
        public User RegisteredUser { get => this.registered_user; set => this.RaiseAndSetIfChanged(ref this.registered_user, value); }
        public ViewModelBase AuthContent { get => this.auth_content; set => this.RaiseAndSetIfChanged(ref this.auth_content , value); }

        public void AuthNavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            
            var vm =  serviceProvider.GetService<TViewModel>()!;

            this.AuthContent = vm;
        }

        public NavigationService(IServiceProvider service_provider)
        {
            this.serviceProvider = service_provider;
            this.RegisteredUser = new User();

          
           
        
        }
       
    }
}
