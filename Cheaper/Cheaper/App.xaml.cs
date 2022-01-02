using Cheaper.View.Introduction;
using Cheaper.View.SignIn;
using Cheaper.View.UserInterface;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cheaper
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new SignInPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
