﻿using FirstWebApi.DTOs;
using FirstWebApi.Models;

namespace FirstWebApi.Services
{
    public class NotesService
    {
        private readonly DataContext _dataContext;

        public NotesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Note> UpdateNoteAsync(int id, NoteDTO noteDTO)
        {
            var note = await _dataContext.Notes.FindAsync(id);

            if (note == null)
            {
                throw new Exception("Note not found!");
            }

            //// Manually updating models properties using DTO
            if (note != null)
            {
                if (!string.IsNullOrWhiteSpace(noteDTO.Title))
                {
                    note.Title = noteDTO.Title;
                }
                else if (string.IsNullOrEmpty(noteDTO.Title))
                {
                    Console.WriteLine("Ignored empty title");
                }
                else
                {
                    throw new Exception("Please enter valid value in Title!");
                }
                if (!string.IsNullOrWhiteSpace(noteDTO.Description))
                {
                    note.Description = noteDTO.Description;
                }
                else if (string.IsNullOrEmpty(noteDTO.Description))
                {
                    Console.WriteLine("Ignored empty description");
                }
                else
                {
                    throw new Exception("Please enter valid value in Description!");
                }
                if (noteDTO.Done != null)
                {
                    note.Done = noteDTO.Done;
                }
                if (_dataContext.SaveChanges() > 0)
                {
                    return note;

                }
                else
                {
                    throw new Exception("There were no changes!");
                }

            }
            else
            {
                throw new Exception("Note with this Id not found");
            }
        }
    }
}