using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GreenFlux.SmartCharging.Application.Connectors.Models;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Models
{
    public class CreateChargeStation
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int GroupId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxConnectors { get; set; }
        public List<CreateConnector> Connectors { get; set; } = new();
    }
}
