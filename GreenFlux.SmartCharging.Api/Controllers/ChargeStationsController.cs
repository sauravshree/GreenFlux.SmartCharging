using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.ChargeStations.Commands;
using GreenFlux.SmartCharging.Application.ChargeStations.Models;
using GreenFlux.SmartCharging.Application.ChargeStations.Queries;
using GreenFlux.SmartCharging.Application.Groups.Commands;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ChargeStation>> Get(int groupId, int id)
        {
            ChargeStation chargeStation = await _mediator.Send(new GetChargeStationQuery(id));
            if (groupId != chargeStation.GroupId) return BadRequest();
            return chargeStation;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(int groupId, [FromBody] CreateChargeStationModel chargeStation)
        {
            if (groupId != chargeStation.GroupId) return BadRequest();
            return await _mediator.Send(new CreateChargeStationCommand(chargeStation));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> Update(int groupId, [FromBody] ChargeStation chargeStation)
        {
            if (groupId != chargeStation.GroupId) return BadRequest();
            await _mediator.Send(new UpdateChargeStationCommand(chargeStation));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteGroupCommand(id));
            return NoContent();
        }
    }
}
