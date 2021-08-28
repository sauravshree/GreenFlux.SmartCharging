using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.Connectors.Models;
using GreenFlux.SmartCharging.Application.Services;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Connectors.Commands
{
    public class CreateConnectorCommand : IRequest<(int connectorId, bool success, string error)>
    {
        public int GroupId { get; }
        public CreateConnector Connector { get; }

        public CreateConnectorCommand(int groupId, CreateConnector connector)
        {
            GroupId = groupId;
            Connector = connector;
        }

        internal class Handler : IRequestHandler<CreateConnectorCommand, (int connectorId, bool success, string error)>
        {
            private readonly IRepository<Group> _groupRepository;
            private readonly IRepository<ChargeStation> _chargeStationRepository;
            private readonly IRepository<Connector> _connectorRepository;

            public Handler(IRepository<Group> groupRepository,
                IRepository<ChargeStation> chargeStationRepository,
                IRepository<Connector> connectorRepository)
            {
                _groupRepository = groupRepository;
                _chargeStationRepository = chargeStationRepository;
                _connectorRepository = connectorRepository;
            }
            public async Task<(int connectorId, bool success, string error)> Handle(CreateConnectorCommand request, CancellationToken cancellationToken)
            {
                ChargeStation chargeStation = await _chargeStationRepository.GetByIdAsync(request.Connector.ChargeStationId, x => x.Connectors);
                if (chargeStation.MaxConnectors == chargeStation.Connectors.Count) return (0, false, $"Charge station {chargeStation.Name} has already reached max number of connectors. Max allowed connectors: {chargeStation.MaxConnectors}");

                Group group = await _groupRepository.GetByIdAsync(request.GroupId);
                if (group == null) throw new DataMisalignedException($"Group doesn't exist with id: {request.GroupId}");

                List<ChargeStation> existingChargeStations = await _chargeStationRepository.GetAll(x => x.GroupId == group.Id, x => x.Connectors);

                List<double> connectorsMaxCurrentAmps = new() { request.Connector.MaxCurrentAmps };
                connectorsMaxCurrentAmps.AddRange(existingChargeStations.SelectMany(s => s.Connectors)
                    .Select(c => c.MaxCurrentAmps).ToList());

                if (!ValidationService.IsInGroupCapacity(group.CapacityAmps, connectorsMaxCurrentAmps))
                {
                    return (0, false, $"Group {group.Name} do not have enough capacity for requested connectors.");
                }

                Connector connector = new()
                {
                    ChargeStationId = request.Connector.ChargeStationId,
                    MaxCurrentAmps = request.Connector.MaxCurrentAmps
                };
                int connectorId = await _connectorRepository.CreateAsync(connector);
                return (connectorId, true, null);
            }
        }
    }
}
