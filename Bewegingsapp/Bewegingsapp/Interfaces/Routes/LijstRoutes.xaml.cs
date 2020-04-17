using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LijstRoutes : ContentPage
    {
        public LijstRoutes()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing() //geeft lijst met routes weer
        {
            base.OnAppearing();
            Routes.ItemsSource = await App.Database.LijstRoutes();
        }

        private async void Add_Clicked(object sender, EventArgs e) //navigatie naar route toevoegen, via de add button(plus)
        {
            await Navigation.PushAsync(new RouteToevoegen());
        }

        private async void Routes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new BewerkRouteListview { BindingContext = e.SelectedItem });
        }
    }
}