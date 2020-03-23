using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bewegingsapp.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BewerkOefening : ContentPage
    {
        public BewerkOefening()
        {
            InitializeComponent();
        }

        private async void Oefening_update_Clicked(object sender, EventArgs e)
        {
            var oefening = (Oefening)BindingContext;
            await App.Database.UpdateOefening(oefening);
            await Navigation.PopAsync();
        }

        private async void Oefening_verwijder_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Definitief verwijderen?", "Weet u zeker dat u deze oefening wilt verwijderen?", "ja", "nee");
            if (answer == true)
            {
                var oefening = (Oefening)BindingContext;
                await App.Database.VerwijderOefening(oefening);
                await Navigation.PopAsync();
            }

        }
    }
}