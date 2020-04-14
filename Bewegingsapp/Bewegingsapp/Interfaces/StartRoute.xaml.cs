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
            GekozenRoute = await App.Database.LijstCoördinatenRoute(route.IDRoute);
            foreach (Coördinaat coördinaat1 in GekozenRoute)
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
            Map_Start_Route.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(locatie1, locatie2), Distance.FromMeters(50))); //startpunt, locatie eerste coördinaat gekozen route
          

        }

        private void Map_Start_Route_MapClicked(object sender, MapClickedEventArgs e)
        {

        }
    }
}