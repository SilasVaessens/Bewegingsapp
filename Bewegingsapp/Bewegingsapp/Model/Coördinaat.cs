using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Bewegingsapp.Model;

namespace Bewegingsapp.Model
{
    public class Coördinaat
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int IDCoördinaat { get; set; }
        [ForeignKey(typeof(Route))]
        public int IDRoute { get; set; }
    }
}
