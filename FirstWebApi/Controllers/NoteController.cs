using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    public class NoteController : ControllerBase
    {
        /* TODO: i think i need to create some special token which will have notes assigned, like (room)
        * and once people join that room or link with token they will see the notes that others created
        * in that room, so like shared notebook
        */

        // Another thing to add is AUTH

        // Testing commits

        private readonly DataContext _dataContext;
        public NoteController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("/api/GetNotes")]
        public ActionResult<IEnumerable<Note>> GetNotes()
        {
            return _dataContext.Notes.ToList();

        }

        [HttpPost("/api/AddNote")]
        public ActionResult PostNotes(Note note)
        {
            Note existingNote = _dataContext.Notes.FirstOrDefault(n => n.Id == note.Id);

            if (existingNote == null)
            {
                _dataContext.Notes.Add(note);
                _dataContext.SaveChanges();
            }
            else
            {
                throw new Exception("Failed to create Note perhaps note with same ID exists ?");
            }
            return Ok("Succesfully created note with Id: " + note.Id + " and Title: " + note.Title);
        }

        [HttpPut("/api/EditNote")]
        public ActionResult PutNotes(Note note)
        {
            Console.WriteLine(note.Title);
            Console.WriteLine(note.Description);

            Note existingNote = _dataContext.Notes.FirstOrDefault(n => n.Id == note.Id);
            if (existingNote != null)
            {
                if (note.Title != null || note.Title != "" )
                {// TODO issue here with logic, need to handle null values if you use blank spaces 
                    existingNote.Title = note.Title;
                }
                else 
                {
                    return BadRequest("Please enter valid value in Title!");
                }
                if (note.Description != "" || note.Description != null)
                {// TODO issue here with logic, need to handle null values if you use blank spaces
                    existingNote.Description = note.Description;
                }
                else
                {
                    return BadRequest("Please enter valid value in Description!");
                }
                if (note.Done != existingNote.Done)
                {
                    existingNote.Done = note.Done;
                }
                _dataContext.SaveChanges();
                return Ok("Successfully edited note with Id " + note.Id);
            }
            else
            {
                throw new Exception("Note with this Id not found");
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
                throw new Exception("Could not find Note to Delete!");
            }
        }
    }
}
