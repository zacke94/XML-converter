using System;
using System.IO; 
using System.Collections.Generic;
  
namespace XMLConverterApp {
    class XMLConverter {

        static private Dictionary<int, string[]> myDict = new Dictionary<int, string[]>();
        static void ReadTxtFile()
        {
            Console.WriteLine("Trying to read the text file 'rowbased_format.txt'...");

            const string path = @"rowbased_format.txt";
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

            foreach(KeyValuePair<int, string[]> element in myDict)
            {
                if(String.Equals(element.Value[0], "P"))
                {
                    if(String.Equals(previousLetter, "F") || String.Equals(previousLetter, "P"))
                    {
                        Console.WriteLine("Error! The letter '{0}' cannot be used after the '{1}'.", element.Value[0], previousLetter);
                    }
                    else
                    {
                        Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);
                        previousLetter = "P";
                    }

                }
                else if(String.Equals(element.Value[0], "F"))
                {
                    if(String.Equals(previousLetter, "F"))
                    {
                        Console.WriteLine("Error! The letter '{0}' cannot be used after the '{1}'.", element.Value[0], previousLetter);
                    }
                    else
                    {
                        Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);
                        previousLetter = "F";
                    }
                }
                else
                {
                    if(String.Equals(previousLetter, "P") && pLetters.Contains(element.Value[0]))
                    {
                        Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);
                    }
                    else if(String.Equals(previousLetter, "F") && fLetters.Contains(element.Value[0]))
                    {
                        Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);
                    }
                    else
                    {
                        Console.WriteLine("Error! The order is incorrect.");
                        Console.WriteLine("Previous letter is: '{0}'. Current letter is: '{1}'.", previousLetter, element.Value[0]);

                        // EXIT?
                    }
                    //Console.WriteLine("Should not enter");
                }

                

                //Console.WriteLine(element.Value[0]);

                // Console.Write("{0} containts: ",ele1.Key);
                // foreach(string arr in ele1.Value)
                // {
                //     Console.Write("{0} ", arr);
                // }
                
            }
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