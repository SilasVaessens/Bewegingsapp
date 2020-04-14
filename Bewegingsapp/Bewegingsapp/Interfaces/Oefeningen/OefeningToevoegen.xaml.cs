using Bewegingsapp.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;


namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OefeningToevoegen : ContentPage
    {
        public OefeningToevoegen()
        {
            InitializeComponent();
        }

        private async void Oefening_opslaan_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NaamEditor.Text) & String.IsNullOrWhiteSpace(OmschrijvingEditor.Text)) //oefening heeft geen naam en omschrijving, wordt niet toegevoegd
            {
                await DisplayAlert("Niks ingevuld", "U heeft de oefening geen naam en geen omschrijving gegeven", "OK");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(NaamEditor.Text)) //oefening heeft geen naam, wordt niet toegevoegd
                {
                    await DisplayAlert("Geen naam", "U heeft de oefening geen naam gegeven", "OK");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(OmschrijvingEditor.Text)) //oefening heeft geen omschrijving, wordt niet toegevoegd
                    {
                        await DisplayAlert("Geen omschrijvijng", "U heeft de oefening geen omschrijving gegeven", "OK");
                    }
                    else
                    {
                        List<Oefening> oefeningen = await App.Database.LijstOefeningen();
                        foreach (Oefening oefening in oefeningen)
                        {
                            if (oefening.NaamOefening == NaamEditor.Text)
                            {
                                await DisplayAlert("Al in gebruik", "De naam die u hebt gekozen voor deze oefening wordt al gebruikt voor een andere oefening", "ok");
                                break;
                            }
                        }
                    }

                }
            }
            if (String.IsNullOrWhiteSpace(NaamEditor.Text) == false & String.IsNullOrWhiteSpace(OmschrijvingEditor.Text) == false) //oefening met naam en omschrijving wordt toegevoegd
            {
                Oefening oefening = new Oefening()
                {
                    NaamOefening = NaamEditor.Text,
                    OmschrijvingOefening = OmschrijvingEditor.Text
                };
                await App.Database.ToevoegenOefening(oefening);
                await Navigation.PopAsync();

            }
        }
    }
}