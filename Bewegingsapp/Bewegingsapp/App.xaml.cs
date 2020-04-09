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
        public List<Oefening> OefeningenRCS = new List<Oefening>();

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected async override void OnStart()
        {
            List<Route> Routes = await App.Database.LijstRoutes();

            if (Routes.Count == 0)
            {
                Route route = new Route
                {
                    IDRoute = 1,
                    NaamRoute = "RCS"
                };
                await App.Database.ToevoegenRoute(route);

                OefeningenRCS.Add(new Oefening(){NaamOefening = "[RCS] lantaarnpaal",
                    OmschrijvingOefening = "Zoek een lantaarnpaal op de stoep, ga hier dichtbij staan en pak deze met 1 hand op heuphoogte vast. " +
                    "Pak met de andere hand de lantaarnpaal zo hoog mogelijk vast. Wissel dit met beide handen af en herhaal dit enkele keren."});
                OefeningenRCS.Add(new Oefening(){NaamOefening = "[RSC] BankjeRecht",
                    OmschrijvingOefening = "Ga met een rechte rug op het bankje zitten, kruis vervolgens uw armen voor uw borst. Reik vervolgens met uw handen naar uw tenen, herhaal dit enkele keren"});
                OefeningenRCS.Add(new Oefening(){NaamOefening = "[RSC] BankjePunt",
                    OmschrijvingOefening = "Ga op het puntje van de bank zitten, kruis vervolgens uw armen voor uw borst. Leun rustig naar achteren zonder de bank te raken, herhaal dit enkele keren"});
                OefeningenRCS.Add(new Oefening(){NaamOefening = "[RSC] Hek",
                    OmschrijvingOefening = "Zoek aan uw rechterzijde naar een hek en houdt deze stevig vast. Ga vervolgens op een denkbeeldige stoel zitten en kom daarna weer omhoog, herhaal dit enkele keren"});
                OefeningenRCS.Add(new Oefening(){NaamOefening = "[RSC] HakkenTenen",
                    OmschrijvingOefening = "Zoek een lantarenpaal of muur, hou u hieraan vast. ga op uw tenen staan en vervolgens op uw hakken, herhaal dit enkele keren"});

                foreach(Oefening Oefeningen in OefeningenRCS)
                {
                    await App.Database.ToevoegenOefening(Oefeningen);
                }

                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 1, Locatie1 = 51.640149, Locatie2 = 5.284610,
                    RouteBeschrijving = "loop vanuit de voordeur 5 meter naar de stoep en sla rechtsaf, dan na 5 meter slaat u weer Rechtsaf Rouppe van der Voortlaan in."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 2, Locatie1 = 51.640327, Locatie2 = 5.284805,
                    RouteBeschrijving = "Volg gedurende 120 meter Rouppe van der Voortlaan dan komt u aan bij oefening 1"});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 3, Locatie1 = 51.640284, Locatie2 = 5.286323, IDOEfening = 1,
                    RouteBeschrijving = "Vervolg gedurende 50 meter de stoep over Rouppe van der Voortlaan."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 4, Locatie1 = 51.640284, Locatie2 = 5.287407,
                    RouteBeschrijving = "Pas op u loopt langs een supermarkt, het kan hier erg druk zijn met verkeer, en voetgangers. Vervolg uw route gedurende 60 meter over Rouppe van de Voortlaan."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 5, Locatie1 = 51.640310, Locatie2 = 5.288325,
                    RouteBeschrijving = "U bent aangekomen bij De Rode Rik, steek hier links de straat over. Loop vervolgens rechtsaf richting Pieter Vreedesingel gedurende 100 meter tot u het einde van de stoep bereikt."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 6, Locatie1 = 51.640419, Locatie2 = 5.289847,
                    RouteBeschrijving = "Steek nu rechts de straat over Pieter Vreedesingel in, dan na 20 meter steekt u nog een straat over. Hier komt u aan bij oefening 2."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 7, Locatie1 = 51.640155, Locatie2 = 5.289880, IDOEfening = 2,
                    RouteBeschrijving = "loop vervolgens door over Pieter Vreedesingel."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 8, Locatie1 = 51.639769, Locatie2 = 5.289934, 
                    RouteBeschrijving = "Loop gedurende 10 meter de Pieter Vreedesingel af, hier steekt u nog een kruispunt over. Dan komt u aan bij de 3e oefening" });
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 9, Locatie1 = 51.639519, Locatie2 = 5.289942, IDOEfening = 3,
                    RouteBeschrijving = "Blijf Pieter Vreedesingel volgen hier komt nog een kruispunt, hier steekt u over."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 10, Locatie1 = 51.638926, Locatie2 = 5.290046,
                    RouteBeschrijving = "Sla op het einde van de stoep op Pieter Vreedesingel rechtsaf de Hertoglaan in, loop gedurende 100 meter rechtdoor over Hertoglaan, tot u aankomt bij de kruising."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 11, Locatie1 = 51.638672, Locatie2 = 5.288776,
                    RouteBeschrijving = "Steek hier de straat over bij de geleidelijnen, steek vervolgens links de straat over, ook bij de geleidelijnen en vervolg uw weg naar rechts."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 12, Locatie1 = 51.638600, Locatie2 = 5.288455,
                    RouteBeschrijving = "Volg gedurende 40 meter de Hertoglaan dan komt u aan bij de 4e oefening"});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 13, Locatie1 = 51.638565, Locatie2 = 5.287890, IDOEfening = 4,
                    RouteBeschrijving = "vervolg de stoep tot aan de geleidelijn"});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 14, Locatie1 = 51.638605, Locatie2 = 5.286913,
                    RouteBeschrijving = "Steek bij de geleidelijnen de straat over. Vervolgens steekt u rechts de straat over bij de volgende geleidelijnen. U bent aangekomen in de Brabantlaan."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 15, Locatie1 = 51.638817, Locatie2 = 5.286821,
                    RouteBeschrijving = "Loop gedurende 100 meter rechtdoor over de Brabantlaan"});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 16, Locatie1 = 51.639503, Locatie2 = 5.286853, 
                    RouteBeschrijving = "Zoek het einde van de stoep en volg deze linksaf Bisschop Zweisenplein in."}); 
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 17, Locatie1 = 51.639626, Locatie2 = 5.286629,
                    RouteBeschrijving = "Blijf de stoep volgen deze maakt een bocht naar links over Bisschop Zweisenplein na ongeveer 35 meter."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 18, Locatie1 = 51.639240, Locatie2 = 5.285867,
                    RouteBeschrijving = "Steek hier de straat over tot u de stoep vindt. Volg hier de huizenlijn aan uw linkerzijde en de speelplaats aan uw rechterzijde voor 30 meter."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 19, Locatie1 = 51.639240, Locatie2 = 5.284808, IDOEfening = 5,
                    RouteBeschrijving = "Op het einde van de stoep steekt u een smalle straat over, Loop vervolgens rechtsaf richting het einde van de route."});
                CoördinatenRCS.Add(new Coördinaat() { IDRoute = 1, Nummer = 20, Locatie1 = 51.640149, Locatie2 = 5.284610, 
                    RouteBeschrijving = "Steek hier de straat over en volg de stoep naar de voordeur. Aangekomen op uw eindbestemming"});

                foreach (Coördinaat coördinaat in CoördinatenRCS)
                {
                    await App.Database.ToevoegenCoördinaat(coördinaat);
                }
                route.Coördinaten = CoördinatenRCS;
                await App.Database.UpdateRoute(route);


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
