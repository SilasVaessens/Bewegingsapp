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
        List<Coördinaat> CoördinatenRoute = new List<Coördinaat>(); // lijst met alle aangemaakte coördinaten, nodig voor het maken van polylines en het opslaan in de database van de coördinaten
        Route route; // niet verwijderen, dit zorgt ervoor dat alle functies die een route het hebben over hetzelfde route object, 
        Pin pin;
        Polyline polyline;
        Coördinaat coördinaat;

                    // als het route object in iedere functie zelf wordt aangemaakt, dan crasht deze pagina bij het verlaten van deze pagina
        bool opgeslagen = false; // controle of de bestaande route al eens opgeslagen is na het aanmaken van de route

        public RouteToevoegen()
        {
            InitializeComponent();
        }

        // wordt aangeroepen zodat een nieuwe lege route in de database gezet wordt. Deze is nodig, want objecten van de class Coördinaat 
        // hebben een route id nodig om gemaakt te worden, maar de route bestaat eigenlijk nog niet
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            route = new Route() { };
            await App.Database.ToevoegenRoute(route);
        }

        // zorgt ervoor dat er geen lege routes in de database terecht komen, verwijdert de eerder aangemaakte lege route
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if (opgeslagen == false) // deze bool controleert of de route al eens opgeslagen is, zo niet dan wordt de route verwijdert
            {
                await App.Database.VerwijderRoute(route);
            }
        }

        //moet nog aangepast wordt, voegt nu alleen de naam toe, niet de lijst met coördinaten. Slaat de lijst op in de database.
        //moet nog vervangen worden door App.Database.UpdateRoute want de te maken route bestaat al in de database
        private async void Route_opslaan_Clicked(object sender, EventArgs e)
        {
            opgeslagen = true; // zorgt ervoor dat de bestaande route niet verwijdert wordt na de pagina te verlaten en opgeslagen te hebben
            route = new Route()
            {
                NaamRoute = Naam_Route_toevoegen.Text
            };
            await App.Database.ToevoegenRoute(route);
            await Navigation.PopAsync();
        }

        //nutteloos, RoeteToevoegen.xaml moet nog aangepast worden zodat deze eruit gegooid kan worden
        private async void Route_Punt_Verwijderen_Clicked(object sender, EventArgs e)
        {
            bool verwijder = await DisplayAlert("Route punt verwijderen", "Weet u zeker of u dit route punt wilt verwijderen?", "ja", "nee");
            if (verwijder == true)
            {
                    Map_Route_Toevoegen.Pins.Remove(pin); // verwijdert de aangetikte pin
                    Map_Route_Toevoegen.MapElements.Remove(polyline);
                    CoördinatenRoute.Remove(CoördinatenRoute.Last());
            }
        }

        // dit event bevat alle handelingen die kunnen gebeuren als er op de map geklikt wordt
        private async void Map_Route_Toevoegen_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            //longitude en latitude zijn vereist voor het maken van pins op de map
            var location1 = e.Position.Latitude;
            var location2 = e.Position.Longitude;

            //maak nieuwe pin aan op aangeklikte plek op de map
            pin = new Pin
            {
                Label = "label",
                Type = PinType.Place,
                Position = new Position(location1, location2)
            };
            Map_Route_Toevoegen.Pins.Add(pin);

            //maak object van class Coördinaat aan die bij de nieuwe route hoort
            coördinaat = new Coördinaat
            {
                locatie1 = location1,
                locatie2 = location2,
                IDRoute = await App.Database.KrijgRouteID() // hiermee krijg je het ID van de route die je nu aan het toevoegen bent
            };

            //voeg coördinaat object toe aan de list, maakt het mogelijk om later polylines te maken
            CoördinatenRoute.Add(coördinaat);

            //zodra er 2 of meer objecten in de eerder genoemde list zijn, wordt er een polyline getrokken tussen de laatste 2 Coördinaten / pins op de map
            if (CoördinatenRoute.Count >= 2)
            {
                polyline = new Polyline
                {
                    StrokeColor = Color.Blue,
                    StrokeWidth = 10,
                    Geopath =
                    {
                        new Position(CoördinatenRoute[CoördinatenRoute.Count - 2].locatie1, CoördinatenRoute[CoördinatenRoute.Count -2].locatie2), // pakt longitude en latitude van voorlaatste item in de list
                        new Position(CoördinatenRoute.Last().locatie1, CoördinatenRoute.Last().locatie2) // pakt longitude en latitude van laatste item in de list
                    }
                };
                //voegt de netgemaakte polyline toe aan de map
                Map_Route_Toevoegen.MapElements.Add(polyline);
            }

        }
    }
}