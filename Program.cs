using System;
using System.IO; 
using System.Collections.Generic;
using System.Xml;
  
namespace XMLConverterApp {
    class XMLConverter {

        static private List<string[]> myList = new List<string[]>();

        static void ReadTxtFile()
        {
            Console.WriteLine("Reading the text file 'people.txt'...");

            const string path = @"people.txt";
            string fileName = Path.GetFileName(path);
            string[] lines = File.ReadAllLines(fileName);

            foreach(string line in lines)
            {
                string[] splittedLine = line.Split('|');
                myList.Add(splittedLine);
            }
            Console.WriteLine("File successfully read!");
        }

        static void WriteToXML()
        {
            Console.WriteLine("Converting to XML format...");

            string previousLetter = "";
            bool firstPerson = true;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(@"people.xml", settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("people");

            foreach(string[] element in myList)
            {
                if(String.Equals(element[0], "P"))
                {
                    if(firstPerson == false)
                    {
                        writer.WriteEndElement();

                        if(String.Equals(previousLetter, "F"))
                            writer.WriteEndElement();
                    }

                    writer.WriteStartElement("person");

                    writer.WriteStartElement("firstname");
                    writer.WriteString(element[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("lastname");
                    writer.WriteString(element[2]);
                    writer.WriteEndElement();

                    previousLetter = element[0];
                    firstPerson = false;
                }
                else if(String.Equals(element[0], "T"))
                {
                    writer.WriteStartElement("phone");

                    writer.WriteStartElement("mobile");
                    writer.WriteString(element[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("landline");
                    writer.WriteString(element[2]);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                else if(String.Equals(element[0], "A"))
                {
                    writer.WriteStartElement("address");

                    writer.WriteStartElement("street");
                    writer.WriteString(element[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("city");
                    writer.WriteString(element[2]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("zipcode");
                    writer.WriteString(element[3]);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                else if(String.Equals(element[0], "F"))
                {
                    if(String.Equals(previousLetter, "F"))
                    {
                        writer.WriteEndElement();
                    }
                    writer.WriteStartElement("family");

                    writer.WriteStartElement("name");
                    writer.WriteString(element[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("year");
                    writer.WriteString(element[2]);
                    writer.WriteEndElement();

                    previousLetter = element[0];
                }  
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            Console.WriteLine("Successfully converted and generated a XML file, 'people.xml'!");
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