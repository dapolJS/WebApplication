using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    public class NoteController : ControllerBase
    {
        /* TODO: i think i need to create some special token which will have notes assigned, like (room)
        * and once people join that room or link with token they will see the notes that others created
        * in that room, so like shared notebook
        * 2. If notebook exists do not allow to create new one
        * 3. Each note should be easily assigned to notebook without new notebook being created
        * 4. Each note should be easy to edit if not entering any values or leaving empty strings
        * 5. Throw bad requests instead of new errors
        * 6. Add AUTH based on token with expiration time ?
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
            return _dataContext.Notebooks.ToList();
        }

        [HttpPost("/api/CreateNotebook")]
        public ActionResult CreateNotebook(Notebook notebook)
        {
            Notebook existingNotebook = _dataContext.Notebooks.FirstOrDefault(n => n.Id == notebook.Id);
            Notebook existingNotebookTitle = _dataContext.Notebooks.FirstOrDefault(n => n.NotebookTitle == notebook.NotebookTitle);
            if (existingNotebook == null)
            {
                if (notebook.NotebookTitle == null)
                {
                    throw new Exception("Failed to create Notebook. Please enter Title!");
                }
                if(existingNotebookTitle != null) 
                {
                    throw new Exception("Failed to create Notebook. Title already exists!");
                }
                else 
                {
                    _dataContext.Notebooks.Add(notebook);
                    _dataContext.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Failed to create Notebook. Notebook with same Id exists!");
            }
            return Ok("Succesfully created note with Id: " + notebook.Id + " and Title: " + notebook.NotebookTitle);
        }

        [HttpPost("/api/CreateNote")] 
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


            Note existingNote = _dataContext.Notes.FirstOrDefault(n => n.Id == note.Id);
            Console.WriteLine("existing note: ");
            Console.WriteLine(existingNote.Title);
            Console.WriteLine(existingNote.Description);
            if (existingNote != null)
            {
                Console.WriteLine("current note: : ");
                Console.WriteLine(note.Title);
                Console.WriteLine(note.Description);
                if (note.Title != null)
                {
                    existingNote.Title = note.Title;
                }
                else 
                {
                    return BadRequest("Please enter valid value in Title!");
                }
                if (note.Description != null)
                {
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
