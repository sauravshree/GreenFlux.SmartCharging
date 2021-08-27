using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.Groups.Models;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Commands
{
    public class CreateGroupCommand : IRequest<int>
    {
        public CreateGroup Group { get; }

        public CreateGroupCommand(CreateGroup group)
        {
            Group = group;
        }

        internal class Handler : IRequestHandler<CreateGroupCommand, int>
        {
            private readonly IRepository<Group> _repository;

            public Handler(IRepository<Group> repository)
            {
                _repository = repository;
            }
            public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                Group groupEntity = new()
                {
                    Name = request.Group.Name,
                    CapacityAmps = request.Group.CapacityAmps
                };
                return await _repository.CreateAsync(groupEntity);
            }
        }
    }
}
