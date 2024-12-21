using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FirstWebApi.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int? NotebookId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public bool? Done { get; set; }
        [JsonIgnore]
        public Notebook Notebook { get; set; } // Navigation property

        public Note()
        {
            NotebookId = 0;
        }
    }
}
