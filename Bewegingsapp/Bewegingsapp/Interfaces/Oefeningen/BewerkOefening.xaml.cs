using Bewegingsapp.Model;
using System;
using System.Collections.Generic;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Oefening oefening = (Oefening)BindingContext;
            List<int> CheckID = new List<int> { 1, 2, 3, 4, 5 };
            if (CheckID.Contains(oefening.IDOefening))
            {
                BewerkNaam.IsEnabled = false;
                BewerkOmschrijving.IsEnabled = false;
                Delete.IsEnabled = false;
            }
        }

        private async void Oefening_update_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(BewerkNaam.Text) & String.IsNullOrWhiteSpace(BewerkOmschrijving.Text))
            {
                await DisplayAlert("Niks ingevuld", "De oefening heeft geen naam en omschrijving meer", "OK"); //oefening heeft geen naam en omschrijving, wordt niet aangepast
            }
            else
            {
                if (String.IsNullOrWhiteSpace(BewerkNaam.Text))  //oefening heeft geen naam, wordt niet aangepast
                {
                    await DisplayAlert("Geen naam", "De oefening heeft geen naam meer", "OK");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(BewerkOmschrijving.Text)) //oefening heeft geen omschrijving, wordt niet aangepast
                    {
                        await DisplayAlert("Geen omschrijvijng", "De oefening heeft geen naam meer", "OK"); 
                    }
                    else
                    {
                        List<Oefening> oefeningen = await App.Database.LijstOefeningen();
                        Oefening oefening1 = (Oefening)BindingContext;
                        if (oefeningen.Exists(oefening => oefening.NaamOefening == BewerkNaam.Text & oefening.IDOefening != oefening1.IDOefening))
                        {
                            await DisplayAlert("Al in gebruik", "De naam die u hebt gekozen voor deze oefening wordt al gebruikt voor een andere oefening", "ok");
                        }
                        else
                        {
                            var oefening = (Oefening)BindingContext;
                            await App.Database.UpdateOefening(oefening);
                            await Navigation.PopAsync();
                        }
                    }
                }
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