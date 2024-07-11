using Microsoft.AspNetCore.Mvc;
using Parking.Domain;
using Parking.Server.Abstractions.Models;

namespace Parking.Server.Controllers
{
    [ApiController]
    [Route("parking")]
    [Produces("application/json")]
    public class ParkingsController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet, Route("get-items")]
        public IActionResult Get(CancellationToken cancellationToken)
        {
            var items = _context.Parkings!.ToList();
            return Ok(items);
        }

        [HttpPost, Route("save")]
        public IActionResult Add(Parkings parking)
        {
            var items = _context.Add(parking);
            _context.SaveChanges();

            return Ok(items.Entity);
        }
    }
}
