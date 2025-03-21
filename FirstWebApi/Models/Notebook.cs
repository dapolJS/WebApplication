using System.Text.Json.Serialization;

namespace FirstWebApi.Models;

public class Notebook
{
    public int Id { get; init; }
    public string Title { get; set; } = "";
    public ICollection<Note>? Notes { get; init; }
    public string UniqueKey { get; set; } = "";

    [JsonIgnore]
    public Room? Room { get; init; }
}
