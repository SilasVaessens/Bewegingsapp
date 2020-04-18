using Bewegingsapp.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Bewegingsapp.Data
{
    public class DatabaseApp
    {
        readonly SQLiteAsyncConnection sqlite_database;
        public bool OefeningAanUit { get; set; }

        public DatabaseApp(string dbPath)
        {
            sqlite_database = new SQLiteAsyncConnection(dbPath);
            sqlite_database.CreateTableAsync<Oefening>().Wait();
            sqlite_database.CreateTableAsync<Route>().Wait();
            sqlite_database.CreateTableAsync<Coördinaat>().Wait();
        }

        // Alle methodes voor oefeningen
        // Haal alle data uit de table Oefening en zet deze data in een list
        public Task<List<Oefening>> LijstOefeningen()
        {
            return sqlite_database.Table<Oefening>().ToListAsync();
        }

        // Verwijder oefening op basis van ID
        public Task<int> VerwijderOefening(Oefening oefening)
        {
            return sqlite_database.DeleteAsync(oefening);
        }

        // Voeg oefening toe
        public Task<int> ToevoegenOefening(Oefening oefening)
        {
            return sqlite_database.InsertAsync(oefening);
        }

        // Update oefening op basis van ID
        public Task<int> UpdateOefening(Oefening oefening)
        {
            return sqlite_database.UpdateAsync(oefening);
        }

        // Alle methodes voor routes
        // Haal alle data uit de table Route en zet deze data in een list
        public Task<List<Route>> LijstRoutes()
        {
            return sqlite_database.Table<Route>().ToListAsync();
        }

        // Verwijdert route op basis van ID
        public Task<int> VerwijderRoute(Route route)
        {
            return sqlite_database.DeleteAsync(route);
        }

        // Voeg route toe
        public Task<int> ToevoegenRoute(Route route)
        {
            return sqlite_database.InsertAsync(route);
        }

        // Update route op basis van ID
        public Task<int> UpdateRoute(Route route)
        {
            return sqlite_database.UpdateAsync(route);
        }

        // Alle methodes voor coördinaten
        // Haal alle data uit de table Coördinaat en zet deze data in een list
        public Task<List<Coördinaat>> LijstCoördinaten()
        {
            return sqlite_database.Table<Coördinaat>().ToListAsync();
        }

        // Verwijder coördinaat op basis van ID
        public Task<int> VerwijderCoördinaat(Coördinaat coördinaat)
        {
            return sqlite_database.DeleteAsync(coördinaat);
        }

        // Voeg coördinaat toe
        public Task<int> ToevoegenCoördinaat(Coördinaat coördinaat)
        {
            return sqlite_database.InsertAsync(coördinaat);
        }

        // Update coördinaat op basis van ID
        public Task<int> UpdateCoördinaat(Coördinaat coördinaat)
        {
            return sqlite_database.UpdateAsync(coördinaat);
        }

        // Verkrijgt het ID van laatst toegevoegde route, nodig voor het aanmaken van coördinaten in RouteToevoegen.xaml.cs
        public async Task<int> KrijgRouteID()
        {
            List<Route> RouteID= await App.Database.LijstRoutes();
            return RouteID.Last().IDRoute;
        }

        // Verwijdert alle coördinaten van 1 bepaalde route
        public async Task VerwijderCoördinatenRoute(int IDRoute)
        {
            List<Coördinaat> TeVerwijderenCoördinaten = new List<Coördinaat>();
            List<Coördinaat> AlleCoördinaten = await App.Database.LijstCoördinaten(); // alle coördinaten
            foreach (Coördinaat coördinaat in AlleCoördinaten)
            {
                if (coördinaat.IDRoute == IDRoute) // als IDroute van coördinaat hetzelfde is als de parameter IDRoute, wordt het op een aparte lijst gezet
                {
                    TeVerwijderenCoördinaten.Add(coördinaat);
                }
            }
            AlleCoördinaten.Clear(); // weet niet of dit vereist is, maar het werkt
            foreach (Coördinaat coördinaat1 in TeVerwijderenCoördinaten) // verwijdert iedere coördinaat van de route
            {
                await App.Database.VerwijderCoördinaat(coördinaat1);
            }
            TeVerwijderenCoördinaten.Clear(); // weet niet of dit vereist is, maar het werkt
        }

        //update nummer alle coördinaten van 1 bepaalde route
        //werkt vrijwel hetzelfde als bovenstaande functie, maar dan voor het update van het Nummer attribuut van Coördinaat
        //zorgt ervoor dat na verwijdering van een coördinaat, er geen gaten ontstaan in de nummering bij het weergeven
        public async Task TeUpdatenCoördinatenRoute(int IDRoute)
        {
            List<Coördinaat> TeUpdatenCoördinaten = new List<Coördinaat>();
            List<Coördinaat> AlleCoördinaten = await App.Database.LijstCoördinaten();
            foreach (Coördinaat coördinaat in AlleCoördinaten)
            {
                if (coördinaat.IDRoute == IDRoute)
                {
                    TeUpdatenCoördinaten.Add(coördinaat);
                }
            }
            AlleCoördinaten.Clear();
            int i = 1;
            foreach (Coördinaat coördinaat1 in TeUpdatenCoördinaten)
            {
                coördinaat1.Nummer = i++;
                await App.Database.UpdateCoördinaat(coördinaat1);
            }
            TeUpdatenCoördinaten.Clear();
        }

        //krijg list coördinaten van met dezelfde IDRoute
        // geeft een lijst door die alle coördinaten bevat van een route
        // kan waarschijnlijk aangepast worden als er gebruik gemaakt gaat worden van de list Coördinaten die bij ieder object van een Route class horen
        public async Task<List<Coördinaat>>  LijstCoördinatenRoute(int IDRoute)
        {
            List<Coördinaat> CoördinatenRoute = new List<Coördinaat>();
            List<Coördinaat> AlleCoördinaten = await App.Database.LijstCoördinaten();
            foreach (Coördinaat coördinaat in AlleCoördinaten)
            {
                if (coördinaat.IDRoute == IDRoute)
                {
                    CoördinatenRoute.Add(coördinaat);
                }
            }
            return CoördinatenRoute;
        }

        //verwijder lege route bij crash / afsluiten terwijl in route toevoegen menu
        public async Task VerwijderLegeRoute()
        {
            List<Route> LastRoute = await App.Database.LijstRoutes();
            if (LastRoute.Count > 1 & string.IsNullOrWhiteSpace(LastRoute.Last().NaamRoute) == true) // Een route kan niet geen naam hebben
            {
                await App.Database.VerwijderRoute(LastRoute.Last());
            }
        }
    }
}
