using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SupportWebApp.Models;

public class SupportMessage
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Navn er påkrævet")]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email er påkrævet")]
    [EmailAddress(ErrorMessage = "Indtast en gyldig email")]
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefonnummer er påkrævet")]
    [JsonProperty(PropertyName = "phone")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Beskrivelse er påkrævet")]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kategori er påkrævet")]
    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "dateTime")]
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
}