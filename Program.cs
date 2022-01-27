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
            var pLetters = new List<string> {"T", "A", "F"};
            var fLetters = new List<string> {"T", "A"};
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
                    // if(String.Equals(previousLetter, "F") || String.Equals(previousLetter, "P"))
                    // {
                    //     Console.WriteLine("Error! The letter '{0}' cannot be used after the '{1}'.", element.Value[0], previousLetter);
                    // }
                    // else
                    // {
                    if(firstPerson == false)
                    {
                        writer.WriteEndElement();
                    }

                    writer.WriteStartElement("person");

                    writer.WriteStartElement("firstname");
                    writer.WriteString(element.Value[1]);
                    writer.WriteEndElement();

                    writer.WriteStartElement("lastname");
                    writer.WriteString(element.Value[2]);
                    writer.WriteEndElement();

                    //Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);
                    previousLetter = element.Value[0];
                    firstPerson = false;
                    //}

                }
                else if(String.Equals(element.Value[0], "T"))
                {
                    if(String.Equals(previousLetter, "F"))
                    {
                        writer.WriteEndElement();
                    }
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
                    // if(String.Equals(previousLetter, "F"))
                    // {
                    //     writer.WriteEndElement();
                    // }
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
                // else
                // {
                //     if(String.Equals(previousLetter, "P") && pLetters.Contains(element.Value[0]))
                //     {
                //         Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);
                //     }
                //     else if(String.Equals(previousLetter, "F") && fLetters.Contains(element.Value[0]))
                //     {
                //         Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);
                //     }
                //     if(checkCondition(previousLetter, element.Value[0]))
                //     {
                //         Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);

                //         if(String.Equals(element.Value[0], "T"))
                //         {
                //             writer.WriteStartElement("phone");

                //             writer.WriteStartElement("mobile");
                //             writer.WriteString(element.Value[1]);
                //             writer.WriteEndElement();

                //             writer.WriteStartElement("landline");
                //             writer.WriteString(element.Value[2]);
                //             writer.WriteEndElement();

                //             writer.WriteEndElement();
                //         }
                //         else if(String.Equals(element.Value[0], "A"))
                //         {
                //             writer.WriteStartElement("address");

                //             writer.WriteStartElement("street");
                //             writer.WriteString(element.Value[1]);
                //             writer.WriteEndElement();

                //             writer.WriteStartElement("city");
                //             writer.WriteString(element.Value[2]);
                //             writer.WriteEndElement();

                //             writer.WriteStartElement("zipcode");
                //             writer.WriteString(element.Value[3]);
                //             writer.WriteEndElement();

                //             writer.WriteEndElement();
                //         }
                //     }
                //     else
                //     {
                //         Console.WriteLine("Error! The order is incorrect.");
                //         Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);

                //         // EXIT?
                //     }
                //     Console.WriteLine("Should not enter");
                // }

                

                //Console.WriteLine(element.Value[0]);

                // Console.Write("{0} containts: ",ele1.Key);
                // foreach(string arr in ele1.Value)
                // {
                //     Console.Write("{0} ", arr);
                // }
                
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