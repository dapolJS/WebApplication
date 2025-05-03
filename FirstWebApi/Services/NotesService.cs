using FirstWebApi.DTOs;
using FirstWebApi.Models;

namespace FirstWebApi.Services
{
    public class NotesService
    {
        private readonly DataContextEF _dataContextEf;

        public NotesService()
        {
        }

        public NotesService(DataContextEF dataContextEf)
        {
            _dataContextEf = dataContextEf;
        }

        public async Task<Note> UpdateNoteAsync(int id, NoteDTO noteDto)
        {
            var note = await _dataContextEf.Note.FindAsync(id);

            if (note == null)
            {
                throw new Exception("Note not found!");
            }

            if (note != null)
            {
                if (!string.IsNullOrWhiteSpace(noteDto.Content)) //TODO: refactor to guard
                {
                    note.Content = noteDto.Content;
                }
                else if (string.IsNullOrEmpty(noteDto.Content) && noteDto.Content != "string")
                {
                    Console.WriteLine("Ignored empty Content");
                }
                else
                {
                    throw new ArgumentException("Please enter text in Content!");
                }

                if (noteDto.Done != null)
                {
                    note.Done = noteDto.Done;
                }

                if (_dataContextEf.SaveChanges() > 0)
                {
                    return note;
                }

                throw new ArgumentException("There were no changes!");
            }

            throw new ArgumentException("Note with this Id not found");
        }

        public async Task<Note> CreateNote(NoteDTO noteDto)
        {
            var note = new Note
            {
                Content = noteDto.Content,
                Purpose = "To do",
                Owner = "Dani",
                Done = noteDto.Done,
            };

            await _dataContextEf.Note.AddAsync(note);

            if (await _dataContextEf.SaveChangesAsync() > 0)
            {
                return note;
            }
            throw new ArgumentException("There were no changes!");
        }
    }
}