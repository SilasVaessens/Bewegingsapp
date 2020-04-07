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
        Pin pin;
        List<Coördinaat> BewerkAlleCoördinaten = new List<Coördinaat>(); // lijst met alle opgeslagen coördinaten
        List<Coördinaat> BewerkCoördinaatRoute = new List<Coördinaat>(); // lijst met alle coördinaten die bij de geselecteerde route horen
        public StartRoute()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var route = (Route)BindingContext; // verzamel informatie van geselecteerde route
            BewerkAlleCoördinaten = await App.Database.LijstCoördinaten(); // alle opgeslagen coördinaten toevoegen aan een lijst
            foreach (Coördinaat coördinaat in BewerkAlleCoördinaten)
            {
                if (coördinaat.IDRoute == route.IDRoute)
                {
                    BewerkCoördinaatRoute.Add(coördinaat); //coördinaten di een gelijke IDroute hebben toevoegen aan een lijst
                    await Task.Delay(5);
                }
            }

            foreach (Coördinaat coördinaat1 in BewerkCoördinaatRoute)
            {
                var location1 = coördinaat1.Locatie1;
                var location2 = coördinaat1.Locatie2;
                //maak nieuwe pin aan op aangeklikte plek op de map
                Pin pin = new Pin
                {
                    Label = coördinaat1.IDCoördinaat.ToString(),
                    Type = PinType.Place,
                    Position = new Position(location1, location2)
                };
                Map_Start_Route.Pins.Add(pin);
                PinsLijst.Add(pin);

                if (PinsLijst.Count >= 2)
                {
                    Polyline polyline = new Polyline
                    {
                        StrokeColor = Color.Blue,
                        StrokeWidth = 10,
                        Geopath =
                        {
                            new Position(BewerkCoördinaatRoute[PinsLijst.Count - 1].Locatie1, BewerkCoördinaatRoute[PinsLijst.Count - 1].Locatie2), // pakt longitude en latitude van voorlaatste item in de list
                            new Position(BewerkCoördinaatRoute[PinsLijst.Count - 2].Locatie1, BewerkCoördinaatRoute[PinsLijst.Count - 2].Locatie2) // pakt longitude en latitude van laatste item in de list
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
                    new Position(BewerkCoördinaatRoute[0].Locatie1, BewerkCoördinaatRoute[0].Locatie2), // pakt longitude en latitude van voorlaatste item in de list
                    new Position(BewerkCoördinaatRoute.Last().Locatie1, BewerkCoördinaatRoute.Last().Locatie2) // pakt longitude en latitude van laatste item in de list
                }

            };

            Map_Start_Route.MapElements.Add(polyline1);

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request); //longitude, latitude en altitude van de gebruiker wordt hier opgevraagd

                if (location != null)
                {
                    Map_Start_Route.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(0.5))); //startpunt, locatie van gebruiker
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