using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bewegingsapp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartRoute : ContentPage
    {
        public List<Pin> PinsLijst = new List<Pin>(); // lijst met alle aangemaakte pins, is nodig voor het verwijderen van pins op de map
        public List<Coördinaat> GekozenRoute = new List<Coördinaat>(); // lijst met alle coördinaten die bij de geselecteerde route horen
        public StartRoute()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var route = (Route)BindingContext; // verzamel informatie van geselecteerde route
            GekozenRoute = await App.Database.LijstCoördinatenRoute(route.IDRoute);
            foreach (Coördinaat coördinaat1 in GekozenRoute)
            {
                var location1 = coördinaat1.Locatie1;
                var location2 = coördinaat1.Locatie2;
                //maak nieuwe pin aan op aangeklikte plek op de map
                Pin pin = new Pin
                {
                    Label = coördinaat1.Nummer.ToString(),
                    Type = PinType.Place,
                    Position = new Position(location1, location2)
                };
                if (coördinaat1.IDOEfening != null || coördinaat1.RouteBeschrijving != null)
                {
                    Map_Start_Route.Pins.Add(pin);
                }
                PinsLijst.Add(pin);

                if (PinsLijst.Count >= 2)
                {
                    Polyline polyline = new Polyline
                    {
                        StrokeColor = Color.Blue,
                        StrokeWidth = 10,
                        Geopath =
                        {
                            new Position(GekozenRoute[PinsLijst.Count - 1].Locatie1, GekozenRoute[PinsLijst.Count - 1].Locatie2), // pakt longitude en latitude van voorlaatste item in de list
                            new Position(GekozenRoute[PinsLijst.Count - 2].Locatie1, GekozenRoute[PinsLijst.Count - 2].Locatie2) // pakt longitude en latitude van laatste item in de list
                        }
                    };

                    Map_Start_Route.MapElements.Add(polyline);
                }

            }

            Polyline polyline1 = new Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 10,
                Geopath =
                {
                    new Position(GekozenRoute[0].Locatie1, GekozenRoute[0].Locatie2), // pakt longitude en latitude van voorlaatste item in de list
                    new Position(GekozenRoute.Last().Locatie1, GekozenRoute.Last().Locatie2) // pakt longitude en latitude van laatste item in de list
                }

            };

            Map_Start_Route.MapElements.Add(polyline1);

            double locatie1 = GekozenRoute[0].Locatie1;
            double locatie2 = GekozenRoute[0].Locatie2;
            Map_Start_Route.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(locatie1, locatie2), Distance.FromMeters(50))); //startpunt, locatie van gebruiker


        }


        private async void Start_Route_Clicked(object sender, EventArgs e)
        {

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request); //longitude, latitude en altitude van de gebruiker wordt hier opgevraagd

                if (location != null)
                {
                    Map_Start_Route.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(50))); //startpunt, locatie van gebruiker
                }
            }
            catch (FeatureNotSupportedException NotSupported)
            {
                // Verwerkt not supported on device exception
            }
            catch (FeatureNotEnabledException NotEnabled)
            {
                // Verwerkt not enabled on device exception
            }
            catch (PermissionException NotAllowed)
            {
                // Verwerkt permission exception
            }
            catch (Exception NoLocation)
            {
                // Locatie is niet verkregen
            }
        }

        private void Map_Start_Route_MapClicked(object sender, MapClickedEventArgs e)
        {

        }
    }
}