using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;

namespace GreenFlux.SmartCharging.Application.ChargeStations
{
    public interface IValidationService
    {
        bool IsInGroupCapacity(double groupMaxAmps, List<double> connectorsMaxCurrentAmps);
    }
    internal class ValidationService : IValidationService
    {
        public bool IsInGroupCapacity(double groupMaxAmps, List<double> connectorsMaxCurrentAmps)
        {
            throw new NotImplementedException();
        }
    }
}
