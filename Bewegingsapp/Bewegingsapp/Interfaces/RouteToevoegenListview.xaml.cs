using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteToevoegenListview : ContentPage
    {
        public List<Coördinaat> CoördinatenAlleRoutes = new List<Coördinaat>();
        public List<Coördinaat> CoördinatenNieweRoute = new List<Coördinaat>();

        public RouteToevoegenListview()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int IDRoute = await App.Database.KrijgRouteID();
            CoördinatenAlleRoutes = await App.Database.LijstCoördinaten();
            foreach (Coördinaat coördinaat in CoördinatenAlleRoutes)
            {
                if (coördinaat.IDRoute == IDRoute)
                {
                    CoördinatenNieweRoute.Add(coördinaat);
                }
            }
            Listview_Coördinaten.ItemsSource = CoördinatenNieweRoute;
        }

    }
}