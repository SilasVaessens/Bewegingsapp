using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OefeningAanUit : ContentPage
    {
        public OefeningAanUit()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await TextToSpeech.SpeakAsync(Label_Vraag.Text);
            await TextToSpeech.SpeakAsync(Nee_Oefening.Text);
            await TextToSpeech.SpeakAsync(Ja_Oefening.Text);
        }

        private async void Ja_Oefening_Clicked(object sender, EventArgs e) //route met oefeningen, via button
        {
            App.Database.OefeningAanUit = true;
            await Navigation.PushAsync(new RouteKiezen());
        }

        private async void Nee_Oefening_Clicked(object sender, EventArgs e) //route zonder oefeningen, via button
        {
            App.Database.OefeningAanUit = false;
            await Navigation.PushAsync(new RouteKiezen());
        }
    }
}