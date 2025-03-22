using FirstWebApi.DTOs;
using FirstWebApi.Models;

namespace FirstWebApi.Services;

public class NotebooksService
{
    private readonly DataContextEF _dataContextEf;

    public NotebooksService(DataContextEF dataContextEf)
    {
        _dataContextEf = dataContextEf;
    }

    public async Task<Notebook> CreateNotebook(NotebookDTO notebookDto)
    {
        Guard.IsNotNull(notebookDto.Title);

        Notebook notebook = new Notebook
        {
            Title = notebookDto.Title,
            UniqueKey = notebookDto.UniqueKey
        };

        await _dataContextEf.Notebook.AddAsync(notebook);

        if (_dataContextEf.SaveChanges() > 0)
        {
            return notebook;
        }
        else
        {
            throw new Exception("There were no changes!");
        }
    }
}
