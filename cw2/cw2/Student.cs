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


        public Student()
        {
            FirstName = null;
            LastName = null;
            Studies = null;
            Mode = null;
            IndexNumber = null;
            Birthdate = null;
            Email = null;
            MotherFirstName = null;
            FatherFirstName = null;

        }
        public Student(string[] student)
        {
            FirstName = student[0];
            LastName = student[1];
            Studies = student[2];
            Mode = student[3];
            IndexNumber = student[4];
            Birthdate = DateTime.Parse(student[5]).ToString("dd.mm.yyyy");
            Email = student[6];
            MotherFirstName = student[7];
            FatherFirstName = student[8];
         
        }

        public override bool Equals(object obj)
        {

                  return obj is Student student &&
                   FirstName == student.FirstName &&
                   LastName == student.LastName &&
                   Studies == student.Studies &&
                   Mode == student.Mode &&
                   IndexNumber == student.IndexNumber &&
                   Birthdate == student.Birthdate &&
                   Email == student.Email &&
                   MotherFirstName == student.MotherFirstName &&
                   FatherFirstName == student.FatherFirstName;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(Studies);
            hash.Add(Mode);
            hash.Add(IndexNumber);
            hash.Add(Birthdate);
            hash.Add(Email);
            hash.Add(MotherFirstName);
            hash.Add(FatherFirstName);
            return hash.ToHashCode();
        }
    }
}
