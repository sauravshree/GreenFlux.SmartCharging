using System.Collections.Generic;

namespace GreenFlux.SmartCharging.Domain.Entities
{
    public class Group : EntityBase
    {
        public string Name { get; set; }
        public double CapacityAmps { get; set; }
        public List<ChargeStation> ChargeStations { get; set; }
    }
}
