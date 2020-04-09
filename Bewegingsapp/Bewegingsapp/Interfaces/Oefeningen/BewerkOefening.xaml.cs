using Bewegingsapp.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BewerkOefening : ContentPage
    {
        public BewerkOefening()
        {
            InitializeComponent();
        }

        private async void Oefening_update_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(BewerkNaam.Text) & String.IsNullOrEmpty(BewerkOmschrijving.Text))
            {
                await DisplayAlert("Niks ingevuld", "De oefening heeft geen naam en omschrijving meer", "OK"); //oefening heeft geen naam en omschrijving, wordt niet aangepast
            }
            else
            {
                if (String.IsNullOrEmpty(BewerkNaam.Text))  //oefening heeft geen naam, wordt niet aangepast
                {
                    await DisplayAlert("Geen naam", "De oefening heeft geen naam meer", "OK");
                }
                else
                {
                    if (String.IsNullOrEmpty(BewerkOmschrijving.Text)) //oefening heeft geen omschrijving, wordt niet aangepast
                    {
                        await DisplayAlert("Geen omschrijvijng", "De oefening heeft geen naam meer", "OK"); 
                    }
                }
            }
            if (String.IsNullOrEmpty(BewerkNaam.Text) == false & String.IsNullOrEmpty(BewerkOmschrijving.Text) == false) //oefening met naam en omschrijving wordt aangepast
            {
                var oefening = (Oefening)BindingContext;
                await App.Database.UpdateOefening(oefening);
                await Navigation.PopAsync();
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Definitief verwijderen?", "Weet u zeker dat u deze oefening wilt verwijderen?", "ja", "nee"); //oefening verijderen
            if (answer == true)
            {
                var oefening = (Oefening)BindingContext;
                await App.Database.VerwijderOefening(oefening);
                await Navigation.PopAsync();
            }
        }
    }
}