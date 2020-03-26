using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bewegingsapp.Model;

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
    }
}