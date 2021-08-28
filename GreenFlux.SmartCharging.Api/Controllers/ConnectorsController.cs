using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.Connectors.Commands;
using GreenFlux.SmartCharging.Application.Connectors.Models;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Api.Controllers
{
    [Route("api/Groups/{groupId}/ChargeStations/{chargeStationId}/[controller]")]
    [ApiController]
    public class ConnectorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Connector> _repository;

        public ConnectorsController(IMediator mediator,
            IRepository<Connector> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(int groupId, int chargeStationId, [FromBody] CreateConnector connector)
        {
            if (chargeStationId != connector.ChargeStationId) return BadRequest();
            (int connectorId, bool success, string error) = await _mediator.Send(new CreateConnectorCommand(groupId, connector));
            if (!success) return BadRequest(error);
            return connectorId;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> Update(int groupId, int chargeStationId, int id, [FromBody] Connector connector)
        {
            if (chargeStationId != connector.ChargeStationId && id != connector.Id) return BadRequest();
            (bool success, string error) = await _mediator.Send(new UpdateConnectorCommand(groupId, connector));
            if (!success) return BadRequest(error);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
