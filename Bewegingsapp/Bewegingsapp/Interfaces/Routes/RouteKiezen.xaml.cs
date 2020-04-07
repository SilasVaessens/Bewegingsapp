using System;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteKiezen : ContentPage
    {
        public RouteKiezen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Route_Kiezen.ItemsSource = await App.Database.LijstRoutes();
        }
        private async void Routes_ItemSelected(object sender, SelectedItemChangedEventArgs e) //route selecteren uit listview
        {
            bool answer = await DisplayAlert("Bevestiging route", "Weet u zeker dat u deze route wilt kiezen?", "ja", "nee");
            if (answer == true)
            {

                await Navigation.PushAsync(new StartRoute { BindingContext = e.SelectedItem });
            }
        }

    }
}