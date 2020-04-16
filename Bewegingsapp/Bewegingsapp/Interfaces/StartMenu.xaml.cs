using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Bewegingsapp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await TextToSpeech.SpeakAsync(Route.Text);
            await TextToSpeech.SpeakAsync(Instellingen.Text);
        }

        private async void Route_Clicked(object sender, EventArgs e) //navigatie naar het route kiezen, listview van de routes
        {
            await Navigation.PushAsync(new OefeningAanUit());
        }

        private async void Instellingen_Clicked(object sender, EventArgs e) //navigatie naar het instellingen menu
        {
            await Navigation.PushAsync(new InstellingenMenu());
        }
    }
}
