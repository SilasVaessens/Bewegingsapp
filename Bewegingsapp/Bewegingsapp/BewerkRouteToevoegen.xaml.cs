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
    public partial class BewerkRouteToevoegen : ContentPage
    {
        public BewerkRouteToevoegen()
        {
            InitializeComponent();
        }

        private void Toevoegen_Clicked(object sender, EventArgs e)
        {

        }

        private void Map_Route_Bewerken_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {

        }
    }
}