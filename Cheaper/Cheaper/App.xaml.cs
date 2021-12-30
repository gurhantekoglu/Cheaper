using Cheaper.View.Introduction;
using Cheaper.View.SignIn;
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
            MainPage = new NavigationPage(new IntroductionPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
