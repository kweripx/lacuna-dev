using System.Text.Json.Serialization;

namespace LacunaAdmission.Models;

public class UserLoginResponse {
    public string? AcessToken { get;}
    public string Code { get;}
    public string? Message { get;}

    [JsonConstructor]
    public UserLoginResponse(string acessToken, string code, string? message) {
        AcessToken = acessToken;
        Code = code;
        Message = message;
    }
    public override string ToString() { 
        var stringResponse = $"GetRegisterResponse{{Code: {Code} }}";

        if (AcessToken != null) {
            stringResponse += $"AcessToken: {AcessToken}";
        }
        if (Message != null) {
            stringResponse += $"Message: {Message}";
        }
        return stringResponse;
    }
}