using System.Text.Json.Serialization;

namespace LacunaAdmission.Models;

public class UserLoginResponse {
    public string? AccessToken { get;}
    public string Code { get;}
    public string? Message { get;}

    [JsonConstructor]
    public UserLoginResponse(string accessToken, string code, string? message) {
        AccessToken = accessToken;
        Code = code;
        Message = message;
    }
    public override string ToString() { 
        var stringResponse = $"GetRegisterResponse{{Code: {Code} }}";

        if (AccessToken != null) {
            stringResponse += $"AcessToken: {AccessToken}";
        }
        if (Message != null) {
            stringResponse += $"Message: {Message}";
        }
        return stringResponse;
    }
}