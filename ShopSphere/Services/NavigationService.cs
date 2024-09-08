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
    public class NavigationService : ReactiveObject, IAuthNavigationService
    {

        private readonly IServiceProvider serviceProvider;
        private Stack<AuthViewModelBase> navigationStack;
        private User registered_user;
        private AuthViewModelBase auth_content;
        public User RegisteredUser { get => this.registered_user; set => this.RaiseAndSetIfChanged(ref this.registered_user, value); }
        public AuthViewModelBase AuthContent { get => this.auth_content; set => this.RaiseAndSetIfChanged(ref this.auth_content, value); }

        public void AuthNavigateTo<TViewModel>() where TViewModel : AuthViewModelBase
        {

            var vm = serviceProvider.GetService<TViewModel>()!;
            navigationStack.Push(vm);

            this.AuthContent = vm;
        }
        public void AuthNavigateBack() {


            this.navigationStack.Pop();
            this.AuthContent = this.navigationStack.Peek();

}
        
        public NavigationService(IServiceProvider service_provider)
        {
            this.serviceProvider = service_provider;
            this.RegisteredUser = new User();
            this.navigationStack = new Stack<AuthViewModelBase>();
          
           
        
        }
       
    }
}
