using System.Text.Json.Serialization;

namespace LacunaAdmission.Workers;

public class WorkResponse {
    public string Code { get; }
    public string? Message { get;}

    [JsonConstructor]
    public WorkResponse(string code, string? message) {
        Code = code;
        Message = message;
    }
    public override string ToString()
    {
        var str = "$WorkResponse{{Code: {Code}";

        if (Message != null) str += $"Message: {Message}";

        return str + "}";
    }
}