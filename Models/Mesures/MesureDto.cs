namespace WorkShopI2.Models.Mesures
{
    public class MesureDto
    {
        public string Temperature { get; set; }
        public string Humidite { get; set; }
        public string AQI { get; set; }
        public DateTimeOffset DateHeure { get; set; }
        public int ParkId { get; set; }
    }
}
