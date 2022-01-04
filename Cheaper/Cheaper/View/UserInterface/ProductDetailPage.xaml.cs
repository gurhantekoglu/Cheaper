using Cheaper.Database;
using Cheaper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cheaper.View.UserInterface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();

        Product _product;

        private User _user;

        public ProductDetailPage(Product product)
        {
            InitializeComponent();

            Title = "Ürün Detayı";

            BindingContext = product;
            _product = product;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void AddFollowList(object sender, EventArgs e)
        {
            var Auth = await firebaseConnection.CheckFollowListProductBarcode(_product.ProductBarcode);
            if (Auth != null)
            {
                await DisplayAlert("Hata", "Ürün zaten önceden takip listenize eklenmiş.", "Tamam");
            }
            else
            {
                await firebaseConnection.AddFollowList(_product.ProductBarcode, _product.ProductName, _product.ShopName, _product.ProductPhotoUrl, _product.Price, _product.PriceDate);
                await DisplayAlert("Ürün Takip Listenize Eklendi", "Ürün başarılı bir şekilde takip listenize eklendi.", "Tamam");
            }
        }
    }
}