using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.ChargeStations;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Connectors.Commands
{
    public class UpdateConnectorCommand : IRequest<(bool success, string error)>
    {
        public int GroupId { get; }
        public Connector Connector { get; }

        public UpdateConnectorCommand(int groupId, Connector connector)
        {
            GroupId = groupId;
            Connector = connector;
        }

        internal class Handler : IRequestHandler<UpdateConnectorCommand, (bool success, string error)>
        {
            private readonly IRepository<Group> _groupRepository;
            private readonly IRepository<ChargeStation> _chargeStationRepository;
            private readonly IRepository<Connector> _connectorRepository;
            private readonly IValidationService _validationService;

            public Handler(IRepository<Group> groupRepository,
                IRepository<ChargeStation> chargeStationRepository,
                IRepository<Connector> connectorRepository,
                IValidationService validationService)
            {
                _groupRepository = groupRepository;
                _chargeStationRepository = chargeStationRepository;
                _connectorRepository = connectorRepository;
                _validationService = validationService;
            }
            public async Task<(bool success, string error)> Handle(UpdateConnectorCommand request, CancellationToken cancellationToken)
            {
                Group group = await _groupRepository.GetByIdAsync(request.GroupId);
                if (group == null) throw new DataMisalignedException($"Group doesn't exist with id: {request.GroupId}");

                List<ChargeStation> existingChargeStations = await _chargeStationRepository.GetAll(x => x.GroupId == group.Id, x => x.Connectors);

                List<double> connectorsMaxCurrentAmps = new() { request.Connector.MaxCurrentAmps };
                connectorsMaxCurrentAmps.AddRange(existingChargeStations.SelectMany(s => s.Connectors)
                    .Where(x => x.Id != request.Connector.Id)
                    .Select(c => c.MaxCurrentAmps).ToList());

                if (!_validationService.IsInGroupCapacity(group.CapacityAmps, connectorsMaxCurrentAmps))
                {
                    return (false, $"Group {group.Name} do not have enough capacity for requested connectors.");
                }

                await _connectorRepository.UpdateAsync(request.Connector);
                return (true, null);
            }
        }
    }
}
