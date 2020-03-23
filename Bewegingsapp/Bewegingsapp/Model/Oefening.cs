using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Bewegingsapp.Model
{
    [Table("Oefening")]
    public class Oefening
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
    }
}
