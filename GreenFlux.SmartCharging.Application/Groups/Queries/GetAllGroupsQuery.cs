using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Application.Groups.Models;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Queries
{
    public class GetAllGroupsQuery : IRequest<List<ViewGroup>>
    {
        internal class Handler : IRequestHandler<GetAllGroupsQuery, List<ViewGroup>>
        {
            private readonly IRepository<Group> _repository;

            public Handler(IRepository<Group> repository)
            {
                _repository = repository;
            }
            public async Task<List<ViewGroup>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
            {
                var groups= await _repository.GetAll();
                return groups.Select(group => new ViewGroup
                    {Id = group.Id, Name = group.Name, CapacityAmps = group.CapacityAmps}).ToList();
            }
        }
    }
}
