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
                
                // Try multiple common XML structures for coordinate data
                
                // Try common coordinate node names
                var coordNodes = new string[] { "coordinates", "coordinate", "point", "Point", "pos", "Position" };
                
                foreach (string nodeName in coordNodes)
                {
                    var nodes = doc.GetElementsByTagName(nodeName);
                    
                    foreach (XmlNode node in nodes)
                    {
                        if (TryParseCoordinateNode(node, out double lat, out double lon))
                        {
                            coordinates.Add(new CoordinatePair(lat, lon));
                        }
                    }
                    
                    if (coordinates.Count > 0) break; // Found coordinates, stop searching
                }
                
                // If no coordinates found, try parsing the entire XML for lat/lon patterns
                if (coordinates.Count == 0)
                {
                    ParseXmlForLatLonPattern(doc.DocumentElement, coordinates);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error parsing XML file: {ex.Message}", ex);
            }
            
            return coordinates;
        }

        private static bool TryParseCoordinateNode(XmlNode node, out double lat, out double lon)
        {
            lat = 0;
            lon = 0;
            
            try
            {
                // Try various coordinate formats
                
                // Format 1: Space or comma separated values in text content
                string text = node.InnerText?.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    string[] parts = text.Split(new char[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        if (double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out lon) &&
                            double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out lat))
                        {
                            return true;
                        }
                        // Try reverse order
                        if (double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out lat) &&
                            double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out lon))
                        {
                            return true;
                        }
                    }
                }
                
                // Format 2: Separate lat/lon attributes or child elements
                var latAttr = node.Attributes?["lat"] ?? node.Attributes?["latitude"] ?? node.Attributes?["y"];
                var lonAttr = node.Attributes?["lon"] ?? node.Attributes?["longitude"] ?? node.Attributes?["lng"] ?? node.Attributes?["x"];
                
                if (latAttr != null && lonAttr != null)
                {
                    if (double.TryParse(latAttr.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out lat) &&
                        double.TryParse(lonAttr.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out lon))
                    {
                        return true;
                    }
                }
                
                // Format 3: Child elements
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name.ToLower().Contains("lat") && double.TryParse(child.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out lat))
                    {
                        foreach (XmlNode child2 in node.ChildNodes)
                        {
                            if (child2.Name.ToLower().Contains("lon") && double.TryParse(child2.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out lon))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Continue trying other formats
            }
            
            return false;
        }

        private static void ParseXmlForLatLonPattern(XmlNode node, List<CoordinatePair> coordinates)
        {
            if (node == null) return;
            
            // Check if this node contains coordinate-like data
            if (node.NodeType == XmlNodeType.Element)
            {
                string nodeName = node.Name.ToLower();
                if (nodeName.Contains("coord") || nodeName.Contains("point") || nodeName.Contains("position"))
                {
                    if (TryParseCoordinateNode(node, out double lat, out double lon))
                    {
                        coordinates.Add(new CoordinatePair(lat, lon));
                        return; // Don't parse children if we found coordinates in this node
                    }
                }
            }
            
            // Recursively search child nodes
            foreach (XmlNode child in node.ChildNodes)
            {
                ParseXmlForLatLonPattern(child, coordinates);
            }
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
                        continue; // Skip empty lines and comments
                    
                    if (TryParseCoordinateLine(line, out double lat, out double lon))
                    {
                        coordinates.Add(new CoordinatePair(lat, lon));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error parsing text file: {ex.Message}", ex);
            }
            
            return coordinates;
        }

        private static bool TryParseCoordinateLine(string line, out double lat, out double lon)
        {
            lat = 0;
            lon = 0;
            
            try
            {
                // Split by common delimiters
                string[] parts = line.Split(new char[] { ',', ';', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                if (parts.Length >= 2)
                {
                    // Try parsing first two numeric values
                    List<double> values = new List<double>();
                    
                    foreach (string part in parts)
                    {
                        if (double.TryParse(part.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                        {
                            values.Add(value);
                            if (values.Count >= 2) break;
                        }
                    }
                    
                    if (values.Count >= 2)
                    {
                        // Determine which is lat and which is lon based on typical ranges
                        double val1 = values[0];
                        double val2 = values[1];
                        
                        // Latitude is typically -90 to 90, longitude -180 to 180
                        if (Math.Abs(val1) <= 90 && Math.Abs(val2) <= 180)
                        {
                            lat = val1;
                            lon = val2;
                            return true;
                        }
                        else if (Math.Abs(val2) <= 90 && Math.Abs(val1) <= 180)
                        {
                            lat = val2;
                            lon = val1;
                            return true;
                        }
                        else
                        {
                            // If both are valid ranges, assume first is latitude
                            lat = val1;
                            lon = val2;
                            return true;
                        }
                    }
                }
            }
            catch
            {
                // Continue with next attempt
            }
            
            return false;
        }
    }
}
