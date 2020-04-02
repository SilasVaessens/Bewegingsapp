using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteKiezen : ContentPage
    {
        public RouteKiezen()
        {
            InitializeComponent();

        }

        private async void OK_Route_Kiezen_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Bevestiging route", "Weet u zeker dat u deze route wilt kiezen?", "ja", "nee");
            if (answer == true)
            {
                await Navigation.PushAsync(new OefeningAanUit());
            }
        }
    }
}