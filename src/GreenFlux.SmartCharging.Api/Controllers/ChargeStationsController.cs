using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.ChargeStations.Commands;
using GreenFlux.SmartCharging.Application.ChargeStations.Models;
using GreenFlux.SmartCharging.Application.ChargeStations.Queries;
using GreenFlux.SmartCharging.Domain.Entities;
using MediatR;

namespace GreenFlux.SmartCharging.Api.Controllers
{
    [Route("api/Groups/{groupId}/[controller]")]
    [ApiController]
    public class ChargeStationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChargeStationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewChargeStation>>> GetGroupChargeStations(int groupId)
        {
            return await _mediator.Send(new GetGroupChargeStationsQuery(groupId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChargeStation>> Get(int groupId, int id)
        {
            ChargeStation chargeStation = await _mediator.Send(new GetChargeStationQuery(id));

            if (chargeStation == null || groupId != chargeStation.GroupId) return NotFound();
            return chargeStation;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(int groupId, [FromBody] CreateChargeStation chargeStation)
        {
            if (groupId != chargeStation.GroupId) return BadRequest();
            (int chargeStationId, bool success, string error) = await _mediator.Send(new CreateChargeStationCommand(chargeStation));
            if (!success) return BadRequest(error);
            return chargeStationId;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> Update(int groupId, [FromBody] UpdateChargeStation chargeStation)
        {
            if (groupId != chargeStation.GroupId) return BadRequest();
            await _mediator.Send(new UpdateChargeStationCommand(chargeStation));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteChargeStationCommand(id));
            return NoContent();
        }
    }
}
