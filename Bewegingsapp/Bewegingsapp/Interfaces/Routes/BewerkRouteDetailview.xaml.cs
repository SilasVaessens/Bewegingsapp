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

        public BewerkRouteDetailview()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Coördinaat coördinaat = (Coördinaat)BindingContext;
            Title = coördinaat.Nummer.ToString();
            Oefeningen_Picker.ItemsSource = await App.Database.LijstOefeningen(); // itemsource == alle oefeningen die ooit aangemaakt zijn
            var BindingCoördinaat = (Coördinaat)BindingContext;
            if (BindingCoördinaat.IDOEfening != null) // voorkomt dat bij iedere coördinaat standaard de eerste oefening wordt toegevoegd
            {
                Oefeningen_Picker.SelectedIndex = Convert.ToInt32(BindingCoördinaat.IDOEfening) - 1; // ID's beginnen vanaf 1, maar de index telt vanaf 0
            }
            else
            {
                Oefeningen_Picker.SelectedIndex = -1; // oftewel, niks staat geselecteerd in de picker
            }
            if (BindingCoördinaat.IDRoute == 1) //RCS route mag niet worden bewerkt
            {
                Delete.IsEnabled = false;
                Oefeningen_Picker.IsEnabled = false;
                Routeomschrijving.IsEnabled = false;
                Opslaan_Button.IsEnabled = false;
                Reset_Button.IsEnabled = false;
            }
        }

        private async void Info_Clicked(object sender, EventArgs e) //informatie knop, hoe de punten werken
        {
            await DisplayAlert("Soorten punten", "Er zijn 3 soorten punten: navigatie-punten, oefening-punten en onzichtbare punten.\n \n" +
                    "Navigatie-punten hebben altijd een routeomschrijving en geen oefening, ze zijn bedoeld als aanwijzingen voor de slechtzienden.\n \n" +
                    "Oefening-punten zijn bedoeld als optionele oefeningen voor tijdens het lopen, deze hebben altijd een oefening en een routebeschrijving.\n \n" +
                    "Onzichtbare punten zijn bedoeld om de routelijnen te goed laten lopen, want deze volgen niet de straten, deze hebben geen oefening of routebeschrijving.",
                    "OK");
        }


        private async void Opslaan_Button_Clicked(object sender, EventArgs e) //opslaan omschrijving en/of oefening van geselecteerde coördinaat
        {
            var coördinaat1 = (Coördinaat)BindingContext;
            if (Oefeningen_Picker.SelectedIndex == -1 & string.IsNullOrEmpty(Routeomschrijving.Text) == true) //geen oefening en geen omschrijving
            {
                string OnzichtbaarPunt = string.Format("Weet u zeker dat u punt {0} als een onzichtbaar punt wilt opslaan?", coördinaat1.Nummer.ToString());
                bool Onzichtbaar = await DisplayAlert("Opslaan onzichtbaar punt", OnzichtbaarPunt, "JA", "NEE");
                if (Onzichtbaar == true)
                {
                    coördinaat1.IDOEfening = null;
                    await App.Database.UpdateCoördinaat(coördinaat1);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                if (Oefeningen_Picker.SelectedIndex != -1) // oefening ID wordt alleen aangepast als er een ander item geselecteerd wordt
                {
                    coördinaat1.IDOEfening = Oefeningen_Picker.SelectedIndex + 1; // ID's beginnen vanaf 1, maar de index telt vanaf 0
                }
                if (Oefeningen_Picker.SelectedIndex == -1)
                {
                    coördinaat1.IDOEfening = null;
                }
                await App.Database.UpdateCoördinaat(coördinaat1);
                await Navigation.PopAsync();
            }

        }

        private void Reset_Button_Clicked(object sender, EventArgs e) //oefening deselecteren en routeomschrijving leeg maken
        {
            Oefeningen_Picker.SelectedIndex = -1;
            Routeomschrijving.Text = "";
        }

        private async void Delete_Clicked(object sender, EventArgs e) //Coördinaat verwijderen
        {
            var coördinaat2 = (Coördinaat)BindingContext;
            string VerwijderPunt = string.Format("Weet u zeker dat u punt {0} wilt verwijderen?", coördinaat2.Nummer.ToString());
            bool Verwijderen = await DisplayAlert("Punt verwijderen", VerwijderPunt, "JA", "NEE");
            if (Verwijderen == true)
            {
                await App.Database.VerwijderCoördinaat(coördinaat2);
                await App.Database.TeUpdatenCoördinatenRoute(coördinaat2.IDRoute);
                await Navigation.PopAsync();
            }
        }
    }
}