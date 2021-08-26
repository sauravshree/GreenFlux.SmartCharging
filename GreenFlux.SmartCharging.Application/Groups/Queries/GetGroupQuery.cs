using GreenFlux.SmartCharging.Domain.Entities;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Queries
{
    public class GetGroupQuery : IRequest<Group>
    {
        public int GroupId { get; }

        public GetGroupQuery(int groupId)
        {
            GroupId = groupId;
        }
    }
}
