using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace be.Models;

[Table("users")]
public class User
{
    public int Id { get; set; }

    [Column(TypeName = "text")]
    public required string Name { get; set; }

    [JsonIgnore]
    [Column(TypeName = "text")]
    public string? Password { get; set; }

    [Column(TypeName = "text")]
    public string? Email { get; set; }
}