using LacunaAdmission.Models;
using LacunaAdmission.Workers;

namespace LacunaAdmission;

public static class Program {
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://gene.lacuna.cc/") };
    public static readonly string baseAdress = "https://gene.lacuna.cc/";

    static async Task Main(string[] args) {
        if (args.Length == 0) {
            await SendRequest();
            return;
        }
        var command = args[0];

        switch (command) {
            case "register": 
                if (args.Length < 4) {
                    await Console.Error.WriteLineAsync("register username email password");
                    return;
                }
                await Register(args[1], args[2], args[3]);
                break;

            case "start":
                if (args.Length < 3) {
                    await Console.Error.WriteLineAsync("run <username> <password>");
                    return;
                }
                await WorkerRun(args[1], args[2]);
                break;

            default: 
                await SendRequest();
                break;
        }
    }
     private static async Task SendRequest() {
        await Console.Error.WriteLineAsync("Request: gene <args>");
        await Console.Error.WriteLineAsync("register <username> <email> <password>");
        await Console.Error.WriteLineAsync("run <username> <password>");
    }
    private static async Task Register(string username, string email, string password) {
        var authSerivce = new AuthService(Client);
        await authSerivce.Register(new GetRegisterRequest(username, email, password));
        
        Console.WriteLine($"User registered; {username}");
    }
    private static async Task WorkerRun(string username, string password) {
        var authService = new AuthService(Client);
        var workerService = new WorkerService(Client);

        await authService.Login(new UserLoginRequest(username, password));
        Console.WriteLine($"Logged as {username}");

        var worker = await workerService.GetWorker();
        Console.WriteLine($"Worker Running: {worker.Id}");
        Console.WriteLine($"Worker Type: {worker.Type}");

        await workerService.RunWorker(worker);
        Console.WriteLine("Worker executed successfully");
    }
 }
