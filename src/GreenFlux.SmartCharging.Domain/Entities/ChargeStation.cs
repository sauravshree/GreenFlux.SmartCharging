using System.Collections.Generic;

namespace GreenFlux.SmartCharging.Domain.Entities
{
    public class ChargeStation : EntityBase
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int MaxConnectors { get; set; }
        public List<Connector> Connectors { get; set; }
    }
}
