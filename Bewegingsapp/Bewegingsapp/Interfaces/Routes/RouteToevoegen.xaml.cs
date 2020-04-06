using Bewegingsapp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteToevoegen : ContentPage
    {
        public List<Coördinaat> CoördinatenRoute = new List<Coördinaat>(); // lijst met alle aangemaakte coördinaten, nodig voor het maken van polylines en het opslaan in de database van de coördinaten
        Route route;                            // niet verwijderen, dit zorgt ervoor dat alle functies die een route het hebben over hetzelfde route object, 
                                                // als het route object in iedere functie zelf wordt aangemaakt, dan crasht deze pagina bij het verlaten van deze pagina
        bool opgeslagen = false;                // controle of de bestaande route al eens opgeslagen is na het aanmaken van de route
        bool bugfix = true;                     // niet aankomen, voorkomt bug
        public List<Pin> PinsLijst = new List<Pin>(); // lijst met alle aangemaakte pins, is nodig voor het verwijderen van pins op de map
        public List<Polyline> PolylinesLijst = new List<Polyline>(); // lijst met alle aangemaakte pins, is nodig voor het verwijderen van polylines op de map 
        Coördinaat coördinaat;

        public RouteToevoegen()
        {
            InitializeComponent();
        }

        // wordt aangeroepen zodat een nieuwe lege route in de database gezet wordt. Deze is nodig, want objecten van de class Coördinaat 
        // hebben een route id nodig om gemaakt te worden, maar de route bestaat eigenlijk nog niet
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Route> LijstRouteIDs = await App.Database.LijstRoutes();
            if (LijstRouteIDs.Count == 0)
            {
                route = new Route()
                {
                    IDRoute = 1
                };
            }
            if (LijstRouteIDs.Count > 0)
            {
                route = new Route()
                {
                    IDRoute = LijstRouteIDs.Count + 1
                };
            }
            await App.Database.ToevoegenRoute(route);
            opgeslagen = false;
            if (String.IsNullOrEmpty(Naam_Route_toevoegen.Text) == false || CoördinatenRoute.Count != 0)
            {
                bugfix = false;
            }

            //start locatie aanpassen aan de locatie van de gebruiker
            if (bugfix == true)
            {
                Route_Punt_Verwijderen.IsEnabled = false; // voorkomt een out of index error als de gebruiker op de knop drukt terwijl er niks is om te verwijderen
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.High);
                    var location = await Geolocation.GetLocationAsync(request); //longitude, latitude en altitude van de gebruiker wordt hier opgevraagd

                    if (location != null)
                    {
                        Map_Route_Toevoegen.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(0.5))); //startpunt, locatie van gebruiker
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

        //Slaat de coördinaten op in de database en update de bestaande route
        private async void Route_opslaan_Clicked(object sender, EventArgs e)
        {
            if (bugfix == true)
            {
                opgeslagen = true;
                foreach (Coördinaat coördinaat in CoördinatenRoute)
                {
                    await App.Database.ToevoegenCoördinaat(coördinaat);
                    await Task.Delay(5);
                }
                route.NaamRoute = Naam_Route_toevoegen.Text;
                route.Coördinaten = CoördinatenRoute;
                await App.Database.UpdateRoute(route);
            }
            if (bugfix == false)
            {
                opgeslagen = true;
                await App.Database.VerwijderRoute(route);
                List<Route> Routes = await App.Database.LijstRoutes();
                Route UpdateRoute = Routes.Last();
                App.Database.VerwijderCoördinatenRoute(UpdateRoute.IDRoute);
                
                foreach (Coördinaat coördinaat1 in CoördinatenRoute)
                {
                    coördinaat1.IDRoute = UpdateRoute.IDRoute;
                    await App.Database.ToevoegenCoördinaat(coördinaat1);
                    await Task.Delay(5);
                }
                UpdateRoute.NaamRoute = Naam_Route_toevoegen.Text;
                UpdateRoute.Coördinaten = CoördinatenRoute;
                await App.Database.UpdateRoute(UpdateRoute);
            }
            await Navigation.PushAsync(new RouteToevoegenListview());
        }

        // verwijdert pins, coördinaten en polylines in de volgorde zoals ze door de gebruiker zijn toegevoegd
        private async void Route_Punt_Verwijderen_Clicked(object sender, EventArgs e)
        {
            bool verwijder = await DisplayAlert("Route punt verwijderen", "Weet u zeker of u dat u het laatst aangemaakte route punt wilt verwijderen?", "ja", "nee");
            if (verwijder == true)
            {
                Map_Route_Toevoegen.Pins.Remove(PinsLijst.Last());
                PinsLijst.Remove(PinsLijst.Last());
                if (CoördinatenRoute.Count >= 2)
                {
                    Map_Route_Toevoegen.MapElements.Remove(PolylinesLijst.Last());
                    PolylinesLijst.Remove(PolylinesLijst.Last());
                }
                CoördinatenRoute.Remove(CoördinatenRoute.Last());
                // voorkomt een out of index error als de gebruiker op de knop drukt terwijl er niks is om te verwijderen
                if (PinsLijst.Count == 0)
                {
                    Route_Punt_Verwijderen.IsEnabled = false; 
                }
            }
        }

        // dit event bevat alle handelingen die kunnen gebeuren als er op de map geklikt wordt
        private async void Map_Route_Toevoegen_MapClicked(object sender, MapClickedEventArgs e)
        {
            //longitude en latitude zijn vereist voor het maken van pins op de map
            var location1 = e.Position.Latitude;
            var location2 = e.Position.Longitude;
            int NummerCoördinaat = CoördinatenRoute.Count + 1;

            //maak nieuwe pin aan op aangeklikte plek op de map
            Pin pin = new Pin
            {
                Label = NummerCoördinaat.ToString(),
                Type = PinType.Place,
                Position = new Position(location1, location2)
            };
            Map_Route_Toevoegen.Pins.Add(pin);
            PinsLijst.Add(pin);

            //maak object van class Coördinaat aan die bij de nieuwe route hoort
            coördinaat = new Coördinaat
            {
                Nummer = NummerCoördinaat,
                Locatie1 = location1,
                Locatie2 = location2,
                IDRoute = await App.Database.KrijgRouteID() // hiermee krijg je het ID van de route die je nu aan het toevoegen bent
            };

            //voeg coördinaat object toe aan de list, maakt het mogelijk om later polylines te maken
            CoördinatenRoute.Add(coördinaat);

            //zodra er 2 of meer objecten in de eerder genoemde list zijn, wordt er een polyline getrokken tussen de laatste 2 Coördinaten / pins op de map
            if (CoördinatenRoute.Count >= 2)
            {
                Polyline polyline = new Polyline
                {
                    StrokeColor = Color.Blue,
                    StrokeWidth = 10,
                    Geopath =
                    {
                        new Position(CoördinatenRoute[CoördinatenRoute.Count - 2].Locatie1, CoördinatenRoute[CoördinatenRoute.Count -2].Locatie2), // pakt longitude en latitude van voorlaatste item in de list
                        new Position(CoördinatenRoute.Last().Locatie1, CoördinatenRoute.Last().Locatie2) // pakt longitude en latitude van laatste item in de list
                    }
                };
                //voegt de net gemaakte polyline toe aan de map
                Map_Route_Toevoegen.MapElements.Add(polyline);
                PolylinesLijst.Add(polyline);
            }

            Route_Punt_Verwijderen.IsEnabled = true; // zorgt dat de knop werkt zodra er weer pins en polylines bestaan

            //event dat gebeurd als je op een al bestaande pin klikt
            pin.MarkerClicked += (s, args) =>
            {
                args.HideInfoWindow = true; // zorgt ervoor dat het popup-window met label en adres niet verschijnt
            };
        }
    }
}