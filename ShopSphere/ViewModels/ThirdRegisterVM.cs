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

        
        private readonly string default_pic = "avares://ShopSphere/Assets/DefaultPFP.png";
        private string username;
        private string image_extension;
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
        private string userpfp_path;
        private Stream ImageStream = null;
        private IUserRepository userRepository;
        private bool isLoading = false;
        private bool can_try_again;
    //    public bool IsErrorTextVisible { get => this.is_error_visible; set => this.RaiseAndSetIfChanged(ref this.is_error_visible, value); }
        public ThirdRegisterVM(IAuthNavigationService navigation_service , IUserRepository user_repository) : base(navigation_service)
        {
            this.userRepository = user_repository;
            var projectRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));

             this.userpfp_path = Path.Combine(projectRootPath, "ShopSphere", "Assets", "UserPFP");

         

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
     /*private void SetUser()
        {
            navigationService.RegisteredUser.Age = 19;
            navigationService.RegisteredUser.EmailAdress = "cosmingamer2004@gmail.com";
            navigationService.RegisteredUser.FirstName = "Cosmin";
            navigationService.RegisteredUser.LastName = "Andrei";
            navigationService.RegisteredUser.PasswordHash = "fsjhfsasjaf23831139fnasdsankksaf19021439";
            navigationService.RegisteredUser.PhoneNumber = "+40741039009";
            navigationService.RegisteredUser.SecurityPIN = "1234";
           

        }*/
     private void SetErrorText(string message , IImmutableSolidColorBrush color)
        {
            this.SuccessOrErrorText = message;
            this.ErrorColor = color;
        }
        private void DisableErrorText() => this.SuccessOrErrorText = string.Empty;
        private async Task wait_a_bit()
        {
            await Task.Delay(3000);
            this.can_try_again = true;
        }
        private async Task finish_register_command()
        {
            if (can_try_again)
            {
                if (!string.IsNullOrEmpty(this.Username))
                {
                    if (!this.IsErrorVisible)
                    {
                        if (this.ImageStream is not null)
                        {
                            //check if username exists in db
                            if (await this.userRepository.GetUserByUsernameAsync(Username) is not null) // already exists
                            {

                                SetErrorText(ErrorResources.Register.UsernameAlreadyExists, Brushes.Red);
                                //wait 3 seconds
                                this.can_try_again = false;
                                _ = Task.Run(wait_a_bit);
                                return;
                            }
                            else
                            {
                                //ADD PFP TO PROJECT


                                var file_name = $"{this.Username}pfp{this.image_extension}";


                                var full_img_path = Path.Combine(userpfp_path, file_name);

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
                                SetErrorText(ErrorResources.Register.RegisteredSuccessfully, Brushes.YellowGreen);
                                this.can_try_again = false;
                              
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
                    this.image_extension = Path.GetExtension(files[0].Name);
                }
            }


        }
    }
}
