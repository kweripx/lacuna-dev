namespace LacunaAdmission.Workers;

public interface IWorkerService {
    public Task<Job> GetWorker();
    public Task RunWorker(Job Job);
}