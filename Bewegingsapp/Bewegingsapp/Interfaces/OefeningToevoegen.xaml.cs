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
            Oefening oefening = new Oefening()
            {
                Naam = NaamEditor.Text,
                Omschrijving = OmschrijvingEditor.Text
            };
            await App.Database.ToevoegenOefening(oefening);
            await Navigation.PopAsync();
        }
    }
}