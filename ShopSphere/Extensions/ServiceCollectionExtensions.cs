    using Microsoft.Extensions.DependencyInjection;
using ShopSphere.Data;
using ShopSphere.Services;
using ShopSphere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Extensions
{
   
        public static class ServiceCollectionExtensions
        {
            public static void AddCommonServices(this IServiceCollection collection)
            {
            collection.AddDbContext<ShopSphereContext>();
            collection.AddSingleton<IAuthNavigationService, NavigationService>();
            collection.AddTransient<LoginVM>();
            collection.AddTransient<RegisterVM>();
            collection.AddTransient<SecondRegisterVM>();
            collection.AddTransient<ThirdRegisterVM>();
            }
        }
    
}
