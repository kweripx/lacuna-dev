using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LacunaAdmission.Models;

public class AuthService : IAuthService {
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions SerializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public AuthService(HttpClient client) {
        _client = client;
    }
    public async Task<UserLoginResponse> Login(UserLoginRequest data) {

        var response = await _client.PostAsJsonAsync("/api/users/login", data, SerializerOptions);
        var content = await response.Content.ReadFromJsonAsync<UserLoginResponse>(SerializerOptions);

        if (content == null) { throw new NullReferenceException("User Login Response returned null");}

        if (content.Message != null) { throw new Exception($"Isnt possible to login: {content.Message}"); }

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(content.AccessToken);

        return content;
    }

    public async Task<GetRegisterRequest> Register(GetRegisterRequest data) {
        var response = await _client.PostAsJsonAsync("/api/users/create", data, SerializerOptions);
        var content = await response.Content.ReadFromJsonAsync<GetRegisterRequest>(SerializerOptions);

        if (content == null) { throw new NullReferenceException("User Register Response returned null"); }

        return content;
    }
}