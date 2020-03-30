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
        public RouteToevoegen()
        {
            InitializeComponent();
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

        private void Map_Route_Toevoegen_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
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

            Map_Route_Toevoegen.Pins.Add(pin);
            pin.MarkerClicked += (s, args) =>
            {
                args.HideInfoWindow = true;
            };
        }

        
    }
}