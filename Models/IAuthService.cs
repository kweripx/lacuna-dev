namespace LacunaAdmission.Models;

public interface IAuthService {
    public Task <UserLoginResponse> Login(UserLoginResponse data);
    public Task<GetRegisterResponse> Register(GetRegisterRequest data);
}