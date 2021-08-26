using MediatR;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Commands
{
    public class DeleteChargeStationCommand : IRequest
    {
        public int ChargeStationId { get; }

        public DeleteChargeStationCommand(int chargeStationId)
        {
            ChargeStationId = chargeStationId;
        }
    }
}
