using WorkShopI2.Models.Villes;

namespace WorkShopI2.Models.Parks
{
    public class ParkDto
    {
        public string Nom { get; set; }
        public int VilleId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
