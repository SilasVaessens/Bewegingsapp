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
                await DisplayAlert("Niks ingevuld", "U heeft de oefening geen naam en geen omschrijving gegeven.", "OK");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(NaamEditor.Text)) //oefening heeft geen naam, wordt niet toegevoegd
                {
                    await DisplayAlert("Geen naam", "U heeft de oefening geen naam gegeven.", "OK");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(OmschrijvingEditor.Text)) //oefening heeft geen omschrijving, wordt niet toegevoegd
                    {
                        string GeenOmschrijving = string.Format("U heeft de {0} oefening geen omschrijving gegeven.", NaamEditor.Text);
                        await DisplayAlert("Geen omschrijving", GeenOmschrijving, "OK");
                    }
                    else
                    {
                        List<Oefening> oefeningen = await App.Database.LijstOefeningen();
                        if (oefeningen.Exists(oefening => oefening.NaamOefening == NaamEditor.Text)) //naam van oefening is al in gebruik
                        {
                            await DisplayAlert("Al in gebruik", "De naam die u hebt gekozen voor deze oefening wordt al gebruikt door een andere oefening.", "OK");
                        }
                        else //oefening toevoegen, velden zijn ingevuld en de naam is beschikbaar
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
        }

        private void NaamEditor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NaamEditor.Text) == true)
            {
                Title = "Nieuwe oefening toevoegen";
            }
            if (string.IsNullOrWhiteSpace(NaamEditor.Text) == false)
            {
                Title = NaamEditor.Text;
            }
        }
    }
}