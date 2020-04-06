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
    public partial class RouteToevoegenDetailpage : ContentPage
    {
        bool OefeningAangepast = false; // controleert of de bijhorende oefening is aangepast

        public RouteToevoegenDetailpage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            Oefeningen_Picker.ItemsSource = await App.Database.LijstOefeningen(); // itemsource = alle oefeningen die ooit aangemaakt zijn
            var coördinaat = (Coördinaat)BindingContext;
            if (coördinaat.IDOEfening != null) // voorkomt dat bij iedere coördinaat standaard de eerste oefening wordt toegevoegd
            {
                Oefeningen_Picker.SelectedIndex = Convert.ToInt32(coördinaat.IDOEfening) - 1; // ID's beginnen vanaf 1, maar de index telf vanaf 0
                OefeningAangepast = false; // dit telt als een SelectedIndexChanged event (staat standaard ingesteld op index -1),
                                           // maar telt niet als het wijzigen van een oefening
            }
            else
            {
                Oefeningen_Picker.SelectedIndex = -1; // oftewel, niks staat geselcteerd in de picker
                OefeningAangepast = false;
            }
        }

        private async void Opslaan_Button_Clicked(object sender, EventArgs e)
        {
            var coördinaat1 = (Coördinaat)BindingContext;
            if (OefeningAangepast == true) // Oefening ID wordt alleen aangepast als er een ander item geselecteerd wordt
            {
                coördinaat1.IDOEfening = Oefeningen_Picker.SelectedIndex + 1; // ID's beginnen vanaf 1, maar de index telt vanaf 0
            }
            await App.Database.UpdateCoördinaat(coördinaat1);
            await Navigation.PopAsync();
        }

        private void Oefeningen_Picker_SelectedIndexChanged(object sender, EventArgs e) // popt iedere keer op als het geselcteerde item verandert
        {
            OefeningAangepast = true; 
        }

        private void Reset_Button_Clicked(object sender, EventArgs e)
        {
            Oefeningen_Picker.SelectedIndex = -1;
            Routeomschrijving.Text = ""; 
            OefeningAangepast = false;
        }

        private async void Info_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Soorten punten", "Er zijn 3 soorten punten: navigatie-punten, oefening-punten en onzichtbare punten." +
                                " Navigatie-punten hebben altijd een routeomschrijving en geen oefening, ze zijn bedoeld als aanwijzingen voor de slechtzienden" + 
                                " Oefening-punten zijn bedoeld als optionele oefeningen tijdens het lopen, deze hebben altijd een oefening en geen routebeschrijving" +
                                " Onzichtbare punten zijn bedoeld om de route goed laten lopen, want deze volgt niet de straten, deze hebben geen oefening of routebeschrijving",
                                "OK");
        }
    }
}