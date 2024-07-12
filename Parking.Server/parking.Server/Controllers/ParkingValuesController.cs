using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var items = await context.AddAsync(parkingValues, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Ok(items.Entity);
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao salvar valores: {e.Message}");
        }
    }
}
