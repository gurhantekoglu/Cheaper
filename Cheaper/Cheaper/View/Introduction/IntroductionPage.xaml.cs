using Cheaper.Model;
using Cheaper.View.SignIn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cheaper.View.Introduction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroductionPage : ContentPage
    {
        public IntroductionPage()
        {
            InitializeComponent();

            logo.Source = ImageSource.FromResource("Cheaper.View.cheaper_logo.png");

            this.BindingContext = this;
        }

        private async void Next_Page(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInPage());
        }

        private Timer timer;

        public ObservableCollection<InformationPages> InformationPageItems { get => Load(); }

        protected override void OnAppearing()
        {
            timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                if (cvInformationPages.Position == 4)
                {
                    cvInformationPages.Position = 0;
                    return;
                }

                cvInformationPages.Position += 1;
            });
        }

        private ObservableCollection<InformationPages> Load()
        {
            return new ObservableCollection<InformationPages>(new[]
            {
                new InformationPages
                {
                    Image = "carousel_1.png",
                    Description = "Cheaper ile bütçe dostu alışverişe hoşgeldiniz."
                },

                new InformationPages
                {
                    Image = "carousel_2.png",
                    Description = "İstediğiniz ürünün barkodunu taratın ve marketlerde arayın."
                },

                new InformationPages
                {
                    Image = "carousel_3.png",
                    Description = "İlgilendiğiniz ürün için kullanıcıların sizden önce eklediği fiyatları öğrenin ve tasarruf edin."
                },

                new InformationPages
                {
                    Image = "carousel_4.png",
                    Description = "İstediğiniz ürün henüz eklenmemiş ise ilk ekleyen siz olun ve diğer kullanıcılarla paylaşın."
                },

                new InformationPages
                {
                    Image = "carousel_5.png",
                    Description = "Cheaper’a hemen üye olun ve  kullanmaya başlayın."
                }
            });
        }

    }

    public class InformationPages
    {
        public string Image { get; set; }
        public string Description { get; set; }
    }
}