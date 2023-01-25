using System.Text.Json.Serialization;

namespace LacunaAdmission.Models;

public class GetRegisterResponse {
    public readonly string Code;
    public readonly string? Message;

    [JsonConstructor]
    public GetRegisterResponse(string code, string? message) {
        Code = code;
        Message = message;
    }

    public override string ToString() {
        var stringResponse = $"GetRegisterResponse{{Code: {Code}";
        
        if (Message != null) {
            stringResponse += $"Message: {Message}";
        }
        return stringResponse + "}";
    }
}