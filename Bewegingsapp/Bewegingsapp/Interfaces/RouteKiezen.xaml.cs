using System;
using Bewegingsapp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;


namespace Bewegingsapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteKiezen : ContentPage
    {
        public RouteKiezen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Route_Kiezen.ItemsSource = await App.Database.LijstRoutes();
            await TextToSpeech.SpeakAsync(Label_Route.Text); // leest "Kies route" voor de gebruiker
            foreach (Route route in Route_Kiezen.ItemsSource)
            {
                await TextToSpeech.SpeakAsync(route.NaamRoute); // leest iedere route voor voor de gebruiker
                await Task.Delay(200);
            }
            
        }
        private async void Routes_ItemSelected(object sender, SelectedItemChangedEventArgs e) //route selecteren uit listview
        {
            // verandert het interface naar iets wat makkelijker te gebruiken is voor slechtzienden
            // hiervoor stond er een DisplayAlert, maar voor slechtzienden kan dit lastig zijn
            if (Route_Kiezen.SelectedItem != null)
            {
                Ja_Route.IsEnabled = true; // deze knop was eerst niet te gebruiken
                Ja_Route.IsVisible = true;
                Nee_Route.IsEnabled = true; // deze knop was eerst niet te gebruiken
                Nee_Route.IsVisible = true;
                Tussen_Knoppen.IsEnabled = true; // voor een mooi zwart lijntje tussen de knoppen
                Tussen_Knoppen.IsVisible = true;
                Route_Kiezen.IsEnabled = false; // disabled de listview, kan niet per ongeluk een andere route kiezen op de listview
                Route route = (Route)e.SelectedItem;
                Grid_Kiezen.RowDefinitions[0].Height = new GridLength(50, GridUnitType.Star); // verandert hoogte van gridrow
                Grid_Kiezen.RowDefinitions[2].Height = new GridLength(50, GridUnitType.Star); // verandert hoogte van gridrow
                Label_Route.Text = String.Format("Weet u zeker dat u de {0} route wilt gaan lopen?", route.NaamRoute);
                await TextToSpeech.SpeakAsync(Label_Route.Text); // wordt automatisch voorgelezen
                await TextToSpeech.SpeakAsync(Nee_Route.Text); // wordt automatisch voorgelezen
                await TextToSpeech.SpeakAsync(Ja_Route.Text); // wordt automatisch voorgelezen
                Ja_Route.Clicked += async (s, args) =>
                {
                    await Navigation.PushAsync(new StartRoute { BindingContext = e.SelectedItem }); //navigatie naar startroute met geselecteerde route
                };
                Nee_Route.Clicked += async (s, args) => // verandert als wat verandert met het selecteren van een item weet terug naar normaal en deselecteer het item
                {
                    Route_Kiezen.IsEnabled = true;
                    Route_Kiezen.SelectedItem = null;
                    Ja_Route.IsEnabled = false;
                    Ja_Route.IsVisible = false;
                    Nee_Route.IsVisible = false;
                    Nee_Route.IsVisible = false;
                    Tussen_Knoppen.IsEnabled = false;
                    Tussen_Knoppen.IsVisible = false;
                    Grid_Kiezen.RowDefinitions[0].Height = new GridLength(15, GridUnitType.Star);
                    Grid_Kiezen.RowDefinitions[2].Height = new GridLength(85, GridUnitType.Star);
                    Label_Route.Text = "Kies route";
                    await TextToSpeech.SpeakAsync(Label_Route.Text);
                    foreach (Route route1 in Route_Kiezen.ItemsSource)
                    {
                        await TextToSpeech.SpeakAsync(route1.NaamRoute);
                        await Task.Delay(200);
                    }

                };
            }
        }
    }
}