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

        public string NaamOefening; // nodig voor het veranderen van de titel

        public BewerkOefening()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Oefening oefening = (Oefening)BindingContext; //geselecteerde oefening
            NaamOefening = oefening.NaamOefening; // eigenlijke naam oefening
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
                await DisplayAlert("Niks ingevuld", "De oefening heeft geen naam en omschrijving meer.", "OK"); 
            }
            else
            {
                if (String.IsNullOrWhiteSpace(BewerkNaam.Text))  //oefening heeft geen naam, wordt niet aangepast
                {
                    await DisplayAlert("Geen naam", "De oefening heeft geen naam meer.", "OK");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(BewerkOmschrijving.Text)) //oefening heeft geen omschrijving, wordt niet aangepast
                    {
                        string GeenOmschrijving = string.Format("De {0} oefening heeft geen omschrijving meer.", BewerkNaam.Text);
                        await DisplayAlert("Geen omschrijving",GeenOmschrijving , "OK"); 
                    }
                    else 
                    {
                        List<Oefening> oefeningen = await App.Database.LijstOefeningen();
                        Oefening oefening1 = (Oefening)BindingContext; //nodig voor het verkrijgen van het ID en de naam
                        //Kijkt of er al een oefening bestaat met dezelfde naam en of het ID van die oefening niet hetzelfde is als dat van de oefening die bewerkt wordt
                        if (oefeningen.Exists(oefening => oefening.NaamOefening == BewerkNaam.Text & oefening.IDOefening != oefening1.IDOefening))
                        {
                            await DisplayAlert("Al in gebruik", "De naam die u hebt gekozen voor deze oefening wordt al gebruikt door een andere oefening.", "OK");
                        }
                        else //oefening heeft naam, omschrijving en geen al gebruikte naam
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
            string VerwijderString = string.Format("Weet u zeker dat u de {0} oefening wilt verwijderen?", BewerkNaam.Text);
            bool answer = await DisplayAlert("Definitief verwijderen?", VerwijderString, "JA", "NEE"); // om te voorkomen dat dingen per ongeluk verwijdert worden
            if (answer == true)
            {
                var oefening = (Oefening)BindingContext;
                await App.Database.VerwijderOefening(oefening);
                await Navigation.PopAsync();
            }
        }

        // verandert de title van de page als de editor BewerkNaam.Text verandert 
        private void BewerkNaam_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Title is de eigenlijke naam van de oefening (die van de BindingContext) als de editor leeg is of gelijk is aan de eigenlijke naam
            if (string.IsNullOrWhiteSpace(BewerkNaam.Text) == true || BewerkNaam.Text == NaamOefening)
            {
                Title = NaamOefening;
            }
            // Als de editor niet leeg is, dan is de title gelijk aan de text in de editor BewerkNaam
            if (string.IsNullOrWhiteSpace(BewerkNaam.Text) == false)
            {
                Title = BewerkNaam.Text;
            }
        }
    }
}