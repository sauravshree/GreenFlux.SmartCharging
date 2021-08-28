namespace GreenFlux.SmartCharging.Application.ChargeStations.Models
{
    public class ViewChargeStation
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int MaxConnectors { get; set; }
    }
}
