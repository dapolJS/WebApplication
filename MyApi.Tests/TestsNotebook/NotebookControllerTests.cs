using System.Text;
using FirstWebApi.DTOs;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using MyApi.Tests.Auth;
using Newtonsoft.Json;

namespace MyApi.Tests.NotebookTests
{
    public class NotebookControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AuthenticationBearer _auth;

        public NotebookControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _auth = new AuthenticationBearer(_client);
            new AuthenticationBearer(_client).RegisterAsync().GetAwaiter().GetResult();
        }

        [Fact(DisplayName = " =========== TC1 Returns list of existing Notebooks")]
        public async Task GetNotesReturnsListOfNotes()
        {
            await _auth.AuthenticateAsync();

            // Act
            var response = await _client.GetAsync("/api/Notebook/0/None"); // API endpoint to get notes

            // Assert
            response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

            var content = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<List<NotebookDTO>>(content);

            Assert.NotNull(notes); // Verify the list is not null
            Assert.NotEmpty(notes); // Verify that the list is not empty
            Assert.True(notes.Count >= 1); // Check if there is more then 8 notes
        }

        [Fact(DisplayName = " =========== TC2 Create Notebook with Title, UniqueKey")]
        public async Task PostNotebookReturnsCreatedNotebook()
        {
            await _auth.AuthenticateAsync();

            NotebookDTO notebookDTO = new NotebookDTO // New note object to be created
            {
                Title = "PostNotebook",
                UniqueKey = "Test UniqueKey",
            };
            var response = await _client.PostAsync(
                "/api/CreateNotebook",
                new StringContent(
                    JsonConvert.SerializeObject(notebookDTO),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

            // Assert
            Console.WriteLine(
                " ===> PostNotebookReturnsCreatedNotebook Response body : " + content
            );
            Notebook jsonContent = JsonConvert.DeserializeObject<Notebook>(content);
            Assert.NotEmpty(content);
            Assert.NotNull(jsonContent.Title);
            Assert.NotNull(jsonContent.UniqueKey);
            Assert.Equal(jsonContent.UniqueKey, notebookDTO.UniqueKey);
        }

        [Fact(DisplayName = " =========== TC3 Create Notebook with empty title")]
        public async Task PostNotebookReturnsBadRequestWithoutTitle()
        {
            await _auth.AuthenticateAsync();

            NotebookDTO notebookDTO = new NotebookDTO
            {
                Title = "",
                UniqueKey = "From PostNotes test, Title not existing"
            };
            var response = await _client.PostAsync(
                "/api/CreateNotebook",
                new StringContent(
                    JsonConvert.SerializeObject(notebookDTO),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Console.WriteLine(" ===> Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Equal("Please enter valid value in notebookDTO.Title!", content);
        }

        [Fact(DisplayName = " =========== TC4 Create Notebook with empty UniqueKey")]
        public async Task PostNotebookReturnOkWithoutUniqueKey()
        {
            await _auth.AuthenticateAsync();

            NotebookDTO notebookDTO = new NotebookDTO { Title = "PostNotesTitle", UniqueKey = "" };

            var response = await _client.PostAsync(
                "/api/CreateNotebook",
                new StringContent(
                    JsonConvert.SerializeObject(notebookDTO),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

            // Assert
            Console.WriteLine(
                " ===> PostNotebookReturnOkWithoutUniqueKey Response body : " + content
            );
            Notebook jsonContent = JsonConvert.DeserializeObject<Notebook>(content);
            Assert.NotEmpty(content);
            Assert.Equal(jsonContent.Title, jsonContent.Title);
            Assert.Equal(jsonContent.UniqueKey, notebookDTO.UniqueKey);
        }

        [Fact(DisplayName = " =========== TC5 Createbook with note")]
        public async Task PostNotebookCreateNotebookWithExistingNote()
        {
            await _auth.AuthenticateAsync();

            Note note = new Note { Title = "TestNotebook", Description = "For Notebook test" };

            Notebook notebook = new Notebook
            {
                Notes = [note],
                Title = "Test Title",
                UniqueKey = "Test UniqueKey",
            };

            var response = await _client.PostAsync(
                "/api/CreateNotebook",
                new StringContent(
                    JsonConvert.SerializeObject(notebook),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

            var content = await response.Content.ReadAsStringAsync();

            NotebookDTO jsonContent = JsonConvert.DeserializeObject<NotebookDTO>(content);
            Console.WriteLine(
                " ===> PostNotebookCreateNotebookWithExistingNote Response body : " + content
            );
            Assert.NotEmpty(content);
            Assert.Equal(notebook.Title, jsonContent.Title);
            Assert.Equal(notebook.UniqueKey, jsonContent.UniqueKey);
            Assert.True(notebook.Notes.Count >= 1);
        }

        [Fact(DisplayName = " =========== TC6 Delete existing Notebook by Id")]
        public async Task DeleteNotebookById()
        {
            await _auth.AuthenticateAsync();

            Notebook notebookToBeDeleted = new Notebook // New note object to be created
            {
                Title = "PostNotes",
                UniqueKey = "This is from Integration test",
            };
            // Creating new note to delete in this test
            var responseToBeDeletedNote = await _client.PostAsync(
                "/api/CreateNotebook",
                new StringContent(
                    JsonConvert.SerializeObject(notebookToBeDeleted),
                    Encoding.UTF8,
                    "application/json"
                )
            );
            var contentToBeDeleted = await responseToBeDeletedNote.Content.ReadAsStringAsync();
            responseToBeDeletedNote.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

            Console.WriteLine(" ===> contentToBeDeleted Response body : " + contentToBeDeleted);
            Notebook jsonContent = JsonConvert.DeserializeObject<Notebook>(contentToBeDeleted);

            int noteId = jsonContent.Id;

            var response = await _client.DeleteAsync($"/api/DeleteNotebook/{noteId}");
            response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> DeleteNotebookById Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Contains("Succesfully deleted Notebook", content);
        }

        [Fact(DisplayName = " =========== TC7 Delete none existing Notebook by Id")]
        public async Task DeleteNoneExistingNotebookById()
        {
            await _auth.AuthenticateAsync();

            int notebookId = 09128380;

            var response = await _client.DeleteAsync($"/api/DeleteNotebook/{notebookId}");

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" ===> DeleteNoneExistingNotebookById Response body : " + content);
            Assert.NotEmpty(content);
            Assert.Contains($"Notebook by Id : {notebookId} not found!", content);
        }
    }
}
