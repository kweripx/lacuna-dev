using System.Text.Json.Serialization;

namespace LacunaAdmission.Workers;

public class WorkerResponse {
    public Job Job { get;}
    public string Code { get;} 
    public string? Message { get;}

    [JsonConstructor]
    public WorkerResponse(Job job, string code, string? message) {
        Job = job;
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