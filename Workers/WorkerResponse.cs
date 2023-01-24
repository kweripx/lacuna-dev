using System.Text.Json.Serialization;

namespace LacunaAdmission.Workers;

public class WorkerResponse {
    public WorkerOperation WorkerOperation { get;}
    public string Code { get;} 
    public string? Message { get;}

    [JsonConstructor]
    public WorkerResponse(WorkerOperation operation, string code, string? message) {
        WorkerOperation = operation;
        Code = code;
        Message = message;
    }

    public override string ToString()
    {
        var str = "$WorkerResponse{{Worker: {WorkerOperation}, Code: {Code}";

        if (Message != null) str+= $"Message: {Message}";

        return str + "}";
    }
}