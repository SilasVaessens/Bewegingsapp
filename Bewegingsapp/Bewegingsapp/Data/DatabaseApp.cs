using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Bewegingsapp.Model;

namespace Bewegingsapp.Data
{
    public class DatabaseApp
    {
        readonly SQLiteAsyncConnection sqlite_database;

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
    }
}
