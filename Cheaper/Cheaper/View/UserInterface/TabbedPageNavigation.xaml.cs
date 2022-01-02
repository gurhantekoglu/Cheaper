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
    public partial class TabbedPageNavigation : TabbedPage
    {
        public TabbedPageNavigation(User user)
        {
            InitializeComponent();

            this.Children.Add(new HomePage(user) { IconImageSource= "https://img.icons8.com/ios/50/000000/home--v1.png" });
            this.Children.Add(new SearchPage(user) { IconImageSource = "https://img.icons8.com/ios/50/000000/search--v1.png" });
            this.Children.Add(new BarcodeScanPage(user) { IconImageSource = "https://img.icons8.com/external-justicon-lineal-justicon/50/000000/external-barcode-ecommerce-justicon-lineal-justicon.png" });
            this.Children.Add(new FollowListPage(user) { IconImageSource = "https://img.icons8.com/windows/50/000000/like--v1.png" });
            this.Children.Add(new ProfilePage(user) { IconImageSource = "https://img.icons8.com/external-kiranshastry-lineal-kiranshastry/64/000000/external-user-interface-kiranshastry-lineal-kiranshastry-1.png" });
        }
    }
}