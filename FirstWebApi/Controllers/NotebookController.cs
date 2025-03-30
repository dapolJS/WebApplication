using FirstWebApi.DTOs;
using FirstWebApi.Models;
using FirstWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Controllers
{
    [Authorize]
    public class NotebookController : ControllerBase
    {
        private readonly NotebooksService _notebooksService;
        private readonly DataContextEF _dataContextEf;

        public NotebookController(DataContextEF dataContextEf, NotebooksService notebooksService)
        {
            _notebooksService = notebooksService;
            _dataContextEf = dataContextEf;
        }

        [HttpGet("/api/Notebook/{id}/{Title}")]
        public ActionResult<Notebook> Notebook(int id = 0, string title = "None")
        {
            if (id != 0)
            {
                var notebook = _dataContextEf.Notebook.FirstOrDefault(x => x.Id == id);
                if (notebook == null)
                {
                    return NotFound("id is not found!");
                }

                return Ok(notebook);
            }
            else if (title != "None")
            {
                var notebook = _dataContextEf.Notebook.Include(x => x.Notes)
                    .Where(x => x.Title.ToLower().Contains(title.ToLower()));
                if (!notebook.Any())
                {
                    return NotFound("Title not found!");
                }

                return Ok(notebook);
            }
            else
            {
                var notebooks = _dataContextEf.Notebook.Include(x => x.Notes).ToList();
                return Ok(notebooks);
            }
        }

        [HttpPost("/api/CreateNotebook")]
        public async Task<IActionResult> CreateNotebook([FromBody] NotebookDTO notebookDto)
        {
            try
            {
                var createNotebook = await _notebooksService.CreateNotebook(notebookDto);
                return Ok(createNotebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/DeleteNotebook/{id}")]
        public ActionResult DeleteNotebook(int id)
        {
            Notebook existingNotebook = _dataContextEf.Notebook.FirstOrDefault(x => x.Id == id);
            IEnumerable<Notebook> allNotebooks = _dataContextEf.Notebook.Where(x => x != null);

            if (existingNotebook != null) // Deletes notebook by id
            {
                _dataContextEf.Notebook.Remove(existingNotebook);
                _dataContextEf.SaveChanges();
                return Ok(
                    $"Successfully deleted Notebook \n Title : {existingNotebook.Title} \n id : {existingNotebook.Id}");
            }
            else if (id == 0) // Deletes all notebooks if id is 0
            {
                int notebooksCount = allNotebooks.Count();
                _dataContextEf.Notebook.RemoveRange(allNotebooks);
                _dataContextEf.SaveChanges();
                return Ok($"Successfully DELETED all : {notebooksCount} Notebooks!");
            }
            else
            {
                return NotFound($"Notebook by id : {id} not found!");
            }
        }
    }
}