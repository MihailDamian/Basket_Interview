using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout.Core.Models
{
    public record BasketDTO : Entity
    {
        [Required]
        [JsonPropertyName("customer")]
        public string Customer { get; set; }
        [Required]
        [JsonPropertyName("paysVat")]
        public bool PaysVat { get; set; }
    }
}
