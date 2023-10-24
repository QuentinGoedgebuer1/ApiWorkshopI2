using WorkShopI2.Models.Parks;

namespace WorkShopI2.Models.Villes
{
    public class Ville
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public List<Park> Parks { get; set; }
    }
}
