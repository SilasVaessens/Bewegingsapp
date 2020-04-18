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

        public string NaamRoute;

        public BewerkRouteListview()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var BindingRoute = (Route)BindingContext; //ophalen van geselecteerde route
            NaamRoute = BindingRoute.NaamRoute;
            NaamRouteBewerk.Text = BindingRoute.NaamRoute;
            if (BindingRoute.EindeIsBegin == false)
            {
                EindeIsBegin.IsChecked = false;
            }
            if (BindingRoute.EindeIsBegin == true)
            {
                EindeIsBegin.IsChecked = true;
            }
            List<Coördinaat> DezeRoute = await App.Database.LijstCoördinatenRoute(BindingRoute.IDRoute); //ophalen coördinaten van geselecteerde route voor listview
            ObservableCollection<Coördinaat> DezeRouteCollection = new ObservableCollection<Coördinaat>(DezeRoute as List<Coördinaat>);
            Listview_Coördinaten_Bewerk.ItemsSource = DezeRouteCollection; //listview coördinaten
            if (BindingRoute.IDRoute == 1) //route rcs kan niet verwijderd worden
            {
                Add_Punt.IsEnabled = false; //kan geen punt toevoegen
                Delete.IsEnabled = false; //kan geen punt verwijderen
                EindeIsBegin.IsEnabled = false; //kan deze optie ook niet veranderen
                NaamRouteBewerk.IsEnabled = false; //kan de naam van RCS route niet aanpassen
            }
        }

        private async void Listview_Coördinaten_Bewerk_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new BewerkRouteDetailview { BindingContext = e.SelectedItem }); //detailview van geselecteerde coördinaat
        }

        private async void Klaar_Bewerk_Clicked(object sender, System.EventArgs e)
        {
            var UpdateRoute = (Route)BindingContext;
            string OpslaanNaam = string.Format("Bent u klaar met het bewerken van de {0} route?", NaamRouteBewerk.Text);
            bool KlaarBewerken = await DisplayAlert("Route opslaan", OpslaanNaam, "JA", "NEE");
            if (KlaarBewerken == true)
            {
                List<Route> routes = await App.Database.LijstRoutes();
                if (routes.Exists(route1 => route1.NaamRoute == UpdateRoute.NaamRoute & route1.IDRoute != UpdateRoute.IDRoute))
                {
                    await DisplayAlert("Al in gebruik", "De naam die u hebt gekozen voor deze route wordt al gebruikt door een andere route.", "OK");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(NaamRouteBewerk.Text) == true)
                    {
                        await DisplayAlert("Geen naam", "De route heeft geen naam meer.", "OK");
                    }
                    else
                    {
                        UpdateRoute.NaamRoute = NaamRouteBewerk.Text;
                        if (EindeIsBegin.IsChecked == true)
                        {
                            UpdateRoute.EindeIsBegin = true;
                        }
                        if (EindeIsBegin.IsChecked == false)
                        {
                            UpdateRoute.EindeIsBegin = false;
                        }
                        await App.Database.UpdateRoute(UpdateRoute);
                        await Navigation.PopAsync(); //navigatie naar instellingen menu

                    }
                }
            }
        }

        private async void Delete_Clicked(object sender, System.EventArgs e) //verwijderen van route
        {
            string StringVerwijder = string.Format("Weet u zeker dat u de {0} route wilt verwijderen?", NaamRouteBewerk.Text);
            bool VerwijderenRoute = await DisplayAlert("Route verwijderen", StringVerwijder , "JA", "NEE");
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

        private void NaamRouteBewerk_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NaamRouteBewerk.Text) == true || NaamRouteBewerk.Text == NaamRoute)
            {
                Title = NaamRoute;
            }
            if (string.IsNullOrWhiteSpace(NaamRouteBewerk.Text) == false)
            {
                Title = NaamRouteBewerk.Text;
            }
        }
    }
}