using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.ChargeStations.Models;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Commands
{
    public class UpdateChargeStationCommand : IRequest
    {
        public UpdateChargeStation ChargeStation { get; }

        public UpdateChargeStationCommand(UpdateChargeStation chargeStation)
        {
            ChargeStation = chargeStation;
        }

        internal class Handler : IRequestHandler<UpdateChargeStationCommand>
        {
            private readonly IRepository<ChargeStation> _repository;

            public Handler(IRepository<ChargeStation> repository)
            {
                _repository = repository;
            }
            public async Task<Unit> Handle(UpdateChargeStationCommand request, CancellationToken cancellationToken)
            {
                ChargeStation chargeStation = new()
                {
                    Id = request.ChargeStation.Id,
                    GroupId = request.ChargeStation.GroupId,
                    MaxConnectors = request.ChargeStation.MaxConnectors,
                    Name = request.ChargeStation.Name
                };
                await _repository.UpdateAsync(chargeStation);
                return Unit.Value;
            }
        }
    }
}
