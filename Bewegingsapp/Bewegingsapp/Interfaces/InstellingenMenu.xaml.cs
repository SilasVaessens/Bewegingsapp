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
    public partial class InstellingenMenu : ContentPage
    {
        public InstellingenMenu()
        {
            InitializeComponent();
        }

        private async void Oefeningen_Instellingen_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LijstOefeningen());
        }

        private async void Route_Instellingen_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LijstRoutes());
        }
    }
}