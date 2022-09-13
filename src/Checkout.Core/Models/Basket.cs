using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Checkout.Core.Models
{
    public record Basket : Entity
    {
        [JsonPropertyName("VAT")]
        public decimal VAT { get;  set; }
        [JsonPropertyName("totalNet")]
        public decimal TotalNet { get; set; }
        [JsonPropertyName("totalGross")]
        public decimal TotalGross { get; set; }
        [Required]
        [JsonPropertyName("customer")]
        public string Customer { get; set; }
        [Required]
        [JsonPropertyName("paysVat")]
        public bool PaysVat { get; set; }

        [NotMapped]
        public IEnumerable<BasketArticle> Articles { get; set; }
        public Basket()
        {
            //hardcoded VAT. Not recommended, I know :))
            VAT = 10;
        }


    }
}