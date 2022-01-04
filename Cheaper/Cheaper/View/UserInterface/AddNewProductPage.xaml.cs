using Cheaper.Database;
using Cheaper.Model;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cheaper.View.UserInterface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewProductPage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();

        MediaFile file;

        private string ProductPhotoUrl { get; set; }

        private User _user;

        public AddNewProductPage(User user)
        {
            InitializeComponent();

            _user = user;
        }

        private async void ProductPhotoSelect(object sender, EventArgs e)
        {
            var question = await DisplayActionSheet("Ürün Fotoğrafı Yükle", "Kapat", null, "Galeri", "Kamera");
            if (question == "Galeri")
            {
                var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Fotoğraf seçin."
                });

                if (file != null)
                {
                    var stream = await file.OpenReadAsync();

                    ProductPhoto.Source = ImageSource.FromStream(() => stream);
                    ProductPhotoUrl = await firebaseConnection.UploadProductPhoto(file.FileName, await file.OpenReadAsync());
                }

            }
            else if (question == "Kamera")
            {
                var file = await MediaPicker.CapturePhotoAsync();

                if (file != null)
                {
                    var stream = await file.OpenReadAsync();

                    ProductPhoto.Source = ImageSource.FromStream(() => stream);
                    ProductPhotoUrl = await firebaseConnection.UploadProductPhoto(file.FileName, await file.OpenReadAsync());
                }
            }
        }

        private async void AddNewProduct(object sender, EventArgs e)
        {
            var Auth = await firebaseConnection.CheckProductBarcode(ProductBarcode.Text);
            if (Auth != null)
            {
                await DisplayAlert("Hata", "Ürün zaten önceden eklenmiş.", "Tamam");
            }
            else
            {
                await firebaseConnection.AddProduct(ProductBarcode.Text, ProductName.Text, Convert.ToString(Shops.SelectedItem), ProductPhotoUrl.ToString(), Convert.ToDouble(Price.Text), DateTime.Now, _user.Username);
                await DisplayAlert("Ürün Eklendi", "Ürün başarılı bir şekilde eklendi.", "Tamam");
                await Navigation.PopModalAsync();
            }
        }
    }
}