using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GreenFlux.SmartCharging.Application.ChargeStations.Models;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Commands
{
    public class CreateChargeStationCommand : IRequest<(int chargeStationId, bool success, string error)>
    {
        public CreateChargeStation ChargeStation { get; }

        public CreateChargeStationCommand(CreateChargeStation chargeStation)
        {
            ChargeStation = chargeStation;
        }

        public class Validator : AbstractValidator<CreateChargeStationCommand>
        {
            public Validator()
            {
                RuleFor(x => x.ChargeStation.Connectors).NotEmpty();
                RuleFor(x => x.ChargeStation.MaxConnectors).GreaterThan(0);
                RuleFor(x => x.ChargeStation.Connectors.Count).LessThanOrEqualTo(y => y.ChargeStation.MaxConnectors);
            }
        }

        internal class Handler : IRequestHandler<CreateChargeStationCommand, (int chargeStationId, bool success, string error)>
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
            public async Task<(int chargeStationId, bool success, string error)> Handle(CreateChargeStationCommand request, CancellationToken cancellationToken)
            {
                Group group = await _groupRepository.GetByIdAsync(request.ChargeStation.GroupId);
                if (group == null) throw new DataMisalignedException($"Group doesn't exist with id: {request.ChargeStation.GroupId}");

                List<ChargeStation> existingChargeStations = await _chargeStationRepository.GetAll(x => x.GroupId == group.Id, x => x.Connectors);

                List<double> connectorsMaxCurrentAmps = request.ChargeStation.Connectors.Select(x => x.MaxCurrentAmps).ToList();
                connectorsMaxCurrentAmps.AddRange(existingChargeStations.SelectMany(s => s.Connectors).Select(c => c.MaxCurrentAmps));
                if (!_validationService.IsInGroupCapacity(group.CapacityAmps, connectorsMaxCurrentAmps))
                {
                    return (0, false, $"Group {group.Name} do not have enough capacity for requested connectors.");
                }

                ChargeStation chargeStation = new()
                {
                    GroupId = request.ChargeStation.GroupId,
                    MaxConnectors = request.ChargeStation.MaxConnectors,
                    Name = request.ChargeStation.Name
                };

                int chargeStationId = await _chargeStationRepository.CreateAsync(chargeStation);

                List<Connector> connectors = request.ChargeStation.Connectors.Select(x => new Connector
                {
                    ChargeStationId = chargeStationId,
                    MaxCurrentAmps = x.MaxCurrentAmps
                }).ToList();

                await _connectorRepository.CreateAsync(connectors);

                return (chargeStationId, true, null);
            }
        }
    }
}
