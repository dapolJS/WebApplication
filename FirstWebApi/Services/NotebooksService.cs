using FirstWebApi.DTOs;
using FirstWebApi.Models;

namespace FirstWebApi.Services
{
    public class NotebooksService
    {
        private readonly DataContext _dataContext;
        public NotebooksService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Notebook> CreateNotebook(NotebookDTO notebookDTO)
        {

            if (string.IsNullOrWhiteSpace(notebookDTO.Title))
            {
                throw new Exception("Failed to create Notebook. Please enter Title!");
            }

            Notebook notebook = new Notebook
            {
                Title = notebookDTO.Title,
                UniqueKey = notebookDTO.UniqueKey
            };

            await _dataContext.Notebooks.AddAsync(notebook);

            if (_dataContext.SaveChanges() > 0)
            {
                return notebook;
            }
            else
            {
                throw new Exception("There were no changes!");
            }

        }
    }
}