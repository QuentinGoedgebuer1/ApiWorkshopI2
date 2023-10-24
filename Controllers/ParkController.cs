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
    public class ParkController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ParkController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public List<Park> Get()
        {
            return _appDbContext.Parks
                .Include(x => x.Villes)
                .Select(x => new Park()
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    VillesId = x.VillesId,
                    Villes = x.Villes,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    Mesures = x.Mesures
                })
                .ToList();
        }

        [HttpGet("{id}")]
        public Park GetById(int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            return _appDbContext.Parks
                .Include(x => x.Villes)
                .Select(x => new Park()
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    VillesId = x.VillesId,
                    Villes = x.Villes,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    Mesures = x.Mesures
                })
                .FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePark([FromQuery] ParkDto parkDto)
        {

            Guard.Against.NegativeOrZero(parkDto.VilleId, nameof(parkDto.VilleId));

            var ville = _appDbContext.Villes.FirstOrDefault(x => x.Id == parkDto.VilleId);

            var newPark = new Park()
            {
                Nom = parkDto.Nom,
                Longitude = parkDto.Longitude,
                Latitude = parkDto.Latitude,
                VillesId = parkDto.VilleId,
                Villes = ville,
            };

            _appDbContext.Parks.Add(newPark);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePark([FromQuery] int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            Park parkToDelete = _appDbContext.Parks.FirstOrDefault(x => x.Id == id);

            _appDbContext.Parks.Remove(parkToDelete);
            await _appDbContext.SaveChangesAsync();

            return Ok(id);
        }
    }
}
