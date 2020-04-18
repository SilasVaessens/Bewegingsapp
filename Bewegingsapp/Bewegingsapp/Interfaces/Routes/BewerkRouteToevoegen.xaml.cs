using System;
using System.Collections.Generic;
using System.Linq;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BewerkRouteToevoegen : ContentPage
    {
        public List<Coördinaat> CoördinatenRoute = new List<Coördinaat>(); // lijst met alle aangemaakte coördinaten, nodig voor het maken van polylines en het opslaan in de database van de coördinaten
        Coördinaat coördinaat;
        Polyline polyline;
        public List<Pin> NieuwPunt = new List<Pin>(); // hier hoort maar 1 pin in te zitten

        public BewerkRouteToevoegen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Route route = (Route)BindingContext; //ophalen van geselecteerde route
            CoördinatenRoute = await App.Database.LijstCoördinatenRoute(route.IDRoute); //coördinaten ophalen van geselecteerde route

            var latitude = CoördinatenRoute[0].Locatie1;
            var Longitude = CoördinatenRoute[0].Locatie2;
            Map_Route_Bewerken.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, Longitude), Distance.FromKilometers(0.5))); //startpunt, locatie van eerste coördinaat

            foreach (Coördinaat coördinaat in CoördinatenRoute) //voor iedere coördinaat in de geslecteerde route
            {
                double location1 = coördinaat.Locatie1;
                double location2 = coördinaat.Locatie2;
                Pin pin = new Pin
                {
                    Label = coördinaat.Nummer.ToString(),
                    Type = PinType.Place,
                    Position = new Position(location1, location2)
                };
                pin.MarkerClicked += (s, args) => // verbergt de label die verschijnt als je op de pin klikt
                {
                    args.HideInfoWindow = true;
                };
                Map_Route_Bewerken.Pins.Add(pin); //pin toevoegen aan de map
                if (Map_Route_Bewerken.Pins.Count >= 2) //als er 2 of meer pins zijn
                {
                    Polyline polyline = new Polyline
                    {
                        StrokeColor = Color.Blue,
                        StrokeWidth = 10,
                        Geopath =
                        {
                            new Position(CoördinatenRoute[Map_Route_Bewerken.Pins.Count - 1].Locatie1, CoördinatenRoute[Map_Route_Bewerken.Pins.Count - 1].Locatie2), // pakt longitude en latitude van voorlaatste item in de list
                            new Position(CoördinatenRoute[Map_Route_Bewerken.Pins.Count - 2].Locatie1, CoördinatenRoute[Map_Route_Bewerken.Pins.Count - 2].Locatie2) // pakt longitude en latitude van laatste item in de list
                        }
                    };
                    Map_Route_Bewerken.MapElements.Add(polyline); //polyline tekenen
                }

            }

        }

        private async void Toevoegen_Clicked(object sender, EventArgs e) //opslaan van nieuw toegevoegde coördinaat
        {
            if (NieuwPunt.Count == 0) // opslaan niet mogelijk als er geen nieuwe pin is
            {
                await DisplayAlert("Geen punt toegevoegd", "U heeft geen nieuw punt toegevoegd, dus opslaan is niet mogelijk.", "OK");
            }
            else // opslaan wel mogelijk als er wel een nieuwe pin is
            {
                Route UpdateCoördinatenRoute = (Route)BindingContext;
                await App.Database.ToevoegenCoördinaat(coördinaat);
                await Navigation.PopAsync();
            }
        }

        private void Map_Route_Bewerken_MapClicked(object sender, MapClickedEventArgs e)
        {
            Route route = (Route)BindingContext; // vereist voor het krijgen van IDRoute
            //longitude en latitude zijn vereist voor het maken van pins op de map
            double location1 = e.Position.Latitude;
            double location2 = e.Position.Longitude;
            int NummerCoördinaat = CoördinatenRoute.Count + 1;

            if (NieuwPunt.Count == 1) //hier wordt ervoor gezord dat er niet meerdere nieuwe pins tegelijk kunnen worden neergezet
            {
                Map_Route_Bewerken.Pins.Remove(NieuwPunt.Last()); //pin op de map
                NieuwPunt.Remove(NieuwPunt.Last()); // pin in pinslijst
                Map_Route_Bewerken.MapElements.Remove(polyline); // polyline naar pin
            }
            Pin pin = new Pin //maak nieuwe pin aan op aangeklikte plek op de map
            {
                Label = NummerCoördinaat.ToString(),
                Type = PinType.Place,
                Position = new Position(location1, location2)
            };
            pin.MarkerClicked += (s, args) => // verbergt de label die verschijnt als je op de pin klikt
            {
                args.HideInfoWindow = true;
            };
            Map_Route_Bewerken.Pins.Add(pin); //pin toevoegen aan de map
            NieuwPunt.Add(pin); 

            coördinaat = new Coördinaat
            {
                Nummer = NummerCoördinaat,
                Locatie1 = location1,
                Locatie2 = location2,
                IDOEfening = null,
                IDRoute = route.IDRoute
            };

            polyline = new Polyline //maak nieuwe polyline tussen nieuwe pin en laatste pin in route
            {
                StrokeColor = Color.Red,
                StrokeWidth = 10,
                Geopath =
                        {
                            new Position(CoördinatenRoute.Last().Locatie1, CoördinatenRoute.Last().Locatie2), // pakt longitude en latitude van voorlaatste item in de list
                            new Position(coördinaat.Locatie1, coördinaat.Locatie2) // pakt longitude en latitude van laatste item in de list
                        }
            };
            Map_Route_Bewerken.MapElements.Add(polyline); //polyline tekenen
        }

        private async void Info_Clicked(object sender, EventArgs e) //informatie knop, hoe het toevoegen werkt
        {
            await DisplayAlert("Punt toevoegen", "Het is niet mogelijk om meer dan 1 punt tegelijk toe te voegen aan een route. \n \n" +
                                "Het nieuwe punt wordt automatisch toegevoegd aan het einde van de lijst met route punten, het is niet mogelijk " +
                                "om het nieuwe punt tussendoor of aan het begin toe te voegen.", "OK");
        }
    }
}