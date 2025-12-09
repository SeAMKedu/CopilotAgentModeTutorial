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
}
