using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GreenFlux.SmartCharging.Application.Connectors.Models;

namespace GreenFlux.SmartCharging.Application.ChargeStations.Models
{
    public class CreateChargeStationModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int GroupId { get; set; }
        [Required]
        public string Name { get; set; }

        public int MaxConnectors { get; set; }
        public List<CreateConnectorModel> Connectors { get; set; } = new();
    }
}
