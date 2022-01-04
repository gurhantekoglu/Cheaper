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
    public partial class ProfilePage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();

        User _user;

        public ProfilePage(User user)
        {
            InitializeComponent();

            BindingContext = user;
            _user = user;
        }

        private async void ChangePassword_Page(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage(_user));
        }

        private async void DeleteAccount_Page(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new DeleteAccountPage(_user));
        }
    }
}