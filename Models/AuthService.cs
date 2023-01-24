using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LacunaAdmission.Models;

public class AuthService : IAuthService {
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions serializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public AuthService(HttpClient client) {
        _client = client;
    }
    public async Task<UserLoginResponse> Login(UserLoginResponse data) {

        var response = await _client.PostAsJsonAsync("/api/users/login", data, serializerOptions);
        var content = await response.Content.ReadFromJsonAsync<UserLoginResponse>(serializerOptions);

        if (content == null) { throw new NullReferenceException("User Login Response returned null");}

        if (content.Message != null) { throw new Exception($"Isnt possible to login: {content.Message}"); }

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(content.AcessToken);

        return content;
    }

    public async Task<GetRegisterResponse> Register(GetRegisterRequest data) {
        var response = await _client.PatchAsJsonAsync("/api/users/create", data, serializerOptions);
        var content = await response.Content.ReadFromJsonAsync<GetRegisterResponse>(serializerOptions);

        if (content == null) { throw new NullReferenceException("User Register Response returned null"); }

        if (content.Message != null) { throw new Exception($"Isnt possible to register: {content.Message}"); }

        return content;
    }
}