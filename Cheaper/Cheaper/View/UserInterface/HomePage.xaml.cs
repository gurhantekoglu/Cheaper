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
    public partial class HomePage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();
        Product product;
        public HomePage(User user)
        {
            InitializeComponent();

            BindingContext = product;

            logo.Source = ImageSource.FromResource("Cheaper.View.cheaper_logo.png");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allProducts = await firebaseConnection.GetAllProducts();
            Coll.ItemsSource = allProducts;
        }

        private async void ProductDetail_Page(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var _product = btn.CommandParameter as Product;
            await Navigation.PushAsync(new ProductDetailPage(_product));
        }
    }
}