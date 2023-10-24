using WorkShopI2.Models.Parks;

namespace WorkShopI2.Models.Mesures
{
    public class Mesure
    {
        public int Id { get; set; }
        public string Temperature { get; set; }
        public string Humidite { get; set;}
        public DateTimeOffset DateHeure { get; set; }
        public int ParkId { get; set; }
        public Park Park { get; set; }
    }
}
