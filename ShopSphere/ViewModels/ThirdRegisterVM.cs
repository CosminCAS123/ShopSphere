using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
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
    public class ThirdRegisterVM : ViewModelBase
    {
        private IAuthNavigationService navigationService;
        private readonly string default_pic = "avares://ShopSphere/Assets/DefaultPFP.png";

        private Bitmap selected_image;
        public Bitmap SelectedImage { get => this.selected_image; set => this.RaiseAndSetIfChanged(ref this.selected_image, value); }
        public ReactiveCommand<UserControl , Unit> SelectImageCommand { get; set; }
        public ThirdRegisterVM(IAuthNavigationService nav) 
        {
            this.navigationService = nav;
            this.SelectImageCommand = ReactiveCommand.CreateFromTask<UserControl>(selectImageCommand);

            this.SelectedImage = new Bitmap(AssetLoader.Open(new Uri(default_pic))) ;
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
                }) ;

                if (files.Count > 0)
                {
                    await using var stream = await files[0].OpenReadAsync();
                    this.SelectedImage = new Bitmap(stream);
                }
            }

            
        }
    }
}
