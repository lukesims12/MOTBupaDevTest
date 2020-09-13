using System;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConsoleTables;

namespace BupaDentalTestMOT
{
    class Program
    {
        static void Main(string[] args)
        {
            // ### DISPLAY WELCOME MESSAGES ### //
            Console.WriteLine("Hello, Welcome to the DVLA's MOT HISTORY Database.");
            Console.WriteLine("Please Enter a Vehicle Registration Number below to get started.");

            GetUserInput();

            // ### RUN UNTIL USER QUITS APPLICATION ### //
            while (RerunApplication())
            {
                Console.WriteLine("Please Enter a Vehicle Registration Number below:");
                GetUserInput();
            }
        }

        public static void GetUserInput()
        {
            string reg = Console.ReadLine();
            string formattedReg = string.Concat(reg.Where(c => !char.IsWhiteSpace(c)));
            Request(formattedReg.Trim());
        }

        private static void Request(string vehicleReg)
        {
            try 
            { 
                WebRequest webRequest = WebRequest.Create("https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=" + vehicleReg);
                webRequest.Method = "GET";
                webRequest.Headers.Add("x-api-key", "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno");
                webRequest.Headers.Add("Accept", "application/json");
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    string data = JArray.Parse(responseFromServer).ToString();
                    JObject jObject = JObject.Parse(data.Substring(1, data.Length - 2));
                    // ### CHECK IF MOT DATA EXISTS FOR NEWER CARS ### //
                    bool hasMOTData = jObject.ContainsKey("motTests");
                    DisplayVehicleData(data, hasMOTData);
                }
            }
            catch (WebException)
            {
                Console.WriteLine("Sorry, the vehicle was not found.");
                Console.WriteLine("Please Try Again.");
                GetUserInput();
            }

        }

        public static void DisplayVehicleData(string resVehicleData, bool hasMOTData)
        {
            Vehicle vehicle = JsonConvert.DeserializeObject<Vehicle>(resVehicleData.Substring(1, resVehicleData.Length - 2));
            if (hasMOTData)
            {
                var table = new ConsoleTable("Make", "Model", "Colour", "Current MOT Expiry Date", "Mileage", "Last MOT Test Date");
                table.AddRow(vehicle.Make, vehicle.Model, vehicle.PrimaryColour, vehicle.MotTests[0].ExpiryDate, vehicle.MotTests[0].OdometerValue, vehicle.MotTests[0].CompletedDate);
                Console.WriteLine(table);
            }
            else
            {
                Console.WriteLine("No MOT data exists for this vehicle yet.");
                var table = new ConsoleTable("Make", "Model", "Colour");
                table.AddRow(vehicle.Make, vehicle.Model, vehicle.PrimaryColour);        
                Console.WriteLine(table);
            }
        }

        public static bool RerunApplication()
        {
            Console.WriteLine("Do you want to search for another vehicle? (Y/n)");
            ConsoleKeyInfo runAgain = Console.ReadKey();
            Console.WriteLine();
            return (runAgain.Key == ConsoleKey.Y) ? true : false;
        }

    }
}
