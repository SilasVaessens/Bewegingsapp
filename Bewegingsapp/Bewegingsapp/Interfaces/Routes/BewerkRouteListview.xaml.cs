using System.Collections.Generic;
using System.Threading.Tasks;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BewerkRouteListview : ContentPage
    {

        List<Coördinaat> BewerkAlleCoördinaten = new List<Coördinaat>();
        ObservableCollection<Coördinaat> Coördinaten = new ObservableCollection<Coördinaat>();


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
                    Coördinaten.Add(coördinaat);
                    await Task.Delay(5);
                }
            }
            Listview_Coördinaten_Bewerk.ItemsSource = Coördinaten;
            if (BindingRoute.IDRoute == 1)
            {
                Add_Punt.IsEnabled = false;
                Delete.IsEnabled = false;
            }
        }

        private async void Listview_Coördinaten_Bewerk_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BewerkAlleCoördinaten.Clear();
            Coördinaten.Clear();
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

        private async void Delete_Clicked(object sender, System.EventArgs e)
        {
            bool VerwijderenRoute = await DisplayAlert("Route verwijderen", "Weet u zeker dat u deze route wilt verwijderen?", "ja", "nee");
            if (VerwijderenRoute == true)
            {
                var VerwijderRoute = (Route)BindingContext;
                await App.Database.VerwijderCoördinatenRoute(VerwijderRoute.IDRoute);
                await App.Database.VerwijderRoute(VerwijderRoute);
                await Navigation.PopAsync();
            }
        }

        private void Add_Punt_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}