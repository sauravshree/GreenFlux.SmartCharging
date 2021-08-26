using GreenFlux.SmartCharging.Application.Connectors.Models;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Connectors.Commands
{
    public class CreateConnectorCommand : IRequest<int>
    {
        public CreateConnectorModel Connector { get; }

        public CreateConnectorCommand(CreateConnectorModel connector)
        {
            Connector = connector;
        }
    }
}
