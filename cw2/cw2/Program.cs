
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
                if(!Uri.IsWellFormedUriString(args0, UriKind.Absolute)){
                    throw new ArgumentException("Podana ścieżka: {0} jest niepoprawna", args0);
                }else if(!Uri.IsWellFormedUriString(args1, UriKind.Absolute))
                {
                    throw new ArgumentException("Podana ścieżka: {0} jest niepoprawna", args1);
                }else if (!File.Exists(sourcePath))
                {
                    throw new FileNotFoundException("Plik {0} nie istnieje",Path.GetFileName(sourcePath));
                }
                else
                {
                    sourcePath = args0;
                    destinationPath = args1;
                    dataFormat = args[2];
                }

            }


            //2.Loading the data from CSV
            using (StreamReader sReader = new StreamReader(sourcePath))
            using (StreamWriter sWriter = new StreamWriter(logPath)) 

            {
                var studentsHash = new HashSet<Student>();
                var missingData = new List<String>();
                string line = null;
                while ((line = sReader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                  

                    if (data.Length != 9)
                    {
                        missingData.Add(String.Join(',', data));
                        throw new Exception("Wiersz student zawiera błędną liczbę kolumn");

                    }
                    else {
                        studentsHash.Add( new Student(data));
                        //nie usuwa duplikatów, poprawić
                    }

                  

                }
                File.WriteAllLines(logPath, missingData);

                var students = new List<Student>(studentsHash);
                //3.Save CSV into XML
                FileStream writer = new FileStream(destinationPath, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>),
                    new XmlRootAttribute("uczelnia"));
                serializer.Serialize(writer, students);
                writer.Close();


                //4.Save CSV into json
                string jsonString = JsonSerializer.Serialize(students);
                File.WriteAllText("data.json", jsonString);

            }


        }

       
    }
}
