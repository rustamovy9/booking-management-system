using MainApp.Dto_s;
using MainApp.Filter;
using MainApp.Responses;
using MainApp.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("/api/users/")]
public sealed class UserController(IUserService userService):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        bool res = await userService.CreateUserAsync(userDto);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
    {
        bool res = await userService.UpdateUserAsync(userDto);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        bool res = await userService.DeleteUserAsync(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        ReadUserDto? user = await userService.GetUserByIdAsync(id);
        return user is not null
            ? Ok(ApiResponse<ReadUserDto?>.Success(null, user))
            : NotFound(ApiResponse<ReadUserDto?>.Fail(null, null));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserFilter filter)
    {
        PaginationResponse<IEnumerable<ReadUserDto>> res = await userService.GetAllUsersAsync(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ReadUserDto>>>.Success(null, res));
    }
}