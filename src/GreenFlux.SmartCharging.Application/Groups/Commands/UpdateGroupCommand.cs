using System;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Commands
{
    public class UpdateGroupCommand : IRequest
    {
        public Group Group { get; }

        public UpdateGroupCommand(Group group)
        {
            Group = group;
        }

        internal class Handler : IRequestHandler<UpdateGroupCommand>
        {
            private readonly IRepository<Group> _repository;

            public Handler(IRepository<Group> repository)
            {
                _repository = repository;
            }
            public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                Group existingGroup = await _repository.GetByIdAsync(request.Group.Id);
                if (existingGroup == null) throw new DataMisalignedException($"Group with id: {request.Group.Id} doesn't exist.");
                if (IsChanged(existingGroup, request.Group))
                {
                    await _repository.UpdateAsync(request.Group);
                }
                return Unit.Value;
            }

            private static bool IsChanged(Group existingGroup, Group updatingGroup)
            {
                return updatingGroup.Name != existingGroup.Name ||
                       updatingGroup.CapacityAmps != existingGroup.CapacityAmps;
            }
        }

    }
}
