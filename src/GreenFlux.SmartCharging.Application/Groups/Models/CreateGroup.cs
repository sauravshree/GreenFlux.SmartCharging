using System.ComponentModel.DataAnnotations;

namespace GreenFlux.SmartCharging.Application.Groups.Models
{
    public class CreateGroup
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double CapacityAmps { get; set; }
    }
}
