﻿using System.Text;
using FirstWebApi.DTOs;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using MyApi.Tests.Auth;
using Newtonsoft.Json;

namespace MyApi.Tests.NotesTests
{
    public class NoteControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AuthenticationBearer _auth;

        public NoteControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _auth = new AuthenticationBearer(_client);
            new AuthenticationBearer(_client).RegisterAsync().GetAwaiter().GetResult();
        }

        [Fact(DisplayName = " =========== TC1 Returns list of existing notes")]
        public async Task GetNotesReturnsListOfNotes()
        {
            await _auth.AuthenticateAsync();
            // Act
            var response = await _client.GetAsync("/api/GetNotes/0/None"); // API endpoint to get notes

            // Assert
            response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

            var content = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<List<Note>>(content);

            Assert.NotNull(notes); // Verify the list is not null
            Assert.NotEmpty(notes); // Verify that the list is not empty
            Assert.True(notes.Count >= 8); // Check if there is more then 8 notes
        }

        [Fact(DisplayName = " =========== TC2 Create note with Title, Description, Done")]
        public async Task PostNotesReturnsCreatedNote()
        {
            await _auth.AuthenticateAsync();

            NoteDTO noteDto = new NoteDTO // New note object to be created
            {
                Title = "PostNotes",
                Description = "This is from Integration test",
                Done = false
            };
            var response = await _client.PostAsync(
                "/api/CreateNote",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

            // Assert
            Console.WriteLine(" ===> Response body : " + content);
            Note jsonContent = JsonConvert.DeserializeObject<Note>(content);
            Assert.NotEmpty(content);
            Assert.NotNull(jsonContent.Title);
            Assert.NotNull(jsonContent.Description);
        }

        [Fact(DisplayName = " =========== TC3 Create note with empty title")]
        public async Task PostNotesReturnsBadRequestWithoutTitle()
        {
            await _auth.AuthenticateAsync();

            NoteDTO noteDto = new NoteDTO
            {
                Title = "",
                Description = "From PostNotes test, Title not existing"
            };
            var response = await _client.PostAsync(
                "/api/CreateNote",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Console.WriteLine(" ===> Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Equal("Please enter valid value in noteDto.Title!", content);
        }

        [Fact(DisplayName = " =========== TC4 Create note with empty description")]
        public async Task PostNotesReturnBadResponseWithoutDescription()
        {
            await _auth.AuthenticateAsync();

            NoteDTO noteDto = new NoteDTO { Title = "PostNotesTitle", Description = "" };

            var response = await _client.PostAsync(
                "/api/CreateNote",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Equal("Please enter valid value in noteDto.Description!", content);
        }

        [Fact(DisplayName = " =========== TC5 Create note with existing notebook")]
        public async Task PostNotesCreateNoteWithExistingNotebook()
        {
            await _auth.AuthenticateAsync();

            NoteDTO noteDto = new NoteDTO
            {
                NotebookId = 2,
                Title = "Test Title",
                Description = "Test Description",
            };

            var response = await _client.PostAsync(
                "/api/CreateNote",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            Note jsonContent = JsonConvert.DeserializeObject<Note>(content);
            Console.WriteLine(
                " ===> PostNotesCreateNoteWithExistingNotebook Response body : " + content
            );
            Assert.NotEmpty(content);
            Assert.Equal(noteDto.NotebookId, jsonContent.NotebookId);
            Assert.Equal(noteDto.Title, jsonContent.Title);
            Assert.Equal(noteDto.Description, jsonContent.Description);
        }

        [Fact(DisplayName = " =========== TC6 Edit existing notes Title with same Title")]
        public async Task EditNoteWithSameTitle()
        {
            await _auth.AuthenticateAsync();

            int noteId = 7;

            Note noteDto = new Note { Title = "SeedTitle", };

            var response = await _client.PutAsync(
                $"/api/EditNote/{noteId}",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> EditNoteWithSameTitle Response body : " + content);

            Assert.NotEmpty(content);
            Assert.Equal("There were no changes!", content);
        }

        [Fact(DisplayName = " =========== TC7 Edit existing notes Title with new Title")]
        public async Task EditNoteWithNewTitle()
        {
            await _auth.AuthenticateAsync();

            Random random = new Random();

            // Generate a random integer between 0 (inclusive) and 100000 (exclusive)
            int randomNumber = random.Next(0, 100000);
            int noteId = 6;
            Note noteDto = new Note { Title = $"Test Edit Title{randomNumber}", };

            var response = await _client.PutAsync(
                $"/api/EditNote/{noteId}",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> EditNoteWithNewTitle Response body : " + content);
            Note jsonContent = JsonConvert.DeserializeObject<Note>(content);

            Assert.NotEmpty(content);
            Assert.Equal(noteDto.Title, jsonContent.Title);
        }

        [Fact(DisplayName = " =========== TC8 Edit existing notes Title with empty Title")]
        public async Task EditNoteWithEmptyTitle()
        {
            await _auth.AuthenticateAsync();

            int noteId = 6;
            Note noteDto = new Note { Title = "", };

            var response = await _client.PutAsync(
                $"/api/EditNote/{noteId}",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> EditNoteWithEmptyTitle Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Equal("There were no changes!", content);
        }

        [Fact(
            DisplayName = " =========== TC9 Edit existing notes Description with same Description"
        )]
        public async Task EditNoteWithSameDescription()
        {
            await _auth.AuthenticateAsync();

            int noteId = 7;

            Note noteDto = new Note { Description = "SeedDescription", };

            var response = await _client.PutAsync(
                $"/api/EditNote/{noteId}",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(content);
            Assert.Equal("There were no changes!", content);
        }

        [Fact(
            DisplayName = " =========== TC10 Edit existing notes Description with new Description"
        )]
        public async Task EditNoteWithNewDescription()
        {
            await _auth.AuthenticateAsync();

            Random random = new Random();

            // Generate a random integer between 0 (inclusive) and 100000 (exclusive)
            int randomNumber = random.Next(0, 100000);
            int noteId = 6;
            Note noteDto = new Note { Description = $"Test Edit Description{randomNumber}", };

            var response = await _client.PutAsync(
                $"/api/EditNote/{noteId}",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> EditNoteWithNewDescription Response body : " + content);
            Note jsonContent = JsonConvert.DeserializeObject<Note>(content);

            Assert.NotEmpty(content);
            Assert.Equal(noteDto.Description, jsonContent.Description);
        }

        [Fact(
            DisplayName = " =========== TC11 Edit existing notes Description with empty Description"
        )]
        public async Task EditNoteWithEmptyDescription()
        {
            await _auth.AuthenticateAsync();

            int noteId = 6;
            Note noteDto = new Note { Description = "", };

            var response = await _client.PutAsync(
                $"/api/EditNote/{noteId}",
                new StringContent(
                    JsonConvert.SerializeObject(noteDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> EditNoteWithEmptyDescription Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Equal("There were no changes!", content);
        }

        [Fact(DisplayName = " =========== TC12 Delete existing note by Id")]
        public async Task DeleteNoteById()
        {
            await _auth.AuthenticateAsync();

            NoteDTO noteDtoBeDeleted = new NoteDTO // New note object to be created
            {
                Title = "PostNotes",
                Description = "This is from Integration test",
                Done = false
            };
            // Creating new note to delete in this test
            var responseToBeDeletedNote = await _client.PostAsync(
                "/api/CreateNote",
                new StringContent(
                    JsonConvert.SerializeObject(noteDtoBeDeleted),
                    Encoding.UTF8,
                    "application/json"
                )
            );
            var contentToBeDeleted = await responseToBeDeletedNote.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> contentToBeDeleted Response body : " + contentToBeDeleted);
            Note jsonContent = JsonConvert.DeserializeObject<Note>(contentToBeDeleted);

            int noteId = jsonContent.Id;

            var response = await _client.DeleteAsync($"/api/DeleteNote/{noteId}");

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> DeleteNoteById Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Contains("Successfully deleted Note", content);
        }

        [Fact(DisplayName = " =========== TC13 Delete none existing note by Id")]
        public async Task DeleteNoneExistingNoteById()
        {
            await _auth.AuthenticateAsync();

            int noteId = 09128380;

            var response = await _client.DeleteAsync($"/api/DeleteNote/{noteId}");

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> DeleteNoteById Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Contains($"Note by id : {noteId} not found!", content);
        }
        // TODO: continue with edit note tests, delete note tests
    }
}
