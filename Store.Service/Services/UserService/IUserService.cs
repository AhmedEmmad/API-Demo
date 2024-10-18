using Store.Service.Services.UserService.Dtos;

namespace Store.Service.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> Login(LoginDto input);
        Task<UserDto> Register(RegisterDto input);
    }
}
