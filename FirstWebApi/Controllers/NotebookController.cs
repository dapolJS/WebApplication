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
                var createNotebook = _notebooksService.CreateNotebook(notebookDTO);
                return Ok(createNotebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //Notebook existingNotebook = _dataContext.Notebooks.FirstOrDefault(n => n.Id == notebook.Id);
            //Notebook existingNotebookTitle = _dataContext.Notebooks.FirstOrDefault(n => n.Title == notebook.Title);
            //if (existingNotebook == null)
            //{
            //    if (notebook.Title == null)
            //    {
            //        return BadRequest("Failed to create Notebook. Please enter Title!");
            //    }
            //    if (existingNotebookTitle != null)
            //    {
            //        return BadRequest("Failed to create Notebook. Title already exists!");
            //    }
            //    else
            //    {
            //        _dataContext.Entry(notebook).Reference(x => x.Title);
            //        _dataContext.Entry(notebook).Reference(x => x.Title);
            //        _dataContext.SaveChanges();
            //    }
            //}
            //else
            //{
            //    return BadRequest("Failed to create Notebook. Notebook with same Id exists!");
            //}
            //return Ok("Succesfully created note with Id: " + notebook.Id + " and Title: " + notebook.Title);
        }
    }
}
