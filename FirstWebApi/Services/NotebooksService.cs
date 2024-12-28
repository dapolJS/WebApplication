using FirstWebApi.DTOs;
using FirstWebApi.Models;

namespace FirstWebApi.Services
{
    public class NotebooksService
    {
        private readonly DataContextEF _dataContextEF;
        public NotebooksService(DataContextEF dataContextEF)
        {
            _dataContextEF = dataContextEF;
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

            await _dataContextEF.Notebook.AddAsync(notebook);

            if (_dataContextEF.SaveChanges() > 0)
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