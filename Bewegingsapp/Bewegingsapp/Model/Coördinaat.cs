using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Bewegingsapp.Model
{
    public class Coördinaat
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int IDCoördinaat { get; set; }
        public int Nummer { get; set; }
        public double Locatie1 { get; set; }
        public double Locatie2 { get; set; }
        [ForeignKey(typeof(Route))]
        public int IDRoute { get; set; }
    }
}
