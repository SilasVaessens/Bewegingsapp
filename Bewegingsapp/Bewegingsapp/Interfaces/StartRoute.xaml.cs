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
            var route = (Route)BindingContext; // verzamel informatie van geselecteerde route, deze is bij RouteKiezen.xaml.cs doorgeven
            GekozenRoute = await App.Database.LijstCoördinatenRoute(route.IDRoute); //ophalen van coördinaten geselecteerde route
            foreach (Coördinaat coördinaat1 in GekozenRoute) //pins neerzetten voor de punten(coördinaten) uit de lijst
            {
                double location1 = coördinaat1.Locatie1;
                double location2 = coördinaat1.Locatie2;
                Pin pin = new Pin
                {
                    Label = coördinaat1.Nummer.ToString(),
                    Type = PinType.Place,
                    Position = new Position(location1, location2)
                };
                pin.MarkerClicked += (s, args) =>
                {
                    args.HideInfoWindow = true;
                };
                if (coördinaat1.IDOEfening != null || coördinaat1.RouteBeschrijving != null) // zorgt ervoor dat onzichtbare punten niet worden weergegeven als pins
                {
                    Map_Start_Route.Pins.Add(pin);
                }
                PinsLijst.Add(pin);

                if (PinsLijst.Count >= 2) //als er minstens 2 punten zijn
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

                    Map_Start_Route.MapElements.Add(polyline); //polyline wordt getekent tussen 2 punten
                }

            }
            if (route.EindeIsBegin == true)
            {
                Polyline polyline1 = new Polyline
                {
                    StrokeColor = Color.Blue,
                    StrokeWidth = 10,
                    Geopath =
                    {
                        new Position(GekozenRoute[0].Locatie1, GekozenRoute[0].Locatie2), // pakt longitude en latitude van eerste item in de list
                        new Position(GekozenRoute.Last().Locatie1, GekozenRoute.Last().Locatie2) // pakt longitude en latitude van laatste item in de list
                    }
                };
                Map_Start_Route.MapElements.Add(polyline1); //polyline van laatste coördinaat naar eerste coördinaat
            }

            double locatie1 = GekozenRoute[0].Locatie1;
            double locatie2 = GekozenRoute[0].Locatie2;
            Map_Start_Route.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(locatie1, locatie2), Distance.FromMeters(50))); //startpunt, locatie eerste coördinaat gekozen route
            Tekst.Text = String.Format("Om de {0} route te starten, moet u zich op het start punt bevinden en op de start knop duwen", route.NaamRoute);
            await TextToSpeech.SpeakAsync(Tekst.Text);
        }

        private async void Start_Route_Clicked(object sender, EventArgs e)
        {
            bool RouteGestart = true;
            int HuidigCoördinaat = 0; //nodig voor het indexen van de volgende locatie, afstand meten tussen gebruiker en volgende punt
            var route = (Route)BindingContext;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request); //longitude, latitude en altitude van de gebruiker wordt hier opgevraagd

                Location Gebruiker = new Location(location); //locatie gebruiker
                Location BeginPunt = new Location(GekozenRoute[0].Locatie1, GekozenRoute[0].Locatie2); //locatie startpunt route
                double AfstandGebruikerBeginpunt = Location.CalculateDistance(Gebruiker, BeginPunt, DistanceUnits.Kilometers); //afstand berekenen tussen gebruiker en startpunt
                if (AfstandGebruikerBeginpunt > 0.010) //als de afstand groter is dan 10 meter
                {
                    RouteGestart = false; //als dit false is wordt de route niet gestart
                    Tekst.Text = String.Format("U bent niet op het startpunt van de {0} route", route.NaamRoute);
                    await TextToSpeech.SpeakAsync(Tekst.Text);
                    await Task.Delay(5000);
                    Tekst.Text = null;
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

            while (RouteGestart == true) //binnen 10 meter van de start locatie op start route klikken
            {
                Start_Route.IsEnabled = false; //button wordt disabled
                Start_Route.Text = "Onderweg"; //button tekst veranderd
                if (HuidigCoördinaat == 0)
                {
                    Tekst.Text = String.Format("U bent begonnen aan het lopen van de {0} route", route.NaamRoute);
                    await TextToSpeech.SpeakAsync(Tekst.Text);
                    HuidigCoördinaat = 24;
                }
                List<Oefening> Oefeningen = await App.Database.LijstOefeningen();
                Map_Start_Route.HasScrollEnabled = false; //kunt de kaart niet zelf aanpassen als de route is gestart
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    var location = await Geolocation.GetLocationAsync(request); //longitude, latitude en altitude van de gebruiker wordt hier opgevraagd

                    if (location != null)
                    {
                        Map_Start_Route.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(50))); //om de 4 seconden map centreren naar locatie van gebruiker
                        await Task.Delay(4000);
                    }

                    Location Gebruiker = new Location(location); //locatie gebruiker
                    Location Coördinaat = new Location(GekozenRoute[HuidigCoördinaat].Locatie1, GekozenRoute[HuidigCoördinaat].Locatie2); //locatie volgende punt in route
                    double Afstand = Location.CalculateDistance(Gebruiker, Coördinaat, DistanceUnits.Kilometers); //afstand berekenen tussen gebruiker en volgende punt

                    if (Afstand < 0.015) //als de afstand kleiner is dan 15 meter
                    {
                        if (GekozenRoute[HuidigCoördinaat].RouteBeschrijving != null || GekozenRoute[HuidigCoördinaat].IDOEfening != null) //als een punt een routebeschrijving heeft of een oefening heeft
                        {
                            if (GekozenRoute[HuidigCoördinaat].RouteBeschrijving != null & GekozenRoute[HuidigCoördinaat].IDOEfening == null) //als een punt een routebeschrijving heeft
                            {
                                Tekst.Text = GekozenRoute[HuidigCoördinaat].RouteBeschrijving; //richtingsaanwijzing
                                await TextToSpeech.SpeakAsync(Tekst.Text);
                            }
                            if (GekozenRoute[HuidigCoördinaat].RouteBeschrijving != null & GekozenRoute[HuidigCoördinaat].IDOEfening != null) //als een punt een routebeschrijving heeft en een oefening heeft
                            {
                                if (App.Database.OefeningAanUit == true)
                                {
                                    Oefening OefeningBeschrijving = Oefeningen.Find(oefening => oefening.IDOefening == GekozenRoute[HuidigCoördinaat].IDOEfening); //ophalen van de oefening
                                    Tekst.Text = OefeningBeschrijving.OmschrijvingOefening;
                                    await TextToSpeech.SpeakAsync(Tekst.Text); //oefening
                                    await Task.Delay(5000);
                                }
                                Tekst.Text = GekozenRoute[HuidigCoördinaat].RouteBeschrijving; //richtingsaanwijzing
                                await TextToSpeech.SpeakAsync(Tekst.Text);
                            }
                            if (GekozenRoute[HuidigCoördinaat].RouteBeschrijving == null & GekozenRoute[HuidigCoördinaat].IDOEfening != null & App.Database.OefeningAanUit == true) //als een punt een oefening heeft
                            {
                                Oefening OefeningBeschrijving = Oefeningen.Find(oefening => oefening.IDOefening == GekozenRoute[HuidigCoördinaat].IDOEfening); //ophalen van de oefening
                                Tekst.Text = OefeningBeschrijving.OmschrijvingOefening;
                                await TextToSpeech.SpeakAsync(Tekst.Text); //oefening
                            }
                        }
                        if (HuidigCoördinaat < GekozenRoute.Count) //voorkomt index error
                        {
                            HuidigCoördinaat++; //voor het indexen van het volgende punt in de route
                        }
                        if (HuidigCoördinaat == GekozenRoute.Count) //route is aan het einde
                        {
                            RouteGestart = false;
                            Start_Route.Text = "Einde!";
                            Tekst.Text = String.Format("De {0} route is afgelopen, u gaat nu terug naar het hoofdmenu", route.NaamRoute); //app navigeert naar startmenu (poptorootasync)
                            await TextToSpeech.SpeakAsync(Tekst.Text);
                            await Task.Delay(4000);
                            await Navigation.PopToRootAsync();
                        }
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
    }
}