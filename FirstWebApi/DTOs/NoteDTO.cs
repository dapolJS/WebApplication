namespace FirstWebApi.DTOs
{
    public class NoteDTO
    {
        public int? NotebookId { get; init; }
        public string Title { get; init; } = "";
        public string Description { get; init; } = "";
        public bool? Done { get; init; }
    }
}
