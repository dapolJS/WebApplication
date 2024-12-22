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

        [HttpGet("/api/GetNotebooks")]
        public ActionResult<IEnumerable<Notebook>> GetNotebooks()
        {
            return _dataContext.Notebooks.Include(x => x.Notes).ToList();
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
