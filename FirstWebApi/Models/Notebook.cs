using System.Text.Json.Serialization;

namespace FirstWebApi.Models
{
    public class Notebook
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public ICollection<Note>? Notes { get; set; }
        public string UniqueKey { get; set; } = "";
        [JsonIgnore]
        public Room Room { get; set; }
    }
}
