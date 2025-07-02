using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace AgOpenGPS.Helpers
{
    public class CoordinatePair
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public CoordinatePair(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
    }

    public static class ImportFileParser
    {
        public static bool IsImportFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".shp" || extension == ".xml" || extension == ".txt";
        }

        public static List<CoordinatePair> ParseFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            
            switch (extension)
            {
                case ".xml":
                    return ParseXmlFile(filePath);
                case ".txt":
                    return ParseTextFile(filePath);
                case ".shp":
                    // For shapefile support, would need additional library like SharpMap
                    // For now, return empty list with a note
                    throw new NotImplementedException("Shapefile support requires additional libraries. Please convert to XML or TXT format.");
                default:
                    throw new ArgumentException("Unsupported file format");
            }
        }

        private static List<CoordinatePair> ParseXmlFile(string filePath)
        {
            var coordinates = new List<CoordinatePair>();
            
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                // Look for coordinate elements in various John Deere XML formats
                XmlNodeList coordinateNodes = doc.SelectNodes("//coordinates | //coordinate | //point | //Point");
                
                if (coordinateNodes != null && coordinateNodes.Count > 0)
                {
                    foreach (XmlNode node in coordinateNodes)
                    {
                        try
                        {
                            double lat = 0, lon = 0;
                            
                            // Try different attribute names that John Deere might use
                            if (node.Attributes["lat"] != null && node.Attributes["lon"] != null)
                            {
                                lat = double.Parse(node.Attributes["lat"].Value, CultureInfo.InvariantCulture);
                                lon = double.Parse(node.Attributes["lon"].Value, CultureInfo.InvariantCulture);
                            }
                            else if (node.Attributes["latitude"] != null && node.Attributes["longitude"] != null)
                            {
                                lat = double.Parse(node.Attributes["latitude"].Value, CultureInfo.InvariantCulture);
                                lon = double.Parse(node.Attributes["longitude"].Value, CultureInfo.InvariantCulture);
                            }
                            else if (!string.IsNullOrEmpty(node.InnerText))
                            {
                                // Try to parse comma-separated lat,lon
                                string[] coords = node.InnerText.Split(',');
                                if (coords.Length >= 2)
                                {
                                    lat = double.Parse(coords[0].Trim(), CultureInfo.InvariantCulture);
                                    lon = double.Parse(coords[1].Trim(), CultureInfo.InvariantCulture);
                                }
                            }
                            
                            if (lat != 0 && lon != 0)
                            {
                                coordinates.Add(new CoordinatePair(lat, lon));
                            }
                        }
                        catch
                        {
                            // Skip invalid coordinates
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Error parsing XML file: {ex.Message}");
            }
            
            return coordinates;
        }

        private static List<CoordinatePair> ParseTextFile(string filePath)
        {
            var coordinates = new List<CoordinatePair>();
            
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#") || line.StartsWith("//"))
                        continue;
                    
                    try
                    {
                        // Try different text formats:
                        // Format 1: lat,lon
                        // Format 2: lat lon
                        // Format 3: lon lat (sometimes reversed)
                        
                        string[] parts = line.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        
                        if (parts.Length >= 2)
                        {
                            double coord1 = double.Parse(parts[0], CultureInfo.InvariantCulture);
                            double coord2 = double.Parse(parts[1], CultureInfo.InvariantCulture);
                            
                            // Determine if coordinates are lat/lon or lon/lat based on ranges
                            double lat, lon;
                            if (Math.Abs(coord1) <= 90 && Math.Abs(coord2) <= 180)
                            {
                                // Assume first is latitude
                                lat = coord1;
                                lon = coord2;
                            }
                            else if (Math.Abs(coord2) <= 90 && Math.Abs(coord1) <= 180)
                            {
                                // Assume second is latitude
                                lat = coord2;
                                lon = coord1;
                            }
                            else
                            {
                                // Skip invalid coordinates
                                continue;
                            }
                            
                            coordinates.Add(new CoordinatePair(lat, lon));
                        }
                    }
                    catch
                    {
                        // Skip invalid lines
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Error parsing text file: {ex.Message}");
            }
            
            return coordinates;
        }
    }
}
