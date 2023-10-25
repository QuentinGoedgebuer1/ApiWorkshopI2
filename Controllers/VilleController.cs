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
        public Ville GetById(int id) 
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            return _appDbContext.Villes
                .Include(x => x.Parks)
                .Select(x => new Ville()
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
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
