using System.Text;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using MyApi.Tests.Auth;
using Newtonsoft.Json;

namespace FirstWebApi.Tests;

public class RoomControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly AuthenticationBearer _auth;

    public RoomControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _auth = new AuthenticationBearer(_client);
        new AuthenticationBearer(_client).RegisterAsync().GetAwaiter().GetResult();
    }

    [Fact(DisplayName = " =========== TC1 Returns list of existing Rooms")]
    public async Task GetRoomsReturnsListOfRooms()
    {
        await _auth.AuthenticateAsync();

        // Act
        var response = await _client.GetAsync("/api/GetRooms"); // API endpoint to get notes

        // Assert
        response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK

        var content = await response.Content.ReadAsStringAsync();
        var rooms = JsonConvert.DeserializeObject<List<Room>>(content);

        Assert.NotNull(rooms); // Verify the list is not null
        Assert.NotEmpty(rooms); // Verify that the list is not empty
        Assert.True(rooms.Count > 1); // Check if there is more then 8 notes
    }

    [Fact(DisplayName = " =========== TC2 Create a Room")]
    public async Task CreateRoom()
    {
        await _auth.AuthenticateAsync();

        Room newRoom = new Room();

        // Act
        var response = await _client.PostAsync(
            "/api/AddRoom",
            new StringContent(
                JsonConvert.SerializeObject(newRoom),
                Encoding.UTF8,
                "application/json"
            )
        );

        // Assert
        response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK
        var content = await response.Content.ReadAsStringAsync();
        Room jsonContent = JsonConvert.DeserializeObject<Room>(content);

        Assert.NotNull(jsonContent.UniqueKey); // Verify the list is not null
        Assert.NotEmpty(jsonContent.UniqueKey); // Verify that the Unique Key is not empty
    }

    [Fact(DisplayName = " =========== TC2 Delete Room by Id")]
    public async Task DeleteRoomById()
    {
        await _auth.AuthenticateAsync();

        Room newRoom = new Room();

        // Act
        var response = await _client.PostAsync(
            "/api/AddRoom",
            new StringContent(
                JsonConvert.SerializeObject(newRoom),
                Encoding.UTF8,
                "application/json"
            )
        );

        // Assert
        response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK
        var content = await response.Content.ReadAsStringAsync();
        Room jsonContent = JsonConvert.DeserializeObject<Room>(content);

        //var room = JsonConvert.DeserializeObject<Room>(content);

        Assert.NotNull(jsonContent.UniqueKey); // Verify the list is not null
        Assert.NotEmpty(jsonContent.UniqueKey); // Verify that the Unique Key is not empty

        int roomId = jsonContent.Id;
        var responseDeleteRoom = await _client.DeleteAsync($"/api/DeleteRoom/{roomId}");
        responseDeleteRoom.EnsureSuccessStatusCode();
        var responseDeletedContent = await responseDeleteRoom.Content.ReadAsStringAsync();

        Assert.NotNull(responseDeletedContent);
        Assert.NotEmpty(responseDeletedContent);
    }

    [Fact(DisplayName = " =========== TC3 Delete Room by Id when Id not existing")]
    public async Task DeleteRoomByIdWhenIdNoneExisting()
    {
        await _auth.AuthenticateAsync();

        Room newRoom = new Room();

        // Act
        var response = await _client.PostAsync(
            "/api/AddRoom",
            new StringContent(
                JsonConvert.SerializeObject(newRoom),
                Encoding.UTF8,
                "application/json"
            )
        );

        // Assert
        response.EnsureSuccessStatusCode(); // Ensure the response code is 200 OK
        var content = await response.Content.ReadAsStringAsync();
        Room jsonContent = JsonConvert.DeserializeObject<Room>(content);

        Assert.NotNull(jsonContent.UniqueKey); // Verify the list is not null
        Assert.NotEmpty(jsonContent.UniqueKey); // Verify that the Unique Key is not empty

        int roomId = 341234;
        var responseDeleteRoom = await _client.DeleteAsync($"/api/DeleteRoom/{roomId}");
        var responseDeletedContent = await responseDeleteRoom.Content.ReadAsStringAsync();

        Assert.NotNull(responseDeletedContent);
        Assert.NotEmpty(responseDeletedContent);
        Assert.Equal(responseDeletedContent, $"Room by Id : {roomId} not found!");
    }
}
