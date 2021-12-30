using Cheaper.View.Introduction;
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

            MainPage = new IntroductionPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
