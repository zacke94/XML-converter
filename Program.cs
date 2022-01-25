using System;
using System.IO; 
using System.Collections.Generic;
  
namespace XMLConverterApp {
    class XMLConverter {

        static void ReadTxtFile()
        {
            Console.WriteLine("Trying to read the text file 'rowbased_format.txt'...");

            const string path = @"rowbased_format.txt";
            string fileName = Path.GetFileName(path);
        
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                Dictionary<int, string[]> myDict = new Dictionary<int, string[]>();
                int dictIndex = 0;

                foreach(string line in lines)
                {
                    string[] splittedLine = line.Split('|');
                    myDict.Add(dictIndex, splittedLine);
                    dictIndex++;
                }

                /*foreach(KeyValuePair<int, string[]> ele1 in myDict)
                {
                    Console.Write("{0} containts: ",ele1.Key);
                    foreach(string arr in ele1.Value)
                    {
                        Console.Write("{0} ", arr);
                    }
                    Console.WriteLine();
                }*/
                Console.WriteLine("File successfully read!");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                System.Environment.Exit(1);
            }
            //Console.WriteLine("PATH:     {0}", path);
            //Console.WriteLine("FILENAME: {0}", filename);

        }
        static void Main(string[] args) {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("------ Rowbased textfile converter to XML format -----");
            Console.WriteLine("--------------- By Adam Erlandsson -------------------");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();

            ReadTxtFile();
        
        }
    }
}