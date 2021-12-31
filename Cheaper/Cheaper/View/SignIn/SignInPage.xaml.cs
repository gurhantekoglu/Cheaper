using Cheaper.Database;
using Cheaper.View.CreateAccount;
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

        public SignInPage()
        {
            InitializeComponent();

            logo.Source = ImageSource.FromResource("Cheaper.View.cheaper_logo.png");
        }
        
        async void CreateAccount_Page(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateAccountPage()));
        }
    }
}