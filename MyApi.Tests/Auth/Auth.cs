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

    public async Task<bool> VerifyEmailAsync()
    {
        await AuthenticateAsync();

        var responseInfo = await _client.GetAsync("manage/info");

        return responseInfo.IsSuccessStatusCode;
    }

    public async Task RegisterAsync()
    {
        var registerData = new
        {
            email = Environment.GetEnvironmentVariable("TEST_USER"),
            password = Environment.GetEnvironmentVariable("TEST_USER_PSW")
        };

        var verifyEmail = await VerifyEmailAsync();

        if (verifyEmail)
        {
            Console.WriteLine(" ===> Email is already registered");
            return;
        }

        var response = await _client.PostAsync(
            "/register",
            new StringContent(
                JsonConvert.SerializeObject(registerData),
                Encoding.UTF8,
                "application/json"
            )
        );

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(
                " ===> Success to Register Email! : " + response.Content.ReadAsStringAsync().Result
            );
        }
        response.EnsureSuccessStatusCode();
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

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                "Failed to authenticate" + "\n" + response.Content.ReadAsStringAsync().Result
            );
        }
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var token = JsonConvert.DeserializeObject<TokenResponse>(content).AccessToken;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );
    }
}
