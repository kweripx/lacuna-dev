namespace LacunaAdmission.Workers;

public interface IWorkerService {
    public Task<WorkerOperation> GetWorker();
    public Task RunWorker(WorkerOperation worker);
}