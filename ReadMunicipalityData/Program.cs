namespace ReadMunicipalityData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "kunnat2024.csv";

            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    }
}
