using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Bewegingsapp.Model
{
    [Table("Coördinaat")]
    public class Coördinaat
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int IDCoördinaat { get; set; }
        public int Nummer { get; set; }
        public double Locatie1 { get; set; } //latitude
        public double Locatie2 { get; set; } //longitude
        public string RouteBeschrijving { get; set; }
        [ForeignKey(typeof(Route))]
        public int IDRoute { get; set; }
        [ForeignKey(typeof(Oefening))]
        public int? IDOEfening { get; set; } // is nullable omdat een coördinaat niet altijd een oefening heeft
    }
}
