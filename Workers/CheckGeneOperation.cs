namespace LacunaAdmission.Workers;

public class CheckGeneOperation {
    public bool IsActivated {get; }

    public CheckGeneOperation(bool isActivated) {
        IsActivated = isActivated;
    }
}