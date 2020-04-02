using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Bewegingsapp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Route_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RouteKiezen());
        }

        private async void Instellingen_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InstellingenMenu());
        }
    }
}
