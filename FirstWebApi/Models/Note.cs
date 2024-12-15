namespace FirstWebApi.Models
{
    public class Note
    {
        public int Id { get; set; }
        /* when adding notebook.title while creating new note also what happens is 
         * new notebook is being created ??
        */
        public Notebook? Notebook { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public bool Done { get; set; }
    }
}
