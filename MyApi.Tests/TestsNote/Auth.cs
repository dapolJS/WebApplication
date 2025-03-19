using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace MyApi.Tests.Auth;

public class TokenResponse
{
    public string AccessToken { get; }
}

public class AuthenticationBearer
{
    private readonly HttpClient _client;

    public AuthenticationBearer(HttpClient client)
    {
        _client = client;
    }

    public async Task AuthenticateAsync()
    {
        var loginData = new
        {
            email = Environment.GetEnvironmentVariable("TEST_USER"),
            password = Environment.GetEnvironmentVariable("TEST_USER_PSW")
        };

        var response = await _client.PostAsync(
            "/login",
            new StringContent(
                JsonConvert.SerializeObject(loginData),
                Encoding.UTF8,
                "application/json"
            )
        );

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var token = JsonConvert.DeserializeObject<TokenResponse>(content).AccessToken;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );
    }
}
