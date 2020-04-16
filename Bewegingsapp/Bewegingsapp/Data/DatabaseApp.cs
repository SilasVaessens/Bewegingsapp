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

        //alle methodes voor oefeningen
        public Task<List<Oefening>> LijstOefeningen()
        {
            return sqlite_database.Table<Oefening>().ToListAsync();
        }

        public Task<int> VerwijderOefening(Oefening oefening)
        {
            return sqlite_database.DeleteAsync(oefening);
        }

        public Task<int> ToevoegenOefening(Oefening oefening)
        {
            return sqlite_database.InsertAsync(oefening);
        }

        public Task<int> UpdateOefening(Oefening oefening)
        {
            return sqlite_database.UpdateAsync(oefening);
        }

        // alle methodes voor routes
        public Task<List<Route>> LijstRoutes()
        {
            return sqlite_database.Table<Route>().ToListAsync();
        }

        public Task<int> VerwijderRoute(Route route)
        {
            return sqlite_database.DeleteAsync(route);
        }

        public Task<int> ToevoegenRoute(Route route)
        {
            return sqlite_database.InsertAsync(route);
        }

        public Task<int> UpdateRoute(Route route)
        {
            return sqlite_database.UpdateAsync(route);
        }

        // alle methodes voor coördinaten
        public Task<List<Coördinaat>> LijstCoördinaten()
        {
            return sqlite_database.Table<Coördinaat>().ToListAsync();
        }

        public Task<int> VerwijderCoördinaat(Coördinaat coördinaat)
        {
            return sqlite_database.DeleteAsync(coördinaat);
        }

        public Task<int> ToevoegenCoördinaat(Coördinaat coördinaat)
        {
            return sqlite_database.InsertAsync(coördinaat);
        }

        public Task<int> UpdateCoördinaat(Coördinaat coördinaat)
        {
            return sqlite_database.UpdateAsync(coördinaat);
        }

        //deze functie is nodig voor het aanmaken van nieuwe routes. Verkrijgt het ID van laatst toegevoegde route
        public async Task<int> KrijgRouteID()
        {
            List<Route> RouteID= await App.Database.LijstRoutes();
            int ID = RouteID.Last().IDRoute;
            return ID;
        }

        //verwijdert alle coördinaten van 1 bepaalde route
        public async Task VerwijderCoördinatenRoute(int IDRoute)
        {
            List<Coördinaat> TeVerwijderenCoördinaten = new List<Coördinaat>();
            List<Coördinaat> AlleCoördinaten = await App.Database.LijstCoördinaten();
            foreach (Coördinaat coördinaat in AlleCoördinaten)
            {
                if (coördinaat.IDRoute == IDRoute)
                {
                    TeVerwijderenCoördinaten.Add(coördinaat);
                }
            }
            AlleCoördinaten.Clear();
            foreach (Coördinaat coördinaat1 in TeVerwijderenCoördinaten)
            {
                await App.Database.VerwijderCoördinaat(coördinaat1);
            }
            TeVerwijderenCoördinaten.Clear();
        }

        //update nummer alle coördinaten van 1 bepaalde route
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
    }
}
