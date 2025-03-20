using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace MyApi.Tests.Auth;

public class TokenResponse
{
    public required string AccessToken { get; set; }
}

public class AuthenticationBearer
{
    private readonly HttpClient _client;

    public AuthenticationBearer(HttpClient client)
    {
        _client = client;
    }

    public async Task RegisterAsync()
    {
        var registerData = new
        {
            email = Environment.GetEnvironmentVariable("TEST_USER"),
            password = Environment.GetEnvironmentVariable("TEST_USER_PSW")
        };

        var response = await _client.PostAsync(
            "/register",
            new StringContent(
                JsonConvert.SerializeObject(registerData),
                Encoding.UTF8,
                "application/json"
            )
        );

        Console.WriteLine(
            " ===> RegisterAsync Response : " + response.Content.ReadAsStringAsync().Result
        );

        if (response.Content.ReadAsStringAsync().Result.Contains("is already taken."))
        {
            Console.WriteLine(" ===> Email is already taken");
        }
        else
        {
            response.EnsureSuccessStatusCode();
        }
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
