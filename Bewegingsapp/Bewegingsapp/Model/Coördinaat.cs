using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Bewegingsapp.Model
{
    public class Coördinaat
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int IDCoördinaat { get; set; }
        public double locatie1 { get; set; }
        public double locatie2 { get; set; }
        [ForeignKey(typeof(Route))]
        public int IDRoute { get; set; }
    }
}
