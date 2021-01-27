using CsvHelper.Configuration.Attributes;
using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace cw2
{


    [Serializable]
    public class Student
    {

        [Index(0)]
        [JsonPropertyName("Imie")]
        [XmlElement(ElementName = "Imie")]
        public string FirstName { get; set; }

        [Index(1)]
        [JsonPropertyName("Nazwisko")]
        [XmlAttribute("Nazwisko")]
        public string LastName { get; set; }

        [Index(2)]
        public string Studies { get; set; }


        [Index(3)]
        public string Mode { get; set; }

        [Index(4)]
        public string IndexNumber { get; set; }

        [Index(5)]
        public string Birthdate { get; set; }

        [Index(5)]
        public string Email { get; set; }

        [Index(6)]
        public string MotherFirstName { get; set; }

        [Index(7)]
        public string FatherFirstName { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Studies + " " + Mode +
                " " +IndexNumber + " " +Birthdate + " " +Email + " " +MotherFirstName + " " +FatherFirstName;
        }
    }
}
