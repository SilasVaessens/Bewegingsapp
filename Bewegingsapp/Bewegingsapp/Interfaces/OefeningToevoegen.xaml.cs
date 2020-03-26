using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            if (String.IsNullOrEmpty(NaamEditor.Text) & String.IsNullOrEmpty(OmschrijvingEditor.Text))
            {
                await DisplayAlert("Niks ingevuld", "U heeft de oefening geen naam en geen omschrijving gegeven", "OK");
            }
            else
            {
                if (String.IsNullOrEmpty(NaamEditor.Text))
                {
                    await DisplayAlert("Geen naam", "U heeft de oefening geen naam gegeven", "OK");
                }
                else
                {
                    if (String.IsNullOrEmpty(OmschrijvingEditor.Text))
                    {
                        await DisplayAlert("Geen omschrijvijng", "U heeft de oefening geen omschrijving gegeven", "OK");
                    }
                }
            }
            if (String.IsNullOrEmpty(NaamEditor.Text) == false & String.IsNullOrEmpty(OmschrijvingEditor.Text) == false)
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