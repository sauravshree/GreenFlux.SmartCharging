using System.ComponentModel.DataAnnotations;

namespace GreenFlux.SmartCharging.Domain.Entities
{
    public class Connector : EntityBase
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ChargeStationId { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double MaxCurrentAmps { get; set; }
    }
}
