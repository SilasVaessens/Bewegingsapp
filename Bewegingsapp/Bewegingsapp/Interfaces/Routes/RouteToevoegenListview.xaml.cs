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

        public RouteToevoegenListview()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Route> AlleRoutes = await App.Database.LijstRoutes();
            Title = AlleRoutes.Last().NaamRoute;
            int IDRoute = await App.Database.KrijgRouteID();
            Listview_Coördinaten.ItemsSource = await App.Database.LijstCoördinatenRoute(IDRoute);
        }

        private async void Listview_Coördinaten_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new RouteToevoegenDetailpage { BindingContext = e.SelectedItem});
        }

        private async void Klaar_Clicked(object sender, EventArgs e)
        {
            bool KlaarBewerken = await DisplayAlert("Route opslaan", "Bent u klaar met het bewerken van de route?", "ja", "nee");
            if (KlaarBewerken == true)
            {
                var VorigePage = Navigation.NavigationStack.LastOrDefault();
                Navigation.RemovePage(VorigePage);
                await Navigation.PopAsync();
            }
        }
    }
}