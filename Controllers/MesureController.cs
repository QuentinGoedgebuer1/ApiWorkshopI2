using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkShopI2.Models.Mesures;
using WorkShopI2.Models.Parks;
using WorkShopI2.Models.Villes;

namespace WorkShopI2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MesureController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public MesureController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public List<Mesure> Get()
        {
            return _appDbContext.Mesures
                .Include(x => x.Park)
                    .ThenInclude(x => x.Villes)
                .Select(x => new Mesure()
                {
                    Id = x.Id,
                    Temperature = x.Temperature,
                    Humidite = x.Humidite,
                    AQI = x.AQI,
                    DateHeure = x.DateHeure,
                    ParkId = x.ParkId,
                    Park = new Park()
                    {
                        Id = x.Park.Id,
                        Latitude = x.Park.Latitude,
                        Longitude = x.Park.Longitude,
                        Nom = x.Park.Nom,
                        VillesId = x.Park.VillesId,
                        Villes = x.Park.Villes

                    }
                })
                .Take(20)
                .ToList();
        }

        [HttpGet("{id}")]
        public Mesure GetById(int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            return _appDbContext.Mesures
                .Include(x => x.Park)
                    .ThenInclude(x => x.Villes)
                .Select(x => new Mesure()
                {
                    Id = x.Id,
                    Temperature = x.Temperature,
                    Humidite = x.Humidite,
                    AQI = x.AQI,
                    DateHeure = x.DateHeure,
                    ParkId = x.ParkId,
                    Park = new Park()
                    {
                        Id = x.Park.Id,
                        Latitude = x.Park.Latitude,
                        Longitude = x.Park.Longitude,
                        Nom = x.Park.Nom,
                        VillesId = x.Park.VillesId,
                        Villes = x.Park.Villes

                    }
                })
                .FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMesure([FromQuery] MesureDto mesureDto)
        {
            Guard.Against.NegativeOrZero(mesureDto.ParkId, nameof(mesureDto.ParkId));

            var park = _appDbContext.Parks.FirstOrDefault(x => x.Id == mesureDto.ParkId);

            var newMesure = new Mesure()
            {
                Temperature = mesureDto.Temperature,
                Humidite = mesureDto.Humidite,
                AQI = mesureDto.AQI,
                DateHeure = mesureDto.DateHeure,
                ParkId = mesureDto.ParkId,
                Park = park
            };

            _appDbContext.Mesures.Add(newMesure);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMesure([FromQuery] int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            Mesure mesureToDelete = _appDbContext.Mesures.FirstOrDefault(x => x.Id == id);

            _appDbContext.Mesures.Remove(mesureToDelete);
            await _appDbContext.SaveChangesAsync();

            return Ok(id);
        }
    }
}
