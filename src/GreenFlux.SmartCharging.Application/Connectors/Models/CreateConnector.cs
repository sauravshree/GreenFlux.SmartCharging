using System.ComponentModel.DataAnnotations;

namespace GreenFlux.SmartCharging.Application.Connectors.Models
{
    public class CreateConnector
    {
        public int? ChargeStationId { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double MaxCurrentAmps { get; set; }
    }
}
