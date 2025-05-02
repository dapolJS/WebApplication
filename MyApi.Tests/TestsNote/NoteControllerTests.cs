using System.Text;
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

        [Fact(DisplayName = " =========== TC1 Returns list of existing notes",
            Skip = "Temporarily disabled for refactoring")]
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

        [Fact(DisplayName = " =========== TC2 Create note with Title, Description, Done",
            Skip = "Temporarily disabled for refactoring")]
        public async Task PostNotesReturnsCreatedNote()
        {
            await _auth.AuthenticateAsync();

            NoteDTO noteDto = new NoteDTO // New note object to be created
            {
                Content = "This is from Integration test",
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
            Assert.NotNull(jsonContent.Content);
        }

        [Fact(DisplayName = " =========== TC12 Delete existing note by Id",
            Skip = "Temporarily disabled for refactoring")]
        public async Task DeleteNoteById()
        {
            await _auth.AuthenticateAsync();

            NoteDTO noteDtoBeDeleted = new NoteDTO // New note object to be created
            {
                Content = "This is from Integration test",
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

            var noteId = jsonContent.Id;

            var response = await _client.DeleteAsync($"/api/DeleteNote/{noteId}");

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> DeleteNoteById Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Contains("Successfully deleted Note", content);
        }

        [Fact(DisplayName = " =========== TC13 Delete none existing note by Id",
            Skip = "Temporarily disabled for refactoring")]
        public async Task DeleteNoneExistingNoteById()
        {
            await _auth.AuthenticateAsync();

            const int noteId = 09128380;

            var response = await _client.DeleteAsync($"/api/DeleteNote/{noteId}");

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> DeleteNoteById Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Contains($"Note by id : {noteId} not found!", content);
        }
    }
}