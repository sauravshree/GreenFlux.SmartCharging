using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.ChargeStations.Models;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Queries
{
    public class GetGroupChargeStationsQuery : IRequest<List<ViewChargeStation>>
    {
        public int GroupId { get; }

        public GetGroupChargeStationsQuery(int groupId)
        {
            GroupId = groupId;
        }

        internal class Handler : IRequestHandler<GetGroupChargeStationsQuery, List<ViewChargeStation>>
        {
            private readonly IRepository<ChargeStation> _repository;

            public Handler(IRepository<ChargeStation> repository)
            {
                _repository = repository;
            }
            public async Task<List<ViewChargeStation>> Handle(GetGroupChargeStationsQuery request, CancellationToken cancellationToken)
            {
                List<ChargeStation> chargeStations = await _repository.GetAll(x => x.GroupId == request.GroupId);
                return chargeStations.Select(x => new ViewChargeStation
                {
                    Id = x.Id,
                    GroupId = x.GroupId,
                    Name = x.Name,
                    MaxConnectors = x.MaxConnectors
                }).ToList();
            }
        }
    }
}
