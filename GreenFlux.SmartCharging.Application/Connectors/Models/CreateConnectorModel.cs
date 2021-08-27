using System.ComponentModel.DataAnnotations;

namespace GreenFlux.SmartCharging.Application.Connectors.Models
{
    public class CreateConnectorModel
    {

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double CapacityAmps { get; set; }
    }
}
