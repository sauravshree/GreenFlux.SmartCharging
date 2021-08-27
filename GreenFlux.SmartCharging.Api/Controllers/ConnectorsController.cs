using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.Connectors.Commands;
using GreenFlux.SmartCharging.Application.Connectors.Models;
using GreenFlux.SmartCharging.Domain.Entities;
using MediatR;

namespace GreenFlux.SmartCharging.Api.Controllers
{
    [Route("api/Groups/{groupId}/ChargeStations/{chargeStationId}/[controller]")]
    [ApiController]
    public class ConnectorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConnectorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateConnector connector)
        {
            return await _mediator.Send(new CreateConnectorCommand(connector));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> Update([FromBody] Connector connector)
        {
            await _mediator.Send(new UpdateConnectorCommand(connector));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteConnectorCommand(id));
            return NoContent();
        }
    }
}
