using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Controllers
{
    public class NoteController : ControllerBase
    {
        /* TODO: 
        * 1. ONGOING >>> I need to create some special token(room) which will have notebooks sharable
        * 2. DONE === If notebook exists do not allow to create new one
        * 3. DONE === Each note should be easily assigned to notebook without new notebook being created
        * 4. DONE === Each note should be easy to edit if not entering any values or leaving empty strings
        * 5. DONE === Throw bad requests instead of new errors
        * 6. Add AUTH based on token with expiration time ?
        * 7. DONE === Fix note.Done issue that is note.Done not provided it autofills as false even if value existed
        * 8. Try to init DTO's for notebook perhaps ? and others if possible
        * 9. Separate Notebook controllers and Room controllers from Notes controlles
        */

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

        [HttpGet("/api/GetNotebooks")]
        public ActionResult<IEnumerable<Notebook>> GetNotebooks()
        {
            return _dataContext.Notebooks.Include(x => x.Notes).ToList();
        }

        [HttpPost("/api/CreateNotebook")]
        public ActionResult CreateNotebook(Notebook notebook)
        {
            Notebook existingNotebook = _dataContext.Notebooks.FirstOrDefault(n => n.Id == notebook.Id);
            Notebook existingNotebookTitle = _dataContext.Notebooks.FirstOrDefault(n => n.Title == notebook.Title);
            if (existingNotebook == null)
            {
                if (notebook.Title == null)
                {
                    return BadRequest("Failed to create Notebook. Please enter Title!");
                }
                if (existingNotebookTitle != null)
                {
                    return BadRequest("Failed to create Notebook. Title already exists!");
                }
                else
                {
                    _dataContext.Notebooks.Add(notebook);
                    _dataContext.SaveChanges();
                }
            }
            else
            {
                return BadRequest("Failed to create Notebook. Notebook with same Id exists!");
            }
            return Ok("Succesfully created note with Id: " + notebook.Id + " and Title: " + notebook.Title);
        }

        [HttpPost("api/CreateRoom")]
        public ActionResult CreateRoom()
        {
            Room room = new Room();
            return Ok(room.UniqueKey);
        }

        [HttpPost("/api/CreateNote")]
        public ActionResult PostNotes(Note note)
        {
            Note existingNote = _dataContext.Notes.FirstOrDefault(n => n.Id == note.Id);
            var notebook = _dataContext.Notebooks.FindAsync(note.Notebook);

            if (existingNote == null)
            {
                if (notebook == null)
                {
                    return BadRequest("Notebook not found");
                }
                _dataContext.Notes.Add(note);
                _dataContext.SaveChanges();
            }
            else
            {
                return BadRequest("Failed to create Note perhaps note with same ID exists ?");
            }
            return Ok("Succesfully created note with Id: " + note.Id + " and Title: " + note.Title);
        }

        [HttpPut("/api/EditNote")]
        public ActionResult PutNotes(Note note)
        {


            Note existingNote = _dataContext.Notes.FirstOrDefault(n => n.Id == note.Id);

            if (existingNote != null)
            {
                if (!string.IsNullOrWhiteSpace(note.Title))
                {
                    existingNote.Title = note.Title;
                }
                else if(string.IsNullOrEmpty(note.Title)) {
                    Console.WriteLine("Ignored empty title");
                }
                else
                {
                    return BadRequest("Please enter valid value in Title!");
                }
                if (!string.IsNullOrWhiteSpace(note.Description))
                {
                    existingNote.Description = note.Description;
                }
                else if(string.IsNullOrEmpty(note.Description)) {
                    Console.WriteLine("Ignored empty description");
                }
                else
                {
                    return BadRequest("Please enter valid value in Description!");
                }
                Console.WriteLine("Done status : " + note.Done);
                if (note.Done != null)
                {
                    existingNote.Done = note.Done;
                }
                if (_dataContext.SaveChanges() > 0)
                {
                    return Ok("Successfully edited note with Id " + note.Id);

                }
                else
                {
                    return Ok("There were no changes!");
                }
               
            }
            else
            {
                return BadRequest("Note with this Id not found");
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
