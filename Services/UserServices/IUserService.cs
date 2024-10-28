using MainApp.Dto_s;
using MainApp.Filter;
using MainApp.Responses;

namespace MainApp.Services.UserServices;

public interface IUserService
{
    Task<bool> CreateUserAsync(CreateUserDto user);
    Task<bool> UpdateUserAsync(UpdateUserDto user);
    Task<bool> DeleteUserAsync(int userId);
    Task<ReadUserDto?> GetUserByIdAsync(int userId);
    Task<PaginationResponse<IEnumerable<ReadUserDto>>> GetAllUsersAsync(UserFilter filter);
}