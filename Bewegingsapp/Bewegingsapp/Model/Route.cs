using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Bewegingsapp.Model
{
    [Table("Route")]
    public class Route
    {
        [PrimaryKey, Column("ID")]
        public int IDRoute { get; set; }
        [Column("Naam")]
        public string NaamRoute { get; set; }
        [OneToMany]
        public List<Coördinaat> Coördinaten { get; set; }
    }
}