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
            Oefening oefening = (Oefening)BindingContext; //geselecteerde oefening
            List<int> CheckID = new List<int> { 1, 2, 3, 4, 5 }; //controle of het een RCS oefening is
            if (CheckID.Contains(oefening.IDOefening)) //indien het een RCS oefening is, niet verwijderen of bewerken
            {
                BewerkNaam.IsEnabled = false;
                BewerkOmschrijving.IsEnabled = false;
                Delete.IsEnabled = false;
            }
        }

        private async void Oefening_update_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(BewerkNaam.Text) & String.IsNullOrWhiteSpace(BewerkOmschrijving.Text)) //oefening heeft geen naam en omschrijving, wordt niet aangepast
            {
                await DisplayAlert("Niks ingevuld", "De oefening heeft geen naam en omschrijving meer", "OK"); 
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
                    else //oefening heeft naam en omschrijving
                    {
                        List<Oefening> oefeningen = await App.Database.LijstOefeningen();
                        Oefening oefening1 = (Oefening)BindingContext;
                        if (oefeningen.Exists(oefening => oefening.NaamOefening == BewerkNaam.Text & oefening.IDOefening != oefening1.IDOefening)) //naam van oefening is al in gebruik
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

        private async void Delete_Clicked(object sender, EventArgs e) //oefening verijderen
        {
            bool answer = await DisplayAlert("Definitief verwijderen?", "Weet u zeker dat u deze oefening wilt verwijderen?", "ja", "nee");
            if (answer == true)
            {
                var oefening = (Oefening)BindingContext;
                await App.Database.VerwijderOefening(oefening);
                await Navigation.PopAsync();
            }
        }
    }
}