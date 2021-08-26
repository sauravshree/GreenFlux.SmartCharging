using GreenFlux.SmartCharging.Application.ChargeStations.Models;
using MediatR;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Commands
{
    public class CreateChargeStationCommand : IRequest<int>
    {
        public CreateChargeStationModel ChargeStation { get; }

        public CreateChargeStationCommand(CreateChargeStationModel chargeStation)
        {
            ChargeStation = chargeStation;
        }
    }
}
