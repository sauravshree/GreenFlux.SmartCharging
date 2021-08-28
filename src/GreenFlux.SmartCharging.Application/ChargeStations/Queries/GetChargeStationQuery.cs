using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Queries
{
    public class GetChargeStationQuery : IRequest<ChargeStation>
    {
        public int ChargeStationId { get; }

        public GetChargeStationQuery(int chargeStationId)
        {
            ChargeStationId = chargeStationId;
        }

        internal class Handler : IRequestHandler<GetChargeStationQuery, ChargeStation>
        {
            private readonly IRepository<ChargeStation> _repository;

            public Handler(IRepository<ChargeStation> repository)
            {
                _repository = repository;
            }

            public async Task<ChargeStation> Handle(GetChargeStationQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetByIdAsync(request.ChargeStationId, x => x.Connectors);
            }
        }
    }
}
