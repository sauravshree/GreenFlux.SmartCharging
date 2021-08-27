using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Queries
{
    public class GetGroupQuery : IRequest<Group>
    {
        public int GroupId { get; }

        public GetGroupQuery(int groupId)
        {
            GroupId = groupId;
        }

        internal class Handler : IRequestHandler<GetGroupQuery, Group>
        {
            private readonly IRepository<Group> _repository;

            public Handler(IRepository<Group> repository)
            {
                _repository = repository;
            }
            public async Task<Group> Handle(GetGroupQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetByIdAsync(request.GroupId);
            }
        }
    }
}
