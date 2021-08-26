using System.Collections.Generic;
using GreenFlux.SmartCharging.Domain.Entities;
using MediatR;

namespace GreenFlux.SmartCharging.Application.Groups.Queries
{
    public class GetAllGroupsQuery : IRequest<List<Group>>
    {
    }
}
