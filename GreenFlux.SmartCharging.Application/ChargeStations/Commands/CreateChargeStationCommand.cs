﻿using System.Collections.Generic;
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
    public class CreateChargeStationCommand : IRequest<int>
    {
        public CreateChargeStationModel ChargeStation { get; }

        public CreateChargeStationCommand(CreateChargeStationModel chargeStation)
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

        internal class Handler : IRequestHandler<CreateChargeStationCommand, int>
        {
            private readonly IRepository<ChargeStation> _chargeStationRepository;
            private readonly IRepository<Connector> _connectorRepository;

            public Handler(IRepository<ChargeStation> chargeStationRepository,
                IRepository<Connector> connectorRepository)
            {
                _chargeStationRepository = chargeStationRepository;
                _connectorRepository = connectorRepository;
            }
            public async Task<int> Handle(CreateChargeStationCommand request, CancellationToken cancellationToken)
            {
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

                return chargeStationId;
            }
        }
    }
}
