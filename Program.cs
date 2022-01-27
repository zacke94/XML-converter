using System;
using System.IO; 
using System.Collections.Generic;
using System.Xml;
  
namespace XMLConverterApp {
    class XMLConverter {

        static private Dictionary<int, string[]> myDict = new Dictionary<int, string[]>();

        static void ReadTxtFile()
        {
            Console.WriteLine("Trying to read the text file 'rowbased_format.txt'...");

            const string path = @"people.txt";
            string fileName = Path.GetFileName(path);
            string[] lines = File.ReadAllLines(fileName);
            int dictIndex = 0;

            foreach(string line in lines)
            {
                string[] splittedLine = line.Split('|');
                myDict.Add(dictIndex, splittedLine);
                dictIndex++;
            }
            Console.WriteLine("File successfully read!");
        }

        static void WriteToXML()
        {
            string previousLetter = "";
            bool firstPerson = true;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(@"people.xml", settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("people");

            foreach(KeyValuePair<int, string[]> element in myDict)
            {
                if(String.Equals(element.Value[0], "P"))
                {
                    if(firstPerson == false)
                    {
                        writer.WriteEndElement();

                        if(String.Equals(previousLetter, "F"))
                            writer.WriteEndElement();
                    }

                    writer.WriteStartElement("person");

                    writer.WriteStartElement("firstname");
                    writer.WriteString(element.Value[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("lastname");
                    writer.WriteString(element.Value[2]);
                    writer.WriteEndElement();

                    previousLetter = element.Value[0];
                    firstPerson = false;
                }
                else if(String.Equals(element.Value[0], "T"))
                {
                    writer.WriteStartElement("phone");

                    writer.WriteStartElement("mobile");
                    writer.WriteString(element.Value[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("landline");
                    writer.WriteString(element.Value[2]);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                else if(String.Equals(element.Value[0], "A"))
                {
                    writer.WriteStartElement("address");

                    writer.WriteStartElement("street");
                    writer.WriteString(element.Value[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("city");
                    writer.WriteString(element.Value[2]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("zipcode");
                    writer.WriteString(element.Value[3]);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                else if(String.Equals(element.Value[0], "F"))
                {
                    if(String.Equals(previousLetter, "F"))
                    {
                        writer.WriteEndElement();
                    }
                    writer.WriteStartElement("family");

                    writer.WriteStartElement("name");
                    writer.WriteString(element.Value[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("year");
                    writer.WriteString(element.Value[2]);
                    writer.WriteEndElement();

                    previousLetter = element.Value[0];
                }  
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        static void Main(string[] args) {

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("------ Rowbased textfile converter to XML format -----");
            Console.WriteLine("--------------- By Adam Erlandsson -------------------");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();

            ReadTxtFile();
            WriteToXML();
        }
    }
}