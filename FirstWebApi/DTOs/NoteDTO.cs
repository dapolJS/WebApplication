using System.Text.Json.Serialization;

namespace FirstWebApi.DTOs
{
    public class NoteDTO
    {
        public int NotebookId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public bool? Done { get; set; }
    }
}
