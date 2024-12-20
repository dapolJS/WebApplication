using FirstWebApi.Models;
using System.Text.Json.Serialization;

namespace FirstWebApi.DTOs
{
    public class NotebookDTO
    {
        public string Title { get; set; } = "";
        public string UniqueKey { get; set; }
    }
}
