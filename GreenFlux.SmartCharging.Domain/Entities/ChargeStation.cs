using System.Collections.Generic;

namespace GreenFlux.SmartCharging.Domain.Entities
{
    public class ChargeStation : EntityBase
    {
        public string Name { get; set; }
        public List<Connector> Connectors { get; set; }
    }
}
