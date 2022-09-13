using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout.Core.Models
{
    public record Entity
    {
        [Key]
        [JsonPropertyName("id")]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
    }
}
