using SQLite;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;


namespace Bewegingsapp.Model
{
    [Table("Oefening")]
    public class Oefening
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int IDOefening { get; set; }
        public string NaamOefening { get; set; }
        public string OmschrijvingOefening { get; set; }
        [OneToMany]
        public List<Coördinaat> CoördinatenOefening { get; set; } // wordt niks mee gedaan, kan eigenlijk weg
    }
}
