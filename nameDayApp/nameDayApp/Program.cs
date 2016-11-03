/*
      Tewodros Mengesha
      teddy1496@yahoo.com
      0445487579
      11 Nov 2016
      Name Day App. The app is designed to read user input and display matching records from csv file. 

  */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;

namespace nameDayApp
{
    public class Program
    {

        /*
            What main function is coded to read file name and path from app.config. If the file name or format or 
            path is changed on the app.config the console application is capable now to read the changes without 
            hardcoding the file name or the changes. 
        
        */
       public static void Main(string[] args)
        {
            String sttr = ConfigurationManager.AppSettings["filename"];
            try
            {
                MyConfiguration conf = new MyConfiguration();
                //Console.Write(conf.getFileName());
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                throw new InvalidFormat("AppSettings is empty.");
            }

            // Calling user input method
            userInput();
       
        }

        // This function is designed to read user input from console
       public static void userInput()
        {
            string date, month, date_input;
            Console.WriteLine("Please enter date ");
            date = Console.ReadLine();
            Console.WriteLine("Please enter month ");
            month = Console.ReadLine();
            bool isValid = validateInput(date, month);
            if (isValid)
            {
                date_input = date + "." + month + ".";
                Data myData = new Data();
                MyConfiguration config = new MyConfiguration();
                string fileName = config.getFileName();
                myData.handleData(fileName,date_input);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
            else
                userInput();

        }

        // This function validates the userInput that is passed from userInput function. 
        public static bool validateInput(string date, string month)
        {
            int tempDate, tempMonth;
            bool successDate = Int32.TryParse(date, out tempDate);
            bool successMonth = Int32.TryParse(month, out tempMonth);

            //validates date values
            if (successDate)
            {

                if (tempDate < 1 || tempDate > 31)
                {
                    Console.WriteLine("Invalid input, date value should range between 1 and 30!!");
                    userInput();
                }
            }
            else
            {
                Console.WriteLine("Please enter an integer value ranging between 1 and 31!!");
                return false;
            }

            //Validate month values
            if (successMonth)
            {
                if (tempMonth < 1 || tempMonth > 12)
                {
                    Console.WriteLine("Invalid input, month value should range between 1 and 12!!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Please enter an integer value ranging between 1 and 12!!");
                userInput();
            }
            return true;
        }
    }

   /*
   
        MyConfiguration class reads file name and file path from app.config. 
        
        
    */

    public class MyConfiguration
    {
        Dictionary<string, string> _ret = new Dictionary<string, string>();

        public MyConfiguration()
        {          
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                    throw new InvalidFormat("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                       // Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                        _ret.Add(key, appSettings[key]);
                    }
                }
            }

        // getFileName is getter function for the MyConfiguration class and it returns filename and file path. 

        public string getFileName()
        {
            string value = "";
            _ret.TryGetValue("filename", out value);

            return value;
        }
    }

    /*
        Class Data, reads the data from file and displays the matching record ranging from 1-X. If no matching data is found in the record 
        it displays "No matching date found in the record" on console output window. 
    */
    class Data
    {


        //This method is used to handle data, read and display the matching values on console output window
        public void handleData(string key, string value)
        {
            try
            {
                if (!File.Exists(key))
                    throw new FileNotFoundException();


                var reader = new StreamReader(File.OpenRead(key));
                int number_of_matches = 0;
                string processor1_input = value;
                string processor1 = "";

                string processor2 = "";
                int i;
                while (!reader.EndOfStream && number_of_matches <= 1)
                {

                    var line = reader.ReadLine();

                    var values = line.Split(new char[] { ';' });

                    if ((values[0] == processor1_input))

                    {
                        if (number_of_matches == 0) processor1 = line;
                        else processor2 = line;
                        number_of_matches++;

                    }
                }
                // no matching element found 
                if (number_of_matches == 0)
                    Console.WriteLine("No matching date found in the record");
                else
                {
                    // when 1 matching element is found in the record
                    if (number_of_matches == 1)
                    {
                        Console.WriteLine(number_of_matches + " matching record found");
                        Console.WriteLine(processor1);
                    }

                    // when more than 1 matching elements found in the record
                    else
                    {
                        Console.WriteLine(number_of_matches + " matching records found");
                        for (i = 0; i < number_of_matches; i++)

                        {
                            if (i == 0) Console.WriteLine(processor1); else Console.WriteLine(processor2);
                        }

                    }

                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found!!");
            }
            

        }

    }

    //This is how you define new Esceptions
    class InvalidFormat : System.Exception
    {
        public InvalidFormat(string message) : base(message)
        {
            
        }
    }
}

