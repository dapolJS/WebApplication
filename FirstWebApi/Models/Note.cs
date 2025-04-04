﻿using System.Text.Json.Serialization;

namespace FirstWebApi.Models;

public class Note
{
    public int Id { get; init; }
    public int? NotebookId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool? Done { get; set; }
    public DateTime? Date { get; init; }

    [JsonIgnore]
    public Notebook? Notebook { get; init; } // Navigation property

    public Note()
    {
        NotebookId = 0;
    }
}
