using Cheaper.Database;
using Cheaper.View.SignIn;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cheaper.View.CreateAccount
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();

        MediaFile file;
        private string PhotoUrl { get; set; }

        public CreateAccountPage()
        {
            InitializeComponent();

            logo.Source = ImageSource.FromResource("Cheaper.View.cheaper_logo.png");
        }

        private async void ProfilePhotoSelect(object sender, EventArgs e)
        {
            var question = await DisplayActionSheet("Profil Fotoğrafı Yükle", "Kapat", null, "Galeri", "Kamera");
            if (question == "Galeri")
            {
                var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Fotoğraf seçin."
                });

                if (file != null)
                {
                    var stream = await file.OpenReadAsync();

                    UserProfilePhoto.Source = ImageSource.FromStream(() => stream);
                    PhotoUrl = await firebaseConnection.UploadProfilePhoto(file.FileName, await file.OpenReadAsync());
                }
            }
            if (question == "Kamera")
            {
                var file = await MediaPicker.CapturePhotoAsync();

                if (file != null)
                {
                    var stream = await file.OpenReadAsync();

                    UserProfilePhoto.Source = ImageSource.FromStream(() => stream);
                    PhotoUrl = await firebaseConnection.UploadProfilePhoto(file.FileName, await file.OpenReadAsync());
                }
            }
        }

        private async void SignIn_Page(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void CreateAccount_Clicked(object sender, EventArgs e)
        {
            var Auth = await firebaseConnection.CheckUsername(Username.Text);
            if (!(string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text)))
            {
                if (Auth != null)
                {
                    await DisplayAlert("Hata", "Kullanıcı adı zaten kullanılıyor.", "Tamam");
                }
                else
                {
                    await firebaseConnection.AddUser(Username.Text, Password.Text, PhotoUrl.ToString());
                    await DisplayAlert("Hesap Oluşturuldu", "Hesabınız oluşturuldu, oturum açabilirsiniz.", "Tamam");
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await DisplayAlert("Hata", "Eksik bilgiler var, alanları kontrol edin.", "Tamam");
            }
        }
    }
}