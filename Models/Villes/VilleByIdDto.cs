using WorkShopI2.Models.Mesures;
using WorkShopI2.Models.Parks;

namespace WorkShopI2.Models.Villes
{
    public class VilleByIdDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public List<ParkInVilleByIdDto> Parks { get; set; }
    }

    public class ParkInVilleByIdDto
    {
        public int Id { get; set; }
        public int VillesId { get; set; }
        //public Ville Villes { get; set; }
        public string Nom { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public MesureDto Mesure { get; set; }
    }
}
