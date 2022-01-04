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
    public partial class FollowListPage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();
        
        Product product;

        private User _user;

        public FollowListPage(User user)
        {
            InitializeComponent();

            _user = user;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allFollowList = await firebaseConnection.GetAllFollowList();
            Coll.ItemsSource = allFollowList;
        }

        private async void ProductDetail_Page(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var _product = btn.CommandParameter as Product;
            await Navigation.PushAsync(new ProductDetailPage(_product));
        }

        private async void DeleteFollowList(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var _product = btn.CommandParameter as Product;
            await firebaseConnection.DeleteFollowList(_product.ProductBarcode);
            var allFollowList = await firebaseConnection.GetAllFollowList();
            Coll.ItemsSource = allFollowList;
        }
    }
}