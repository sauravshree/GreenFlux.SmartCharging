using GreenFlux.SmartCharging.Domain.Entities;
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
    }
}
