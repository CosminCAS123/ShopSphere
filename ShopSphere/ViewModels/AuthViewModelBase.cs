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
    public abstract class AuthViewModelBase : ReactiveObject
    {
        public IAuthNavigationService navigationService;
        public ReactiveCommand<Unit, Unit> GoBackCommand { get; set; }
        public AuthViewModelBase(IAuthNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.GoBackCommand = ReactiveCommand.Create(() => { this.navigationService.AuthNavigateBack(); });
        }
    }
}
