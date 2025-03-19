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
        private readonly DataContextEF _dataContextEF;
        public NotebookController(DataContextEF dataContextEF, NotebooksService notebooksService)
        {

            _notebooksService = notebooksService;
            _dataContextEF = dataContextEF;
        }

        [HttpGet("/api/Notebook/{Id}/{Title}")]
        public ActionResult<Notebook> Notebook(int Id = 0, string Title = "None")
        {
            if (Id != 0)
            {
                var notebook = _dataContextEF.Notebook.FirstOrDefault(x => x.Id == Id);
                if (notebook == null)
                {
                    return NotFound("Id is not found!");
                }
                return Ok(notebook);
            }
            else if (Title != "None")
            {
                var notebook = _dataContextEF.Notebook.Include(x => x.Notes).Where(x => x.Title.ToLower().Contains(Title.ToLower()));
                if (!notebook.Any())
                {
                    return NotFound("Title not found!");
                }
                return Ok(notebook);
            }
            else
            {
                var notebooks = _dataContextEF.Notebook.Include(x => x.Notes).ToList();
                return Ok(notebooks);
            }
        }

        [HttpPost("/api/CreateNotebook")]
        public async Task<IActionResult> CreateNotebook([FromBody] NotebookDTO notebookDTO)
        {

            try
            {
                var createNotebook = await _notebooksService.CreateNotebook(notebookDTO);
                return Ok(createNotebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("/api/DeleteNotebook/{Id}")]
        public ActionResult DeleteNotebook(int Id)
        {
            Notebook existingNotebook = _dataContextEF.Notebook.FirstOrDefault(x => x.Id == Id);
            IEnumerable<Notebook> allNotebooks = _dataContextEF.Notebook.Where(x => x != null);

            if (existingNotebook != null) // Deletes notebook by Id
            {
                _dataContextEF.Notebook.Remove(existingNotebook);
                _dataContextEF.SaveChanges();
                return Ok($"Succesfully deleted Notebook \n Title : {existingNotebook.Title} \n Id : {existingNotebook.Id}");
            }
            else if (Id == 0) // Deletes all notebooks if Id is 0
            {
                int notebooksCount = allNotebooks.Count();
                _dataContextEF.Notebook.RemoveRange(allNotebooks);
                _dataContextEF.SaveChanges();
                return Ok($"Sucessfully DELETED all : {notebooksCount} Notebooks!");
            }
            else
            {
                return NotFound($"Notebook by Id : {Id} not found!");
            }
        }
    }
}
