using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Commands
{
    public class DeleteGroupCommand : IRequest
    {
        public int GroupId { get; }

        public DeleteGroupCommand(int groupId)
        {
            GroupId = groupId;
        }

        internal class Handler : IRequestHandler<DeleteGroupCommand>
        {
            public Handler()
            {

            }

            public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                return Unit.Value;
            }
        }
    }
}
