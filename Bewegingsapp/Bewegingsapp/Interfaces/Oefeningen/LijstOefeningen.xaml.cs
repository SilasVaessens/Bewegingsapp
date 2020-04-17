using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LijstOefeningen : ContentPage
    {
        public LijstOefeningen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing() //bij openen LijstOefeningen pagina
        {
            base.OnAppearing();
            Oefeningen.ItemsSource = await App.Database.LijstOefeningen(); //geeft lijst met oefeningen weer
        }

        private async void Add_Clicked(object sender, EventArgs e) //navigatie naar oefening toevoegen, via de add button(plus)
        {
            await Navigation.PushAsync(new OefeningToevoegen());
        }

        private async void Oefeningen_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new BewerkOefening //navigatie naar oefening bewerken, via button. selecteer bestaande oefening 
            {
                BindingContext = e.SelectedItem
            });

        }
    }
}