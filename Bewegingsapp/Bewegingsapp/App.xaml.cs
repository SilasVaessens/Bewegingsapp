using Bewegingsapp.Data;
using System;
using System.IO;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using Bewegingsapp.Model;

namespace Bewegingsapp
{
    public partial class App : Application
    {
        static DatabaseApp databaseApp;
        public static DatabaseApp Database
        {
            get
            {
                if (databaseApp == null)
                {
                    databaseApp = new DatabaseApp(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Bewegingsapp.db"));
                }
                return databaseApp;
            }
        }

        public List<Coördinaat> CoördinatenRCS = new List<Coördinaat>();


        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected async override void OnStart()
        {
            Route routetest = new Route
            {
                IDRoute = 0,
                NaamRoute = "test"
            };

            List<Route> Routes = await App.Database.LijstRoutes();
            if (Routes[0].NaamRoute != "RCS")
            {
                await App.Database.VerwijderRoute(routetest);
                Route route = new Route
                {
                    IDRoute = 1,
                    NaamRoute = "RCS"
                };
                await App.Database.ToevoegenRoute(route);

                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 1, Locatie1 = 51.640327, Locatie2 = 5.284805 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 2, Locatie1 = 51.640284, Locatie2 = 5.286323 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 3, Locatie1 = 51.640284, Locatie2 = 5.287407 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 4, Locatie1 = 51.640284, Locatie2 = 5.288317 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 5, Locatie1 = 51.640419, Locatie2 = 5.289847 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 6, Locatie1 = 51.640155, Locatie2 = 5.289880 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 7, Locatie1 = 51.639769, Locatie2 = 5.289934 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 8, Locatie1 = 51.639519, Locatie2 = 5.289942 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 9, Locatie1 = 51.638926, Locatie2 = 5.290046 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 10, Locatie1 = 51.638672, Locatie2 = 5.288776 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 11, Locatie1 = 51.638638, Locatie2 = 5.288571 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 12, Locatie1 = 51.638600, Locatie2 = 5.288455 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 13, Locatie1 = 51.638605, Locatie2 = 5.286913 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 14, Locatie1 = 51.638817, Locatie2 = 5.286821 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 15, Locatie1 = 51.639503, Locatie2 = 5.286853 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 16, Locatie1 = 51.639626, Locatie2 = 5.286629 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 17, Locatie1 = 51.639539, Locatie2 = 5.285867 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 18, Locatie1 = 51.639240, Locatie2 = 5.284808 });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 18, Locatie1 = 51.640149, Locatie2 = 5.284610 });

                foreach(Coördinaat coördinaat in CoördinatenRCS)
                {
                    await App.Database.ToevoegenCoördinaat(coördinaat);
                    route.Coördinaten = CoördinatenRCS;
                    await App.Database.UpdateRoute(route);
                }

            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
