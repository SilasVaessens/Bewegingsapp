using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Bewegingsapp.Model
{
    [Table("Route")]
    public class Route
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int IDRoute { get; set; }
        [Column("Naam")]
        public string NaamRoute { get; set; }
        [OneToMany]
        public List<Coördinaat> Coördinaten { get; set; }
    }
}