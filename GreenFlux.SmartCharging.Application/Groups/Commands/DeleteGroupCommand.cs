using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Commands
{
    public class DeleteGroupCommand : IRequest
    {
        public int GroupId { get; }

        public DeleteGroupCommand(int groupId)
        {
            GroupId = groupId;
        }

        internal class Handler : IRequestHandler<DeleteGroupCommand>
        {
            private readonly IRepository<Group> _repository;

            public Handler(IRepository<Group> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                //Also deletes associated charge stations and connectors because of db constraint onDelete.Cascade
                await _repository.DeleteAsync(request.GroupId);
                return Unit.Value;
            }
        }
    }
}
