using System.Collections.Generic;
using System.Threading.Tasks;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BewerkRouteListview : ContentPage
    {

        List<Coördinaat> BewerkAlleCoördinaten = new List<Coördinaat>();
        List<Coördinaat> BewerkCoördinaatRoute = new List<Coördinaat>();

        public BewerkRouteListview()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var BindingRoute = (Route)BindingContext;
            Title = BindingRoute.NaamRoute;
            BewerkAlleCoördinaten = await App.Database.LijstCoördinaten();
            foreach (Coördinaat coördinaat in BewerkAlleCoördinaten)
            {
                if (coördinaat.IDRoute == BindingRoute.IDRoute)
                {
                    BewerkCoördinaatRoute.Add(coördinaat);
                    await Task.Delay(5);
                }
            }
            Listview_Coördinaten_Bewerk.ItemsSource = BewerkCoördinaatRoute;
        }

        private async void Listview_Coördinaten_Bewerk_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new BewerkRouteDetailview { BindingContext = e.SelectedItem });
        }

        private async void Klaar_Bewerk_Clicked(object sender, System.EventArgs e)
        {
            bool KlaarBewerken = await DisplayAlert("Route opslaan", "Bent u klaar met het bewerken van de route?", "ja", "nee");
            if (KlaarBewerken == true)
            {
                await Navigation.PopAsync();
            }
        }
    }
}