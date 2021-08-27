using GreenFlux.SmartCharging.Application.Connectors.Models;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Connectors.Commands
{
    public class CreateConnectorCommand : IRequest<int>
    {
        public CreateConnector Connector { get; }

        public CreateConnectorCommand(CreateConnector connector)
        {
            Connector = connector;
        }
    }
}
