using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenFlux.SmartCharging.Application.Services
{

    public static class ValidationService
    {
        public static bool IsInGroupCapacity(double groupMaxAmps, List<double> connectorsMaxCurrentAmps)
        {
            return groupMaxAmps >= connectorsMaxCurrentAmps.Sum();
        }
    }
}
