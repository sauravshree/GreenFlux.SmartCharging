using GreenFlux.SmartCharging.Domain.Entities;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Connectors.Commands
{
    public class UpdateConnectorCommand : IRequest
    {
        public Connector Connector { get; }

        public UpdateConnectorCommand(Connector connector)
        {
            Connector = connector;
        }
    }
}
