using System;
using AgOpenGPS.Helpers;

namespace TestJohnDeere
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Testing John Deere XML Parser...");
                var xmlCoords = JohnDeereFileParser.ParseFile("test_johndeere.xml");
                Console.WriteLine($"XML file parsed successfully. Found {xmlCoords.Count} coordinates:");
                foreach (var coord in xmlCoords)
                {
                    Console.WriteLine($"  Lat: {coord.Latitude}, Lon: {coord.Longitude}");
                }

                Console.WriteLine("\nTesting John Deere TXT Parser...");
                var txtCoords = JohnDeereFileParser.ParseFile("test_johndeere.txt");
                Console.WriteLine($"TXT file parsed successfully. Found {txtCoords.Count} coordinates:");
                foreach (var coord in txtCoords)
                {
                    Console.WriteLine($"  Lat: {coord.Latitude}, Lon: {coord.Longitude}");
                }

                Console.WriteLine("\nParser test completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
