using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkShopI2.Models.Mesures;
using WorkShopI2.Models.Parks;
using WorkShopI2.Models.Villes;
using WorkShopI2.Models.WeatherForecast;

namespace WorkShopI2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VilleController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public VilleController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public PaginatedList<Ville> Get(int page = 1, int pageSize = 5)
        {
            List<Ville> villeList = _appDbContext.Villes
                .Include(x => x.Parks)
                .Select(x => new Ville()
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    Parks = x.Parks
                })
                .ToList();

            int startIndex = (page - 1) * pageSize;

            var villePaginatedList = villeList.OrderByDescending(x => x.Id).Skip(startIndex).Take(pageSize).ToList();

            var newVillePaginatedList = new PaginatedList<Ville>
            {
                Items = villePaginatedList,
                PageNumber = page,
                TotalItems = villeList.Count(),
                ItemsPerPage = pageSize,
            };

            return newVillePaginatedList;
        }

        [HttpGet("{id}")]
        public VilleByIdDto GetById(int id) 
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            var mesures = _appDbContext.Mesures.ToList();

            var listNewParks = new List<ParkInVilleByIdDto>() { };

            var listParks = _appDbContext.Parks.Include(x => x.Mesures).ToList();
            if (listParks.Any())
            {
                listParks.ForEach(x =>
                {
                    if (x.Mesures.Any())
                    {
                        var park = new ParkInVilleByIdDto()
                        {
                            Id = x.Id,
                            Latitude = x.Latitude,
                            Longitude = x.Longitude,
                            Nom = x.Nom,
                            VillesId = x.VillesId,
                            Mesure = new MesureDto()
                            {
                                AQI = mesures.FirstOrDefault(m => m.ParkId == x.Id).AQI,
                                DateHeure = mesures.FirstOrDefault(m => m.ParkId == x.Id).DateHeure,
                                Humidite = mesures.FirstOrDefault(m => m.ParkId == x.Id).Humidite,
                                ParkId = mesures.FirstOrDefault(m => m.ParkId == x.Id).ParkId,
                                Temperature = mesures.FirstOrDefault(m => m.ParkId == x.Id).Temperature
                            }
                        };
                        listNewParks.Add(park);
                    }
                    else
                    {
                        var park = new ParkInVilleByIdDto()
                        {
                            Id = x.Id,
                            Latitude = x.Latitude,
                            Longitude = x.Longitude,
                            Nom = x.Nom,
                            VillesId = x.VillesId,
                        };

                        listNewParks.Add(park);
                    }
                });
            }
            

            var ville = _appDbContext.Villes
                .Include(x => x.Parks)
                .Select(x => new VilleByIdDto()
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    Parks = listNewParks
                })
                .FirstOrDefault(x => x.Id == id);

            return ville;
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateVille([FromQuery] VilleDto villeDto)
        {
            var newVille = new Ville()
            {
                Nom = villeDto.Nom,
                Longitude = villeDto.Longitude,
                Latitude = villeDto.Latitude,
            };
            _appDbContext.Villes.Add(newVille);
            await _appDbContext.SaveChangesAsync();
            return Ok(newVille.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVille([FromQuery] int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            Ville villeToDelete = _appDbContext.Villes.FirstOrDefault(x => x.Id == id);

            _appDbContext.Villes.Remove(villeToDelete);
            await _appDbContext.SaveChangesAsync();

            return Ok(id);
        }
    }
}
