using System;
using System.Collections.Generic;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Services
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
