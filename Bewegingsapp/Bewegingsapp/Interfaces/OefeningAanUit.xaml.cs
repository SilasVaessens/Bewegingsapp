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

        private async void Ja_Oefening_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartRoute());
        }

        private async void Nee_Oefening_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartRoute());
        }
    }
}