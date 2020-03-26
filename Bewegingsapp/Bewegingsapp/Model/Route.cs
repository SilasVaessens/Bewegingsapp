using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Bewegingsapp.Model
{
    [Table("Route")]
    public class Route
    {
        [PrimaryKey, AutoIncrement]
        public int IDRoute { get; set; }
        public string NaamRoute { get; set; }
        //public List<Coördinaat> coördinaten = new List<Coördinaat>();
    }
}