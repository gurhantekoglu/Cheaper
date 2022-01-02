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
    public partial class ChangePasswordPage : ContentPage
    {
        FirebaseConnection firebaseConnection = new FirebaseConnection();

        public string Auth;
        private User _user;

        public ChangePasswordPage(User user)
        {
            InitializeComponent();

            BindingContext = user;
            _user = user;

            Title = "Şifre Değişikliği";
        }

        private async void Change_Password(object sender, EventArgs e)
        {
            var Auth = await firebaseConnection.CheckUsername(_user.Username);
            if (!(string.IsNullOrEmpty(NewPassword.Text)))
            {
                if (Auth != null)
                {
                    await firebaseConnection.UpdateUser(_user, NewPassword.Text);
                    NewPassword.Text = string.Empty;
                    await DisplayAlert("Başarılı", "Şifreniz güncellendi.", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Hata", "Eksik bilgiler var, alanları kontrol edin.", "Tamam");
            }
        }
    }
}