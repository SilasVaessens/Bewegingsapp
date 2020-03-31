using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bewegingsapp.Model;
using Xamarin.Forms.Maps;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteToevoegen : ContentPage
    {
        List<Coördinaat> CoördinatenRoute = new List<Coördinaat>();

        public RouteToevoegen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Route route = new Route() { };
            await App.Database.ToevoegenRoute(route);
        }

        private async void Route_opslaan_Clicked(object sender, EventArgs e)
        {
            Route route = new Route()
            {
                NaamRoute = Naam_Route_toevoegen.Text
            };
            await App.Database.ToevoegenRoute(route);
            await Navigation.PopAsync();
        }

        private void Route_Punt_Verwijderen_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void Map_Route_Toevoegen_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            var location1 = e.Position.Latitude;
            var location2 = e.Position.Longitude;

            Pin pin = new Pin
            {
                Label = "test label",
                Address = "test adress",
                Type = PinType.Place,
                Position = new Position(location1, location2)
            };

            Coördinaat coördinaat = new Coördinaat
            {
                locatie1 = location1,
                locatie2 = location2,
                IDRoute = await App.Database.KrijgRouteID()
            };

            Console.WriteLine(coördinaat.locatie1);
            Console.WriteLine(coördinaat.locatie2);
            Console.WriteLine(coördinaat.IDRoute);


            Map_Route_Toevoegen.Pins.Add(pin);
            pin.MarkerClicked += async (s, args) =>
            {
                args.HideInfoWindow = true;
                bool verwijder = await DisplayAlert("Route punt verwijderen", "Weet u zeker of u dit route punt wilt verwijderen?", "ja", "nee");
                if (verwijder == true)
                {
                    Map_Route_Toevoegen.Pins.Remove(pin);
                }
            };
        }

        
    }
}