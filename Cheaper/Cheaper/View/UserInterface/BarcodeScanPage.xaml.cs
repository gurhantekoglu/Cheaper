﻿using Cheaper.Model;
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
    public partial class BarcodeScanPage : ContentPage
    {
        public BarcodeScanPage(User user)
        {
            InitializeComponent();
        }
    }
}