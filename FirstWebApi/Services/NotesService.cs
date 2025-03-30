using FirstWebApi.DTOs;
using FirstWebApi.Models;

namespace FirstWebApi.Services
{
    public class NotesService
    {
        private readonly DataContextEF _dataContextEf;

        public NotesService() { }

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
                if (!string.IsNullOrWhiteSpace(noteDto.Title)) //TODO: refactor to guard
                {
                    note.Title = noteDto.Title;
                }
                else if (string.IsNullOrEmpty(noteDto.Title) && noteDto.Title != "string")
                {
                    Console.WriteLine("Ignored empty title");
                }
                else
                {
                    throw new ArgumentException("Please enter valid value in Title!");
                }
                if (
                    !string.IsNullOrWhiteSpace(noteDto.Description)
                    && noteDto.Description != "string"
                )
                {
                    note.Description = noteDto.Description;
                }
                else if (string.IsNullOrEmpty(noteDto.Description))
                {
                    Console.WriteLine("Ignored empty description");
                }
                else
                {
                    throw new ArgumentException("Please enter valid value in Description!");
                }
                if (noteDto.NotebookId != 0)
                {
                    note.NotebookId = noteDto.NotebookId;
                }
                else if (noteDto.NotebookId == 0)
                {
                    Console.WriteLine("Ignored empty title");
                }
                else
                {
                    throw new ArgumentException("Please enter valid value in NotebookId!");
                }
                if (noteDto.Done != null)
                {
                    note.Done = noteDto.Done;
                }
                if (_dataContextEf.SaveChanges() > 0)
                {
                    return note;
                }
                else
                {
                    throw new ArgumentException("There were no changes!");
                }
            }
            else
            {
                throw new ArgumentException("Note with this Id not found");
            }
        }

        public Note CreateNote(NoteDTO noteDto)
        {
            Guard.IsNotNullOrString(noteDto.Title);
            Guard.IsNotNullOrString(noteDto.Description);

            Note note = new Note
            {
                NotebookId = noteDto.NotebookId,
                Title = noteDto.Title,
                Description = noteDto.Description,
                Done = noteDto.Done,
            };

            _dataContextEf.Note.AddAsync(note);

            if (_dataContextEf.SaveChanges() > 0)
            {
                return note;
            }
            else
            {
                throw new ArgumentException("There were no changes!");
            }
        }
    }
}
