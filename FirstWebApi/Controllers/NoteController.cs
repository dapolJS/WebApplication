using FirstWebApi.DTOs;
using FirstWebApi.Models;
using FirstWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    // [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly NotesService _notesService;
        private readonly DataContextEF _dataContextEf;

        public NoteController(DataContextEF dataContextEf, NotesService notesService)
        {
            _notesService = notesService;
            _dataContextEf = dataContextEf;
        }

        [HttpGet("/api/GetNotes")]
        public ActionResult<Note> GetNotes()
        {
            var notes = _dataContextEf.Note.ToList();
            return Ok(notes);
        }

        [HttpPost("/api/CreateNote")]
        public async Task<ActionResult<Note>> PostNotes([FromBody] NoteDTO noteDto)
        {
            try
            {
                var createNote = await _notesService.CreateNote(noteDto);
                return Ok(createNote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/EditNote/{id:int}")]
        public async Task<ActionResult<Note>> PutNotes(int id, [FromBody] NoteDTO noteDto)
        {
            try
            {
                var updatedNote = await _notesService.UpdateNoteAsync(id, noteDto);
                return Ok(updatedNote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/DeleteNote/{id}")] // Delete all notes if id = 0 is not provided
        public ActionResult DeleteNote(int id)
        {
            Note existingNote = _dataContextEf.Note.FirstOrDefault(n => n.Id == id);
            IEnumerable<Note> allNotes = _dataContextEf.Note.Where(x => x != null);

            if (existingNote != null) // Deletes note by id
            {
                _dataContextEf.Note.Remove(existingNote);
                _dataContextEf.SaveChanges();
                return Ok($"Successfully deleted Note id : {existingNote.Id}");
            }
            else if (id == 0) // Deletes all notes if id is 0
            {
                int notesCount = allNotes.Count();
                _dataContextEf.Note.RemoveRange(allNotes);
                _dataContextEf.SaveChanges();
                return Ok($"Successfully DELETED all : {notesCount} Notes!");
            }
            else
            {
                return NotFound($"Note by id : {id} not found!");
            }
        }
    }
}