using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Features.Sales.Command.UpdateSales;
using Sales.Application.Features.Sales.Queries.GetSalesByRegion;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator mediator;

        public SalesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetSalesByRegion")]
        [ProducesResponseType(typeof(IList<SalesByRegionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<SalesByRegionDto>>> GetSalesByRegion(string region)
        {
            var query = new GetSalesByRegionQuery(region);
            var salesByRegion = await mediator.Send(query);
            return Ok(salesByRegion);
        }


        [HttpPut(Name = "UpdateSalesRecord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateSalesRecord([FromBody] UpdateSalesCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
