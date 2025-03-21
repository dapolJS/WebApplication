﻿using FirstWebApi.DTOs;
using FirstWebApi.Models;

namespace FirstWebApi.Services
{
    public class NotesService
    {
        private readonly DataContextEF _dataContextEF;

        public NotesService() { }

        public NotesService(DataContextEF dataContextEF)
        {
            _dataContextEF = dataContextEF;
        }

        public async Task<Note> UpdateNoteAsync(int id, NoteDTO noteDTO)
        {
            var note = await _dataContextEF.Note.FindAsync(id);

            if (note == null)
            {
                throw new Exception("Note not found!");
            }

            if (note != null)
            {
                if (!string.IsNullOrWhiteSpace(noteDTO.Title)) //TODO: refactor to guard
                {
                    note.Title = noteDTO.Title;
                }
                else if (string.IsNullOrEmpty(noteDTO.Title) && noteDTO.Title != "string")
                {
                    Console.WriteLine("Ignored empty title");
                }
                else
                {
                    throw new ArgumentException("Please enter valid value in Title!");
                }
                if (
                    !string.IsNullOrWhiteSpace(noteDTO.Description)
                    && noteDTO.Description != "string"
                )
                {
                    note.Description = noteDTO.Description;
                }
                else if (string.IsNullOrEmpty(noteDTO.Description))
                {
                    Console.WriteLine("Ignored empty description");
                }
                else
                {
                    throw new ArgumentException("Please enter valid value in Description!");
                }
                if (noteDTO.NotebookId != 0)
                {
                    note.NotebookId = noteDTO.NotebookId;
                }
                else if (noteDTO.NotebookId == 0)
                {
                    Console.WriteLine("Ignored empty title");
                }
                else
                {
                    throw new ArgumentException("Please enter valid value in NotebookId!");
                }
                if (noteDTO.Done != null)
                {
                    note.Done = noteDTO.Done;
                }
                if (_dataContextEF.SaveChanges() > 0)
                {
                    return note;
                }
                else
                {
                    throw new ArgumentException("There were no changes!");
                }
            }
            else
            {
                throw new ArgumentException("Note with this Id not found");
            }
        }

        public Note CreateNote(NoteDTO noteDTO)
        {
            Guard.IsNotNullOrString(noteDTO.Title);
            Guard.IsNotNullOrString(noteDTO.Description);

            Note note = new Note
            {
                NotebookId = noteDTO.NotebookId,
                Title = noteDTO.Title,
                Description = noteDTO.Description,
                Done = noteDTO.Done,
            };

            _dataContextEF.Note.AddAsync(note);

            if (_dataContextEF.SaveChanges() > 0)
            {
                return note;
            }
            else
            {
                throw new ArgumentException("There were no changes!");
            }
        }
    }
}
