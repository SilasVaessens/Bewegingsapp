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
            //sqlite_database.CreateTableAsync<Routes>().Wait();
            //sqlite_database.CreateTableAsync<Coördinaten>().Wait();
        }

        public Task<List<Oefening>> LijstOefeningen()
        {
            return sqlite_database.Table<Oefening>().ToListAsync();
        }

        public Task<Oefening> DetailOefening(int id)
        {
            return sqlite_database.Table<Oefening>()
                                  .Where(i => i.ID == id)
                                  .FirstOrDefaultAsync();
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
    }
}
