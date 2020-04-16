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
            Oefeningen_Picker.ItemsSource = await App.Database.LijstOefeningen(); // itemsource = alle oefeningen die ooit aangemaakt zijn
            var BindingCoördinaat = (Coördinaat)BindingContext;
            if (BindingCoördinaat.IDOEfening != null) // voorkomt dat bij iedere coördinaat standaard de eerste oefening wordt toegevoegd
            {
                Oefeningen_Picker.SelectedIndex = Convert.ToInt32(BindingCoördinaat.IDOEfening) - 1; // ID's beginnen vanaf 1, maar de index telt vanaf 0
            }
            else
            {
                Oefeningen_Picker.SelectedIndex = -1; // oftewel, niks staat geselecteerd in de picker
            }
            if (BindingCoördinaat.IDRoute == 1)
            {
                Delete.IsEnabled = false;
                Oefeningen_Picker.IsEnabled = false;
                Routeomschrijving.IsEnabled = false;
                Opslaan_Button.IsEnabled = false;
                Reset_Button.IsEnabled = false;
            }
        }

        private async void Info_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Soorten punten", "Er zijn 3 soorten punten: navigatie-punten, oefening-punten en onzichtbare punten.\n \n" +
                    "Navigatie-punten hebben altijd een routeomschrijving en geen oefening, ze zijn bedoeld als aanwijzingen voor de slechtzienden.\n \n" +
                    "Oefening-punten zijn bedoeld als optionele oefeningen tijdens het lopen, deze hebben altijd een oefening en een routebeschrijving, zodat verder gelopen kan worden na de oefening.\n \n" +
                    "Onzichtbare punten zijn bedoeld om de route goed laten lopen, want deze volgt niet de straten, deze hebben geen oefening of routebeschrijving.",
                    "OK");
        }


        private async void Opslaan_Button_Clicked(object sender, EventArgs e)
        {
            var coördinaat1 = (Coördinaat)BindingContext;
            if (Oefeningen_Picker.SelectedIndex == -1 & string.IsNullOrEmpty(Routeomschrijving.Text) == true)
            {
                bool Onzichtbaar = await DisplayAlert("Opslaan onzichtbaar punt", "Weet u zeker dat u dit als een onzichtbaar punt?", "ja", "nee");
                if (Onzichtbaar == true)
                {
                    coördinaat1.IDOEfening = null;
                    await App.Database.UpdateCoördinaat(coördinaat1);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                if (Oefeningen_Picker.SelectedIndex != -1) // Oefening ID wordt alleen aangepast als er een ander item geselecteerd wordt
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

        private void Reset_Button_Clicked(object sender, EventArgs e)
        {
            Oefeningen_Picker.SelectedIndex = -1;
            Routeomschrijving.Text = "";
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool Verwijderen = await DisplayAlert("Punt verwijderen", "Weet u zeker dat u dit punt wilt verwijderen?", "ja", "nee");
            if (Verwijderen == true)
            {
                var coördinaat2 = (Coördinaat)BindingContext;
                await App.Database.VerwijderCoördinaat(coördinaat2);
                await App.Database.TeUpdatenCoördinatenRoute(coördinaat2.IDRoute);
                await Navigation.PopAsync();
            }
        }
    }
}