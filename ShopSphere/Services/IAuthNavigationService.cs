using ShopSphere.Models;
using ShopSphere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Services
{
    public interface IAuthNavigationService
    {
        void AuthNavigateTo<TViewModel>() where TViewModel : AuthViewModelBase;
        void AuthNavigateBack();
        void AuthRegisterFinished();
        User RegisteredUser { get; set; }
        AuthViewModelBase AuthContent { get; set; }

    }
}
