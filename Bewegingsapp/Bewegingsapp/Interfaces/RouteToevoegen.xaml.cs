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
        Route route;

        public RouteToevoegen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            route = new Route() { };
            await App.Database.ToevoegenRoute(route);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if (CoördinatenRoute.Count == 0 || string.IsNullOrEmpty(Naam_Route_toevoegen.Text))
            {
                await App.Database.VerwijderRoute(route);
            }
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

            CoördinatenRoute.Add(coördinaat);
            Map_Route_Toevoegen.Pins.Add(pin);

            if (CoördinatenRoute.Count >= 2)
            {
                Polyline polyline = new Polyline
                {
                    StrokeColor = Color.Blue,
                    StrokeWidth = 10,
                    Geopath =
                    {
                        new Position(CoördinatenRoute[CoördinatenRoute.Count - 2].locatie1, CoördinatenRoute[CoördinatenRoute.Count -2].locatie2),
                        new Position(CoördinatenRoute.Last().locatie1, CoördinatenRoute.Last().locatie2)
                    }
                };
                Map_Route_Toevoegen.MapElements.Add(polyline);
            }

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