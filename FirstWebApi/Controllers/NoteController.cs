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

        [HttpGet("/api/GetNotes")]
        public ActionResult<IEnumerable<Note>> GetNotes()
        {
            return _dataContext.Notes.ToList();

        }

        [HttpPost("/api/CreateNote")]
        public async Task<IActionResult> PostNotes(NoteDTO noteDTO)
        {
            try
            {
                var createNote = await _notesService.CreateNote(noteDTO);
                return Ok(createNote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("/api/EditNote")]
        public async Task<IActionResult> PutNotes(int Id, NoteDTO noteDTO)
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

        [HttpDelete("/api/DeleteNote")]
        public ActionResult DeleteNote([FromBody] int id)
        {
            Note existingNote = _dataContext.Notes.FirstOrDefault(n => n.Id == id);
            if (existingNote != null)
            {
                _dataContext.Notes.Remove(existingNote);
                _dataContext.SaveChanges();
                return Ok("Succesfully deleted note with ID: " + id + " and Title: " + existingNote.Title);
            }
            else
            {
                return BadRequest("Could not find Note to Delete!");
            }
        }
    }
}
