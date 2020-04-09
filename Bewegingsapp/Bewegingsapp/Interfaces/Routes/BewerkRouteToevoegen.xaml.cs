using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

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

        private void Map_Route_Bewerken_MapClicked(object sender, MapClickedEventArgs e)
        {
        }
    }
}