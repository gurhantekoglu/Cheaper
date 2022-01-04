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
    public partial class SearchPage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();
        Product product;
        private User _user;

        public SearchPage(User user)
        {
            InitializeComponent();

            _user = user;
        }

        private async void AddProductPage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddNewProductPage(_user));
        }

        private async void Search1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search1.Text.Length >= 3)
            {
                var searchProduct = await firebaseConnection.GetAllProducts();
                var searchResult = searchProduct.Where(c => c.ProductName.ToLower().Contains(Search1.Text));
                Coll.ItemsSource = searchResult;
            }
            else
            {
                Coll.ItemsSource = null;
            }
        }

        private async void ProductDetail_Page(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductDetailPage(product));
        }
    }
}