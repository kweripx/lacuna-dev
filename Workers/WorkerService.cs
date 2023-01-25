using System.Net.Http.Json;
using System.Text.Json;

namespace LacunaAdmission.Workers;

public class WorkerService : IWorkerService {
    private readonly HttpClient _client;

    private static readonly JsonSerializerOptions serializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public WorkerService(HttpClient client) {
        _client = client;
    }
    public async Task<Job> GetWorker() {
        var response = await _client.GetAsync("/api/dna/jobs");
        var content = await response.Content.ReadFromJsonAsync<WorkerResponse>(serializerOptions);

        if (content == null) { throw new NullReferenceException("Job response was null"); }

        if (content.Message != null) { throw new Exception("$Failed to get a job: {content.Message}"); }

        return content.Job;
    }
    public async Task RunWorker(Job job) {
        switch (job.Type) {
            case "GeneCheck": 
                await CheckGeneWorker(job);
                break;
            case "EncodedStrand":
                await CheckEncodedStrand(job);
                break;
            case "DecodeStrand":
                await CheckDecodedStrand(job);
                break;
            
        }
    }
    private async Task CheckGeneWorker(Job job) {
        var isActivated = Job.GeneCheck(job.GeneEncoded!, Job.DecodeStrand(job.StrandEncoded!));

        var response = await _client.PostAsJsonAsync($"/api/dna/jobs/{job.Id}/gene",
            new CheckGeneOperation(isActivated),
            serializerOptions);

        var content = await response.Content.ReadFromJsonAsync<WorkerResponse>();

        if (content == null) {
            throw new NullReferenceException("Null worker response");
         } if (content.Message != null) { throw new Exception($"Wasnt able to check gene: {content.Message}"); }
     }
    
    private async Task CheckEncodedStrand(Job job) {

        var strandEncoded = Job.EncodedStrand(job.Strand!);
        var response = await _client.PostAsJsonAsync($"api/dna/jobs/{job.Id}/encode",
            new EncodeStrandOperation(strandEncoded),
            serializerOptions);

        var content = await response.Content.ReadFromJsonAsync<WorkerResponse>();

        if (content == null) { throw new NullReferenceException("Null worker response"); }

        if (content.Message != null) {throw new Exception($"Wasnt able to encode strand: {content.Message}"); }
    }
    
    private async Task CheckDecodedStrand(Job job) {
        var strandEncoded = Job.EncodedStrand(job.StrandEncoded!);

        var response = await _client.PostAsJsonAsync($"/api/dna/jobs/{job.Id}/decode", 
            new DecodeStrandOperation(strandEncoded),
            serializerOptions);

        var content = await response.Content.ReadFromJsonAsync<WorkerResponse>();

        if (content == null) { throw new NullReferenceException("Null worker response"); }
        if (content.Message != null) {throw new Exception($"Wasnt able to decode strand: {content.Message}"); }
    }
}