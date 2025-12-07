using System;
using System.Collections.Generic;
using System.IO;

namespace ReadMunicipalityData
{
    internal class Municipality
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Population { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "kunnat2024.csv";

            try
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

                foreach (var municipality in municipalities)
                {
                    Console.WriteLine($"{municipality.ID}: {municipality.Name} ({municipality.Population})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    }
}
