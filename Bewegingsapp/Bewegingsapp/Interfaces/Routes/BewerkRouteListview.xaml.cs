using System.Collections.Generic;
using System.Threading.Tasks;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BewerkRouteListview : ContentPage
    {
        public BewerkRouteListview()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var BindingRoute = (Route)BindingContext; //ophalen van geselecteerde route
            Title = BindingRoute.NaamRoute;
            List<Coördinaat> DezeRoute = await App.Database.LijstCoördinatenRoute(BindingRoute.IDRoute); //ophalen coördinaten van geselecteerde route voor listview
            ObservableCollection<Coördinaat> DezeRouteCollection = new ObservableCollection<Coördinaat>(DezeRoute as List<Coördinaat>);
            Listview_Coördinaten_Bewerk.ItemsSource = DezeRouteCollection; //listview coördinaten
            if (BindingRoute.IDRoute == 1) //route rcs kan niet verwijderd worden
            {
                Add_Punt.IsEnabled = false; //kan geen punt toevoegen
                Delete.IsEnabled = false; //kan geen punt verwijderen
            }
        }

        private async void Listview_Coördinaten_Bewerk_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new BewerkRouteDetailview { BindingContext = e.SelectedItem }); //detailview van geselecteerde coördinaat
        }

        private async void Klaar_Bewerk_Clicked(object sender, System.EventArgs e)
        {
            bool KlaarBewerken = await DisplayAlert("Route opslaan", "Bent u klaar met het bewerken van de route?", "ja", "nee");
            if (KlaarBewerken == true)
            {
                await Navigation.PopAsync(); //navigatie naar instellingen menu
            }
        }

        private async void Delete_Clicked(object sender, System.EventArgs e) //verwijderen van route
        {
            bool VerwijderenRoute = await DisplayAlert("Route verwijderen", "Weet u zeker dat u deze route wilt verwijderen?", "ja", "nee");
            if (VerwijderenRoute == true)
            {
                var VerwijderRoute = (Route)BindingContext;
                await App.Database.VerwijderCoördinatenRoute(VerwijderRoute.IDRoute);
                await App.Database.VerwijderRoute(VerwijderRoute);
                await Navigation.PopAsync();
            }
        }

        private async void Add_Punt_Clicked(object sender, System.EventArgs e) //punt toevoegen aan route
        {
            var Add = (Route)BindingContext;
            var bewerkRouteToevoegen = new BewerkRouteToevoegen
            {
                BindingContext = Add
            };
            await Navigation.PushAsync(bewerkRouteToevoegen);
        }
    }
}