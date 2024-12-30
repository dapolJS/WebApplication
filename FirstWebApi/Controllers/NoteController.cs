using FirstWebApi.DTOs;
using FirstWebApi.Models;
using FirstWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    public class NoteController : ControllerBase
    {

        private readonly NotesService _notesService;
        private readonly DataContextEF _dataContextEF;
        public NoteController(DataContextEF dataContextEF, NotesService notesService)
        {
            _notesService = notesService;
            _dataContextEF = dataContextEF;
        }

        [HttpGet("/api/GetNotes/{Id}/{Title}")]
        public ActionResult<Note> GetNotes(int Id = 0, string Title = "None")
        {
            if (Id != 0)
            {
                var note = _dataContextEF.Note.FirstOrDefault(x => x.Id == Id);
                if (note == null)
                {
                    return NotFound("Note by Id not found!");
                }
                return Ok(note);
            }
            else if (Title != "None")
            {
                var note = _dataContextEF.Note.Where(x => x.Title.ToLower().Contains(Title.ToLower()));
                if (!note.Any())
                {
                    return NotFound("Note by Title not found!");
                }
                return Ok(note);
            }
            else
            {
                var notes = _dataContextEF.Note.ToList();
                return Ok(notes);
            }
        }

        [HttpPost("/api/CreateNote")]
        public ActionResult<Note> PostNotes([FromBody] NoteDTO noteDTO)
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

        [HttpPut("/api/EditNote/{Id}")]
        public async Task<ActionResult<Note>> PutNotes(int Id, [FromBody] NoteDTO noteDTO)
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

        [HttpDelete("/api/DeleteNote/{Id}")] // Delete all notes if Id = 0 is not provided
        public ActionResult DeleteNote(int Id)
        {
            Note existingNote = _dataContextEF.Note.FirstOrDefault(n => n.Id == Id);
            IEnumerable<Note> allNotes = _dataContextEF.Note.Where(x => x != null);

            if (existingNote != null) // Deletes note by Id
            {
                _dataContextEF.Note.Remove(existingNote);
                _dataContextEF.SaveChanges();
                return Ok($"Succesfully deleted Note \n Title : {existingNote.Title} \n Id : {existingNote.Id}");
            }
            else if (Id == 0) // Deletes all notes if Id is 0
            {
                int notesCount = allNotes.Count();
                _dataContextEF.Note.RemoveRange(allNotes);
                _dataContextEF.SaveChanges();
                return Ok($"Succesfully DELETED all : {notesCount} Notes!");
            }
            else
            {
                return NotFound($"Note by Id : {Id} not found!");
            }
        }
    }
}
