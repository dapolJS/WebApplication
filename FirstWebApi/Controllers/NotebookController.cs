using FirstWebApi.DTOs;
using FirstWebApi.Models;
using FirstWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Controllers
{
    public class NotebookController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly NotebooksService _notebooksService;
        public NotebookController(DataContext dataContext, NotebooksService notebooksService)
        {

            _dataContext = dataContext;
            _notebooksService = notebooksService;
        }

        [HttpGet("/api/Notebook/{Id}/{Title}")]
        public ActionResult<Notebook> Notebook(int Id = 0, string Title = "None")
        {
            if (Id != 0)
            {
                var notebook = _dataContext.Notebooks.FirstOrDefault(x => x.Id == Id);
                if (notebook == null)
                {
                    return NotFound("Id is not found!");
                }
                return Ok(notebook);
            }
            else if (Title != "None")
            {
                var notebook = _dataContext.Notebooks.Where(x => x.Title.ToLower().Contains(Title.ToLower()));
                if (!notebook.Any())
                {
                    return NotFound("Title not found!");
                }
                return Ok(notebook);
            }
            else
            {
                var notebooks = _dataContext.Notebooks.Include(x => x.Notes).ToList();
                return Ok(notebooks);
            }
        }

        [HttpPost("/api/CreateNotebook")]
        public async Task<IActionResult> CreateNotebook(NotebookDTO notebookDTO)
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
    }
}
