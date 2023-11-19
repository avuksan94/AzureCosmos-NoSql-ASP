using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PPPK_Zadatak05.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

    }
}
