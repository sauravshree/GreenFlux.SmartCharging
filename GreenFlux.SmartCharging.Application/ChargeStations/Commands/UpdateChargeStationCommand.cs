using GreenFlux.SmartCharging.Domain.Entities;
using MediatR;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Commands
{
    public class UpdateChargeStationCommand : IRequest
    {
        public ChargeStation ChargeStation { get; }

        public UpdateChargeStationCommand(ChargeStation chargeStation)
        {
            ChargeStation = chargeStation;
        }
    }
}
