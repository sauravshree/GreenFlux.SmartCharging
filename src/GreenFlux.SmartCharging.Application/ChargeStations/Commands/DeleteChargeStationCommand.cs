using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
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

        internal class Handler : IRequestHandler<DeleteChargeStationCommand>
        {
            private readonly IRepository<ChargeStation> _chargeStationRepository;

            public Handler(IRepository<ChargeStation> chargeStationRepository)
            {
                _chargeStationRepository = chargeStationRepository;
            }
            public async Task<Unit> Handle(DeleteChargeStationCommand request, CancellationToken cancellationToken)
            {
                //Also deletes associated connectors because of db constraint onDelete.Cascade
                await _chargeStationRepository.DeleteAsync(request.ChargeStationId);
                return Unit.Value;
            }
        }
    }
}
