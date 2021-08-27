using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.Groups.Commands;
using GreenFlux.SmartCharging.Application.Groups.Models;
using GreenFlux.SmartCharging.Application.Groups.Queries;
using GreenFlux.SmartCharging.Domain.Entities;
using MediatR;

namespace GreenFlux.SmartCharging.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> Get(int id)
        {
            Group group = await _mediator.Send(new GetGroupQuery(id));
            if (group == null) return NotFound();
            return group;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewGroupModel>>> GetAll()
        {
            return await _mediator.Send(new GetAllGroupsQuery());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteGroupCommand(id));
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateGroupModel group)
        {
            return await _mediator.Send(new CreateGroupCommand(group));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Group group)
        {
            await _mediator.Send(new UpdateGroupCommand(group));
            return NoContent();
        }
    }
}