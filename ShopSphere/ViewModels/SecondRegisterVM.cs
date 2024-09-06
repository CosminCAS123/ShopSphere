using ReactiveUI;
using ShopSphere.Helpers;
using ShopSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels;

    public class SecondRegisterVM : ViewModelBase
    {

   

    private List<PhoneNationality> phoneNationalities;
        public List<PhoneNationality> PhoneNumbers { get => this.phoneNationalities; set => this.RaiseAndSetIfChanged(ref this.phoneNationalities , value); }
     
        public SecondRegisterVM()
        {

            this.PhoneNumbers = new List<PhoneNationality>
              {
           
   

       new PhoneNationality(PhoneResources.MaxDigits.France,  PhoneResources.Prefixes.France, PhoneResources.URIs.France),

       new PhoneNationality(PhoneResources.MaxDigits.Germany, PhoneResources.Prefixes.Germany, PhoneResources.URIs.Germany),

       new PhoneNationality(PhoneResources.MaxDigits.Italy, PhoneResources.Prefixes.Italy, PhoneResources.URIs.Italy),

       new PhoneNationality(PhoneResources.MaxDigits.Netherlands, PhoneResources.Prefixes.Netherlands, PhoneResources.URIs.Netherlands),

       new PhoneNationality(PhoneResources.MaxDigits.Romania, PhoneResources.Prefixes.Romania, PhoneResources.URIs.Romania),

       new PhoneNationality(PhoneResources.MaxDigits.Spain, PhoneResources.Prefixes.Spain, PhoneResources.URIs.Spain),

       new PhoneNationality(PhoneResources.MaxDigits.UnitedKingdom, PhoneResources.Prefixes.UnitedKingdom, PhoneResources.URIs.UnitedKingdom)



    };


        }
    }



        
    

