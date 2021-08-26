using MediatR;

namespace GreenFlux.SmartCharging.Application.Connectors.Commands
{
    public class DeleteConnectorCommand : IRequest
    {
        public int ConnectorId { get; }

        public DeleteConnectorCommand(int connectorId)
        {
            ConnectorId = connectorId;
        }
    }
}
