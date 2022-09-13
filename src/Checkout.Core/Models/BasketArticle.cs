using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout.Core.Models
{
    public record BasketArticle : Entity
    {
        [Required]
        [JsonPropertyName("item")]
        public string Item { get; set; }
        [Required]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonIgnore]
        [ForeignKey("Basket")]
        public int BasketId { get; set; }
    }
}
