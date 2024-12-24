using FirstWebApi.DTOs;
using FirstWebApi.Models;
using FirstWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    public class NoteController : ControllerBase
    {

        private readonly DataContext _dataContext;
        private readonly NotesService _notesService;
        public NoteController(DataContext dataContext, NotesService notesService)
        {
            _dataContext = dataContext;
            _notesService = notesService;
        }

        [HttpGet("/api/GetNotes/{Id}/{Title}")]
        public ActionResult<Note> GetNotes(int Id = 0, string Title = "None")
        {
            if (Id != 0)
            {
                var note = _dataContext.Notes.FirstOrDefault(x => x.Id == Id);
                if (note == null)
                {
                    return NotFound("Note by Id not found!");
                }
                return Ok(note);
            }
            else if (Title != "None")
            {
                var note = _dataContext.Notes.Where(x => x.Title.ToLower().Contains(Title.ToLower()));
                if (!note.Any())
                {
                    return NotFound("Note by Title not found!");
                }
                return Ok(note);
            }
            else
            {
                var notes = _dataContext.Notes.ToList();
                return Ok(notes);
            }
        }

        [HttpPost("/api/CreateNote")]
        public ActionResult<Note> PostNotes(NoteDTO noteDTO)
        {
            try
            {
                var createNote = _notesService.CreateNote(noteDTO);
                return Ok(createNote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/EditNote")]
        public async Task<ActionResult<Note>> PutNotes(int Id, NoteDTO noteDTO)
        {
            try
            {
                var updatedNote = await _notesService.UpdateNoteAsync(Id, noteDTO);
                return Ok(updatedNote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/DeleteNote/{Id}")]
        public ActionResult DeleteNote(int Id)
        {
            Note existingNote = _dataContext.Notes.FirstOrDefault(n => n.Id == Id);
            if (existingNote != null)
            {
                _dataContext.Notes.Remove(existingNote);
                _dataContext.SaveChanges();
                return Ok("Succesfully deleted note with ID: " + Id + " and Title: " + existingNote.Title);
            }
            else
            {
                return BadRequest("Could not find Note to Delete!");
            }
        }
    }
}
