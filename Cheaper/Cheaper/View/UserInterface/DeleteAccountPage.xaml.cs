using Cheaper.Database;
using Cheaper.Model;
using Cheaper.View.SignIn;
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
    public partial class DeleteAccountPage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();
        User _user;

        public DeleteAccountPage(User user)
        {
            InitializeComponent();

            BindingContext = user;
            _user = user;
        }

        private async void BackToProfilePage(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void DeleteMyAccount(object sender, EventArgs e)
        {
            await firebaseConnection.DeleteUser(_user.Username);
            await Navigation.PushModalAsync(new SignInPage());
        }
    }
}