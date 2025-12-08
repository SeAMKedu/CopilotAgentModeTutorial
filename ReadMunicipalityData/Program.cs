using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReadMunicipalityData
{
    internal class Municipality
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Population { get; set; }
        public string Region { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string filePath = "kunnat2024.csv";
            const string outputPath = "kunnat2024_enriched.csv";

            try
            {
                var municipalities = LoadMunicipalities(filePath);

                using var httpClient = CreateHttpClient();
                await EnrichWithNominatimAsync(municipalities, httpClient);

                WriteMunicipalitiesCsv(municipalities, outputPath);

                Console.WriteLine($"\nEnriched data written to {outputPath}");
                Console.WriteLine($"Total municipalities processed: {municipalities.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }

        private static List<Municipality> LoadMunicipalities(string filePath)
        {
            var municipalities = new List<Municipality>();

            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(';');
                if (parts.Length < 3)
                    continue;

                if (!int.TryParse(parts[0].Trim(), out var id))
                    continue;

                if (!int.TryParse(parts[2].Trim(), out var population))
                    continue;

                municipalities.Add(new Municipality
                {
                    ID = id,
                    Name = parts[1].Trim(),
                    Population = population
                });
            }

            return municipalities;
        }

        private static async Task EnrichWithNominatimAsync(IEnumerable<Municipality> municipalities, HttpClient httpClient)
        {
            foreach (var municipality in municipalities)
            {
                try
                {
                    var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(municipality.Name)},Finland&format=json&addressdetails=1&limit=1";
                    var response = await httpClient.GetStringAsync(url);

                    using var doc = JsonDocument.Parse(response);
                    var root = doc.RootElement;

                    if (root.GetArrayLength() > 0)
                    {
                        var firstResult = root[0];
                        if (firstResult.TryGetProperty("lat", out var lat) &&
                            firstResult.TryGetProperty("lon", out var lon))
                        {
                            if (double.TryParse(lat.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var latValue))
                                municipality.Latitude = latValue;

                            if (double.TryParse(lon.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var lonValue))
                                municipality.Longitude = lonValue;
                        }

                        if (firstResult.TryGetProperty("address", out var address))
                        {
                            if (address.TryGetProperty("county", out var county))
                            {
                                municipality.Region = county.GetString() ?? "";
                            }
                            else if (address.TryGetProperty("state", out var state))
                            {
                                municipality.Region = state.GetString() ?? "";
                            }
                            else if (address.TryGetProperty("state_district", out var stateDistrict))
                            {
                                municipality.Region = stateDistrict.GetString() ?? "";
                            }
                            else if (address.TryGetProperty("region", out var region))
                            {
                                municipality.Region = region.GetString() ?? "";
                            }
                        }
                    }

                    Console.WriteLine($"Processed: {municipality.Name}");
                    await Task.Delay(1000); // Respect rate limits
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching data for {municipality.Name}: {ex.Message}");
                }
            }
        }

        private static void WriteMunicipalitiesCsv(IEnumerable<Municipality> municipalities, string outputPath)
        {
            using var writer = new StreamWriter(outputPath);
            writer.WriteLine("ID;Name;Population;Region;Latitude;Longitude");

            foreach (var municipality in municipalities)
            {
                var latitude = municipality.Latitude.ToString(CultureInfo.InvariantCulture);
                var longitude = municipality.Longitude.ToString(CultureInfo.InvariantCulture);

                writer.WriteLine($"{municipality.ID};{municipality.Name};{municipality.Population};{municipality.Region};{latitude};{longitude}");
            }
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "MunicipalityDataApp/1.0");
            return client;
        }
    }
}
