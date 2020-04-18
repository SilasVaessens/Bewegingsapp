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
        int IDRoute;
        List<Route> AlleRoutes; 

        public RouteToevoegenListview()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AlleRoutes = await App.Database.LijstRoutes(); //lijst van alle routes
            Title = AlleRoutes.Last().NaamRoute; //selecteerd route die net gemaakt is(laatste route)
            IDRoute = await App.Database.KrijgRouteID();
            Listview_Coördinaten.ItemsSource = await App.Database.LijstCoördinatenRoute(IDRoute); //listview coördinaten laatste route
        }

        private async void Listview_Coördinaten_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new RouteToevoegenDetailpage { BindingContext = e.SelectedItem}); //detailview van geselecteerde coördinaat
        }

        private async void Klaar_Clicked(object sender, EventArgs e) //navigatie naar instellingen menu
        {
            string OpslaanRoute = string.Format("Bent u klaar met het bewerken van de {0} route?", AlleRoutes.Last().NaamRoute);
            bool KlaarBewerken = await DisplayAlert("Route opslaan", OpslaanRoute , "JA", "NEE"); // om per ongeluk opslaan tegen te gaan
            if (KlaarBewerken == true)
            {
                if (EindeIsBegin.IsChecked == true) // er wordt niks opgeslagen als er deze niet gecheckt is, staat namelijk al standaard op false
                {
                    Route route = AlleRoutes.Find(route1 => route1.IDRoute == IDRoute); // heeft geen bindingcontext, dus het juiste Route object wordt om deze manier gevonden
                    route.EindeIsBegin = true;
                    await App.Database.UpdateRoute(route);
                }
                var VorigePage = Navigation.NavigationStack.LastOrDefault(); // verwijdert RouteToevoegen interface van de Navigationstack
                Navigation.RemovePage(VorigePage);
                await Navigation.PopAsync();
            }
        }
    }
}