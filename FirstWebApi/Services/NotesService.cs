using FirstWebApi.DTOs;
using FirstWebApi.Models;

namespace FirstWebApi.Services
{
    public class NotesService
    {
        private readonly DataContext _dataContext;

        public NotesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Note> UpdateNoteAsync(int id, NoteDTO noteDTO)
        {
            var note = await _dataContext.Notes.FindAsync(id);

            if (note == null)
            {
                throw new Exception("Note not found!");
            }

            //// Manually updating models properties using DTO
            if (note != null)
            {
                if (!string.IsNullOrWhiteSpace(noteDTO.Title))
                {
                    note.Title = noteDTO.Title;
                }
                else if (string.IsNullOrEmpty(noteDTO.Title))
                {
                    Console.WriteLine("Ignored empty title");
                }
                else
                {
                    throw new Exception("Please enter valid value in Title!");
                }
                if (!string.IsNullOrWhiteSpace(noteDTO.Description))
                {
                    note.Description = noteDTO.Description;
                }
                else if (string.IsNullOrEmpty(noteDTO.Description))
                {
                    Console.WriteLine("Ignored empty description");
                }
                else
                {
                    throw new Exception("Please enter valid value in Description!");
                }
                if (noteDTO.NotebookId != 0)
                {
                    note.NotebookId = noteDTO.NotebookId;
                }
                else if (noteDTO.NotebookId == 0)
                {
                    Console.WriteLine("Ignored empty title");
                }
                else
                {
                    throw new Exception("Please enter valid value in NotebookId!");
                }
                if (noteDTO.Done != null)
                {
                    note.Done = noteDTO.Done;
                }
                if (_dataContext.SaveChanges() > 0)
                {
                    return note;

                }
                else
                {
                    throw new Exception("There were no changes!");
                }

            }
            else
            {
                throw new Exception("Note with this Id not found");
            }
        }

        public async Task<Note> CreateNote(NoteDTO noteDTO)
        {
            if (string.IsNullOrWhiteSpace(noteDTO.Title))
            {
                throw new Exception("Please enter valid value in Title!");
            }

            if (string.IsNullOrWhiteSpace(noteDTO.Description))
            {
                throw new Exception("Please enter valid value in Description!");
            }

            Note note = new Note
            {
                NotebookId = noteDTO.NotebookId,
                Title = noteDTO.Title,
                Description = noteDTO.Description,
                Done = noteDTO.Done,
            };

            await _dataContext.Notes.AddAsync(note);

            if (_dataContext.SaveChanges() > 0)
            {
                return note;
            }
            else
            {
                throw new Exception("There were no changes!");
            }
        }
    }
}
