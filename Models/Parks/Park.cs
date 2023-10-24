using WorkShopI2.Models.Mesures;
using WorkShopI2.Models.Villes;

namespace WorkShopI2.Models.Parks
{
    public class Park
    {
        public int Id { get; set; }
        public int VillesId { get; set; }
        public Ville Villes { get; set; }
        public string Nom { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public List<Mesure> Mesures { get; set; }
    }
}
