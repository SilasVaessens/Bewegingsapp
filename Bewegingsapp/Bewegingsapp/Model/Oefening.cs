using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Bewegingsapp.Model
{
    [Table("Oefening")]
    public class Oefening
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int IDOefening { get; set; }
        public string NaamOefening { get; set; }
        public string OmschrijvingOefening { get; set; }
    }
}
