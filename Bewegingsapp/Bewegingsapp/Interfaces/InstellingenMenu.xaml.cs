using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstellingenMenu : ContentPage
    {
        public InstellingenMenu()
        {
            InitializeComponent();
        }

        private async void Oefeningen_Instellingen_Clicked(object sender, EventArgs e) //navigatie naar lijst met oefeningen, via button
        {
            await Navigation.PushAsync(new LijstOefeningen());
        }

        private async void Route_Instellingen_Clicked(object sender, EventArgs e) //navigatie naar lijst met routes, via button
        {
            await Navigation.PushAsync(new LijstRoutes());
        }
    }
}