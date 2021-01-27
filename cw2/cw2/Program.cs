
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.Getting the parameters
            string sourcePath = "dane.csv";
            string destinationPath = "result.xml";
            string dataFormat = "xml";
            string logPath = "log.txt";

            if (args.Length == 0)
            {
                System.Console.WriteLine("Using default parameters");
            }
            else
            {
                string args0 = args[0];
                string args1 = args[1];
                if (!Uri.IsWellFormedUriString(args0, UriKind.Absolute))
                {
                    throw new ArgumentException("Podana ścieżka: {0} jest niepoprawna", args0);
                }
                else if (!Uri.IsWellFormedUriString(args1, UriKind.Absolute))
                {
                    throw new ArgumentException("Podana ścieżka: {0} jest niepoprawna", args1);
                }
                else if (!File.Exists(sourcePath))
                {
                    throw new FileNotFoundException("Plik {0} nie istnieje", Path.GetFileName(sourcePath));
                }
                else
                {
                    sourcePath = args0;
                    destinationPath = args1;
                    dataFormat = args[2];
                }

            }

            var students = new List<Student>();
            var missingData = new List<String>();

            try
            {

                //2.Loading the data from CSV
                using (StreamReader sReader = new StreamReader(sourcePath))
                using (var csv = new CsvReader(sReader, CultureInfo.InvariantCulture))
                {
                    var record = csv.GetRecord<Student>();
                    var hasMissingData = false;
                    foreach (PropertyInfo pi in record.GetType().GetProperties())
                    {

                        if (pi.PropertyType == typeof(string))
                        {
                            string value = (string)pi.GetValue(record);
                            if (string.IsNullOrEmpty(value))
                            {
                                hasMissingData = true;

                            }
                        }
                    }

                    if (hasMissingData) missingData.Add(record.ToString());
                    else students.Add(record);


                }

                File.WriteAllLines(logPath, missingData);


                FileStream writer = new FileStream(destinationPath, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>),
                    new XmlRootAttribute("uczelnia"));
                serializer.Serialize(writer, students);
                writer.Close();


                //4.Save CSV into json
                string jsonString = JsonSerializer.Serialize(students);
                File.WriteAllText("data.json", jsonString);



            }
            catch (Exception e)
            {
                File.WriteAllText(logPath, e.Message);
            }

            Console.WriteLine(students.Count());

            Console.WriteLine("Missing data of : " + missingData.Count());
        }


    }
}
