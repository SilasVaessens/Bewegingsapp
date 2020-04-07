using System;

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

        private async void Ja_Oefening_Clicked(object sender, EventArgs e) //route met oefeningen, via button
        {
            await Navigation.PushAsync(new RouteKiezen());
        }

        private async void Nee_Oefening_Clicked(object sender, EventArgs e) //route zonder oefeningen, via button
        {
            await Navigation.PushAsync(new RouteKiezen());
        }
    }
}