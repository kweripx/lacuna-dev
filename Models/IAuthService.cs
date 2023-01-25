namespace LacunaAdmission.Models;

public interface IAuthService {
    public Task <UserLoginResponse> Login(UserLoginRequest data);
    public Task<GetRegisterRequest> Register(GetRegisterRequest data);
}