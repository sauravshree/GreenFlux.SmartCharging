using GreenFlux.SmartCharging.Application.Groups.Models;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Commands
{
    public class CreateGroupCommand : IRequest<int>
    {
        public CreateGroupModel Group { get; }

        public CreateGroupCommand(CreateGroupModel group)
        {
            Group = group;
        }

    }
}
