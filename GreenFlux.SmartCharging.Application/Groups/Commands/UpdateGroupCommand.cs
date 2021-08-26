using System;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Commands
{
    public class UpdateGroupCommand : IRequest
    {
        public Group Group { get; }

        public UpdateGroupCommand(Group group)
        {
            Group = @group;
        }


        internal class Handler : IRequestHandler<UpdateGroupCommand>
        {
            public Handler()
            {

            }
            
            public Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
