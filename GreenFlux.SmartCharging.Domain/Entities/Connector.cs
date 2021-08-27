namespace GreenFlux.SmartCharging.Domain.Entities
{
    public class Connector : EntityBase
    {
        public int ChargeStationId { get; set; }
        public double MaxCurrentAmps { get; set; }
    }
}
