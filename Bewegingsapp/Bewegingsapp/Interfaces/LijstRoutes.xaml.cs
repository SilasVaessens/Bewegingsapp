using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Routes.ItemsSource = await App.Database.LijstRoutes();
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RouteToevoegen());
        }

        private void Routes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}