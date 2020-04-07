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
    public partial class BewerkRouteDetailview : ContentPage
    {
        public bool OefeningAangepast = false;

        public BewerkRouteDetailview()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Oefeningen_Picker.ItemsSource = await App.Database.LijstOefeningen(); // itemsource = alle oefeningen die ooit aangemaakt zijn
            var BindingCoördinaat = (Coördinaat)BindingContext;
            if (BindingCoördinaat.IDOEfening != null) // voorkomt dat bij iedere coördinaat standaard de eerste oefening wordt toegevoegd
            {
                Oefeningen_Picker.SelectedIndex = Convert.ToInt32(BindingCoördinaat.IDOEfening) - 1; // ID's beginnen vanaf 1, maar de index telt vanaf 0
                OefeningAangepast = false; // dit telt als een SelectedIndexChanged event (staat standaard ingesteld op index -1),
                                           // maar telt niet als het wijzigen van een oefening
            }
            else
            {
                Oefeningen_Picker.SelectedIndex = -1; // oftewel, niks staat geselcteerd in de picker
                OefeningAangepast = false;
            }
        }

        private async void Info_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Soorten punten", "Er zijn 3 soorten punten: navigatie-punten, oefening-punten en onzichtbare punten." +
                    " Navigatie-punten hebben altijd een routeomschrijving en geen oefening, ze zijn bedoeld als aanwijzingen voor de slechtzienden" +
                    " Oefening-punten zijn bedoeld als optionele oefeningen tijdens het lopen, deze hebben altijd een oefening en geen routebeschrijving" +
                    " Onzichtbare punten zijn bedoeld om de route goed laten lopen, want deze volgt niet de straten, deze hebben geen oefening of routebeschrijving",
                    "OK");
        }

        private void Oefeningen_Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            OefeningAangepast = true;
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

        private void Reset_Button_Clicked(object sender, EventArgs e)
        {
            Oefeningen_Picker.SelectedIndex = -1;
            Routeomschrijving.Text = "";
            OefeningAangepast = false;
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool Verwijderen = await DisplayAlert("Punt verwijderen", "Weet u zeker dat u dit punt wilt verwijderen?", "ja", "nee");
            if (Verwijderen == true)
            {
                var coördinaat2 = (Coördinaat)BindingContext;
                await App.Database.VerwijderCoördinaat(coördinaat2);
                //var VorigePage = Navigation.NavigationStack.LastOrDefault();
                //Navigation.RemovePage(VorigePage);
                //Navigation.InsertPageBefore(new RouteToevoegenListview(), this);
                await Navigation.PopAsync();
            }
        }
    }
}