using Microsoft.AspNetCore.Mvc;
using Parking.Domain;
using Parking.Server.Abstractions.Models;

namespace Parking.Server.Controllers
{
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private AplicationDbContext _context;

        public ParkingController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, Route("get-items")]
        public IActionResult Get()
        {
            var items = _context.Parkings.ToList();
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
