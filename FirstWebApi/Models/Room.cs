namespace FirstWebApi.Models;

public class Room
{
    public int Id { get; init; }
    public string UniqueKey { get; init; }
    public ICollection<Notebook> Notebooks { get; init; }

    public Room()
    {
        UniqueKey = Guid.NewGuid().ToString();
        Notebooks = new List<Notebook>(); // Initialize the Notebooks collection
    }
}
