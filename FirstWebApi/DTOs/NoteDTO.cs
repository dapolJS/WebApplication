namespace FirstWebApi.DTOs
{
    public class NoteDTO
    {
        public required string Content { get; init; } = "";
        public required bool Done { get; init; }
    }
}
