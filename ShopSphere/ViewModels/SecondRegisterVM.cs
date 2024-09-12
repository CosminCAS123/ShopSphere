using ReactiveUI;
using ShopSphere.Helpers;
using ShopSphere.Models;
using ShopSphere.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels;

    public class SecondRegisterVM : AuthViewModelBase
    {
    #region private fields

    private string pin1 = string.Empty;
    private string pin2 = string.Empty;
    private string pin3 = string.Empty;
    private string pin4 = string.Empty;
    private int selected_index;
    private string max_digits;
    private List<PhoneNationality> phoneNationalities;
    private string phoneNumber;
    

    #endregion

    #region public properties

    public int SelectedIndex { get => this.selected_index; set 
        
        { 
            this.RaiseAndSetIfChanged(ref this.selected_index, value);
            if (SelectedIndex >= 0)this.MaxDigits = this.PhoneNumbers[SelectedIndex].MaxDigits.ToString();    
        } }  

    public string PhoneNumberNoPrefix { get => this.phoneNumber; set => this.RaiseAndSetIfChanged(ref this.phoneNumber, value); }

    public string PinOne { get => this.pin1; set => this.RaiseAndSetIfChanged(ref this.pin1, value); }
    public string PinTwo { get => this.pin2; set => this.RaiseAndSetIfChanged(ref this.pin2, value); }

    public string PinThree { get => this.pin3; set => this.RaiseAndSetIfChanged(ref this.pin3, value); }

    public string PinFour { get => this.pin4; set => this.RaiseAndSetIfChanged(ref this.pin4, value); }

    public string MaxDigits { get => this.max_digits; set => this.RaiseAndSetIfChanged(ref this.max_digits, value); }
 

    public List<PhoneNationality> PhoneNumbers { get => this.phoneNationalities; set => this.RaiseAndSetIfChanged(ref this.phoneNationalities, value); }
    public ReactiveCommand<Unit, Unit> GoToNextRegisterCommand { get; set; }

    #endregion


    public SecondRegisterVM(IAuthNavigationService navigation_service) : base(navigation_service)
    {
        var nav = this.navigationService;
       
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
        this.SelectedIndex = 0;
        this.MaxDigits = "?";
        this.GoToNextRegisterCommand = ReactiveCommand.Create(go_to_next_register);
      

        }
 
    private void go_to_next_register()
    {
        if (!string.IsNullOrEmpty(this.PinOne) &&
            !string.IsNullOrEmpty(this.PinTwo) &&
            !string.IsNullOrEmpty(this.PinThree) &&
            !string.IsNullOrEmpty(this.PinFour) &&
            this.PhoneNumberNoPrefix.Length.ToString() == this.MaxDigits)
        {
            //add to registered user
            //go to third register
            var pin = this.PinOne + this.PinTwo + this.PinThree + this.PinFour;
            this.navigationService.RegisteredUser.PhoneNumber = this.PhoneNumbers[SelectedIndex].Prefix + this.PhoneNumberNoPrefix;
            this.navigationService.RegisteredUser.SecurityPIN = pin;
            this.navigationService.AuthNavigateTo<ThirdRegisterVM>();

        }
        

        
    }
    

    }



        
    

