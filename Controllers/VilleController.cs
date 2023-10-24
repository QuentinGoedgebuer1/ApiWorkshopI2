using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public List<Ville> Get()
        {
            return _appDbContext.Villes
                .Include(x => x.Parks)
                .Select(x => new Ville()
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    Parks = x.Parks,
                })
                .ToList();
        }

        [HttpGet("{id}")]
        public Ville GetById(int id) 
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            return _appDbContext.Villes
                .Include(x => x.Parks)
                .Select(x => new Ville()
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    Parks = x.Parks,
                })
                .FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVille([FromQuery] VilleDto villeDto)
        {
            var newVille = new Ville()
            {
                Nom = villeDto.Nom,
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
