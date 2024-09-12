using Avalonia.Controls;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReactiveUI;
using ShopSphere.Helpers;
using ShopSphere.Repositories;
using ShopSphere.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShopSphere.ViewModels
{
    public class ThirdRegisterVM : AuthViewModelBase
    {
        private bool isgobackvisible = true;
        private string success_or_error_text;
        private ISolidColorBrush error_color;
        private readonly string userpfp_path = "ShopSphere/Assets/UserPFP/";
        
        private readonly string default_pic = "avares://ShopSphere/Assets/DefaultPFP.png";
        private string username;
        private bool is_error_visible;
        private Bitmap selected_image;
        public ISolidColorBrush ErrorColor { get => this.error_color; set => this.RaiseAndSetIfChanged(ref this.error_color, value); }
        public Bitmap SelectedImage { get => this.selected_image; set => this.RaiseAndSetIfChanged(ref this.selected_image, value); }
        public string SuccessOrErrorText { get => this.success_or_error_text; set => this.RaiseAndSetIfChanged(ref this.success_or_error_text, value); }
        public ReactiveCommand<UserControl , Unit> SelectImageCommand { get; set; }
        public string? Username { get => this.username; set => this.RaiseAndSetIfChanged(ref this.username, value); }
        public bool IsGoBackVisible { get => this.isgobackvisible; set => this.RaiseAndSetIfChanged(ref this.isgobackvisible, value); }
        public bool IsErrorVisible { get => this.is_error_visible; set => this.RaiseAndSetIfChanged(ref this.is_error_visible, value); }
        public ReactiveCommand<Unit ,Unit> FinishRegisterCommand { get; set; }
        private Stream ImageStream = null;
        private IUserRepository userRepository;
        private bool isLoading = false;
        public ThirdRegisterVM(IAuthNavigationService navigation_service , IUserRepository user_repository) : base(navigation_service)
        {
            this.userRepository = user_repository;
           
          
            this.SelectImageCommand = ReactiveCommand.CreateFromTask<UserControl>(selectImageCommand);
          
            this.SelectedImage = new Bitmap(AssetLoader.Open(new Uri(default_pic))) ;

            this.FinishRegisterCommand = ReactiveCommand.CreateFromTask(finish_register_command);
            this.SuccessOrErrorText = string.Empty;

            ObserveUsername();
            
        }
        private void ObserveUsername()
        {
            this.WhenAnyValue(x => x.Username).Subscribe(x =>
            {
                /*username needs - length at least 6
                                 - one uppercase letter
                                 - one lowercase letter 
                                 - one digit */
                string pattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)[A-Za-z\\d]{6,}$";
                if (!string.IsNullOrEmpty(x) && !Regex.IsMatch(x, pattern)) this.IsErrorVisible = true;
                else this.IsErrorVisible = false;



            });
        }
        private void SetUserTest()
        {
           
           
            navigationService.RegisteredUser.FirstName = "Devon";
            navigationService.RegisteredUser.LastName = "Larrat";
            navigationService.RegisteredUser.EmailAdress = "devonlarrat123@gmail.com";
            navigationService.RegisteredUser.PasswordHash = "12345";
            navigationService.RegisteredUser.Age = 25;
            navigationService.RegisteredUser.SecurityPIN = "12345";
            navigationService.RegisteredUser.PhoneNumber = "12345678";
        }
        private async Task finish_register_command()
        {
            if (!isLoading) {
                isLoading = true;
                if (!string.IsNullOrEmpty(this.Username))
                {
                    if (!this.IsErrorVisible)
                    {
                        if (this.ImageStream is not null)
                        {
                            //check if username exists in db
                            if (await this.userRepository.GetUserByUsernameAsync(Username) is not null) // already exists
                            {
                                this.SuccessOrErrorText = ErrorResources.Register.UsernameAlreadyExists;
                                this.ErrorColor = Brushes.Red;
                            }
                            else
                            {
                                //ADD PFP TO PROJECT
                                SetUserTest();
                                var file_name = this.Username + "pfp";
                                var full_img_path = Path.Combine(AppContext.BaseDirectory, this.userpfp_path.Replace("/", "\\"), file_name);

                              
                                // Create the file at the full image path
                                await using var fileStream = File.Create(full_img_path);

                                // Ensure the image stream is at the beginning
                                this.ImageStream.Seek(0, SeekOrigin.Begin);

                                // Copy the image stream to the file
                                await this.ImageStream.CopyToAsync(fileStream);



                                //REGISTER WHOLE USER

                                this.navigationService.RegisteredUser.ProfilePictureUrl = full_img_path;
                                this.navigationService.RegisteredUser.Username = this.Username;

                                //ADD USER TO DATABASE

                                await this.userRepository.AddUserAsync(navigationService.RegisteredUser);

                                //Show user registered 
                                this.ErrorColor = Brushes.YellowGreen;
                                this.SuccessOrErrorText = ErrorResources.Register.RegisteredSuccessfully;
                            }
                        }
                        else //image not entered
                        {
                            
                            this.SuccessOrErrorText = "You must select a profile picture.";
                            this.ErrorColor = Brushes.Red;
                        }
                        
                    }
                    
                    else //invalid username 
                    {
                        this.SuccessOrErrorText = "Username has invalid format.";
                        this.ErrorColor = Brushes.Red;
                    }
                }
                else //username null
                {
                    this.SuccessOrErrorText = "Username cannot be empty.";
                    this.ErrorColor = Brushes.Red;
                } 

                isLoading = false;
            }
        }
        private async Task selectImageCommand(UserControl registerView)
        {
            var topLevel = TopLevel.GetTopLevel(registerView);
            if (topLevel?.StorageProvider is not null)
            {
                var files = await topLevel.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
                {
                    Title = "Select an image",
                    AllowMultiple = false,
                    FileTypeFilter = new[]
                      {
                     new FilePickerFileType("PNG, JPG, JPEG") { Patterns = new[] { "*.png", "*.jpg", "*.jpeg" } }
                }
                });

                if (files.Count > 0)
                {
                    var stream = await files[0].OpenReadAsync();
                    this.ImageStream = stream;
                    this.SelectedImage = new Bitmap(stream);
                }
            }


        }
    }
}
