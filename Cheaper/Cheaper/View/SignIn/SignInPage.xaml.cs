using Cheaper.Database;
using Cheaper.View.CreateAccount;
using Cheaper.View.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cheaper.View.SignIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();

        public string Auth;

        public SignInPage()
        {
            InitializeComponent();

            logo.Source = ImageSource.FromResource("Cheaper.View.cheaper_logo.png");
        }

        private async void CreateAccount_Page(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateAccountPage()));
        }

        private async void Home_Page(object sender, EventArgs e)
        {
            var Auth = await firebaseConnection.CheckUser(Username.Text, Password.Text);
            if (!(string.IsNullOrEmpty(Username.Text) && string.IsNullOrEmpty(Password.Text)))
            {
                if (Auth != null)
                {
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Hata", "Kullanıcı bilgileri hatalı.", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Hata", "Eksik bilgiler var, alanları kontrol edin.", "Tamam");
            }
        }
    }
}