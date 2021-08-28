using System.ComponentModel.DataAnnotations;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Models
{
    public class UpdateChargeStation
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int GroupId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxConnectors { get; set; }
    }
}
