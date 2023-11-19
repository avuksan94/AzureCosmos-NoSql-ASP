using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PPPK_Zadatak05.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isCompleted")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "personId")]
        public string PersonId { get; set; }
    }
}
