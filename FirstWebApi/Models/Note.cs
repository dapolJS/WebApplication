using System.ComponentModel.DataAnnotations;

namespace FirstWebApi.Models;

public class Note
{
    public int Id { get; init; }
    [MaxLength(200)]
    public required string Content { get; set; } = "";
    public required bool Done { get; set; }
    public DateTime? Date { get; init; }
    
}
