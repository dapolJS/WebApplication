namespace FirstWebApi.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string UniqueKey { get; set; }
        public ICollection<Notebook> Notebooks { get; set; }

        public Room()
        {
            UniqueKey = Guid.NewGuid().ToString();
            Notebooks = new List<Notebook>();  // Initialize the Notebooks collection
        }

    }
}
