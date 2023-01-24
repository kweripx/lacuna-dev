using System.Text.Json.Serialization;

namespace LacunaAdmission.Models;

public class GetRegisterRequest {
    public string Username { get; }
    public string Email { get; }
    public string Password { get; }

    public GetRegisterRequest(string username, string email, string password) {
        Username = username;
        Email = email;
        Password = password;
    }
}