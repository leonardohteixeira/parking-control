using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Domain;
using Parking.Domain.Abstractions.Models;
using Parking.Server.Abstractions.Models;

namespace Parking.Server.Controllers
{
    [ApiController]
    [Route("parkings")]
    [Produces("application/json")]
    public class ParkingsController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet, Route("get-items")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var items = await _context.Set<Parkings>().ToListAsync(cancellationToken);
                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao obter os itens: {e.Message}");
            }
        }

        [HttpPost, Route("save")]
        public async Task<IActionResult> Save([FromBody] Parkings parking, CancellationToken cancellationToken)
        {
            try
            {
                var existent = await _context.Set<Parkings>().FirstOrDefaultAsync(p => p.ParkingsId == parking.ParkingsId, cancellationToken);

                if (existent is not null)
                {
                    existent.Plate = parking.Plate;
                    existent.ArrivalTime = parking.ArrivalTime;
                    existent.DepartureTime = parking.DepartureTime;

                    if (existent.DepartureTime is not null)
                    {
                        var values = await context.Set<ParkingValues>().FirstOrDefaultAsync(cancellationToken);

                        if (values is not null)
                            existent = await Calculate(existent, values, cancellationToken);
                    }
                }
                else
                {
                    await _context.AddAsync(parking, cancellationToken);
                    existent = parking;
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Ok(existent);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao salvar o item: {e.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var parking = await _context.Set<Parkings>().FirstOrDefaultAsync(p => p.ParkingsId == id, cancellationToken);

                if (parking == null)
                {
                    return BadRequest("Item de estacionamento não encontrado.");
                }

                _context.Entry(parking).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);

                return Ok(parking);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao marcar data: {e.Message}");
            }
        }

        [HttpDelete, Route("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var item = await _context.Set<Parkings>().FirstOrDefaultAsync(p => p.ParkingsId == id, cancellationToken);
            if (item == null)
            {
                return BadRequest();
            }

            _context.Set<Parkings>().Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(item);
        }

        private static async Task<Parkings> Calculate(Parkings parkings, ParkingValues values, CancellationToken cancellationToken)
        {
            if (parkings.ArrivalTime is null && parkings.DepartureTime is null)
                return parkings;

            var timePassed = parkings.DepartureTime - parkings.ArrivalTime ?? TimeSpan.Zero;

            if (timePassed.TotalMinutes <= 30)
            {
                parkings.TotalValue = values.Value / 2;
                return parkings;
            }

            var fullHours = timePassed.TotalMinutes / 60;
            var remainingMinutes = timePassed.TotalMinutes % 60;

            var totalFee = decimal.Parse(fullHours.ToString()) * values.Value;

            if (remainingMinutes > values.HourlyTolerance)
                totalFee += values.Value;

            parkings.TotalValue = totalFee;

            return parkings;
        }
    }
}
