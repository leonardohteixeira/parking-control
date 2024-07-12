using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Notice;
using Parking.Domain;
using Parking.Domain.Abstractions.Models;
using Parking.Server.Abstractions.Models;

namespace Parking.Server.Controllers;

[ApiController]
[Route("parking-values")]
[Produces("application/json")]
public class ParkingValuesController(ApplicationDbContext context) : Controller
{
    [HttpGet, Route("get-values")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await context.Set<ParkingValues>().FirstOrDefaultAsync(cancellationToken));
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao obter valores: {e.Message}");
        }
    }

    [HttpPost, Route("save-values")]
    public async Task<IActionResult> Save([FromBody] ParkingValues parkingValues, CancellationToken cancellationToken)
    {
        try
        {
            var existent = await context.Set<ParkingValues>().FirstOrDefaultAsync(p => p.ParkingValuesId == parkingValues.ParkingValuesId, cancellationToken);

            if (existent is not null)
            {
                existent.Value = parkingValues.Value;
                existent.HalfInTheFirstHour = parkingValues.HalfInTheFirstHour;
                existent.HourlyTolerance = parkingValues.HourlyTolerance;
            }
            else
            {
                await context.AddAsync(parkingValues, cancellationToken);
                existent = parkingValues;
            }

            await context.SaveChangesAsync(cancellationToken);

            return Ok(existent);
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao salvar valores: {e.Message}");
        }
    }
}
