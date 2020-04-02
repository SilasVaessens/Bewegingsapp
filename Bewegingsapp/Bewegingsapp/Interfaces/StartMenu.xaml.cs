using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Bewegingsapp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Route_Clicked(object sender, EventArgs e) //navigatie naar het route kiezen, listview van de routes
        {
            await Navigation.PushAsync(new RouteKiezen());
        }

        private async void Instellingen_Clicked(object sender, EventArgs e) //navigatie naar het instellingen menu
        {
            await Navigation.PushAsync(new InstellingenMenu());
        }
    }
}
