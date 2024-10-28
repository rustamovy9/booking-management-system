using MainApp.DataContexts;
using MainApp.Dto_s;
using MainApp.Entities;
using MainApp.ExtansionMethod.Mapper;
using MainApp.Filter;
using MainApp.Responses;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Services.UserServices;

public sealed class UserService(DataContext context):IUserService
{
    public async Task<bool> CreateUserAsync(CreateUserDto user)
    {
        try
        {
            await context.Users.AddAsync(user.CreateDtoToUser());
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> UpdateUserAsync(UpdateUserDto user)
    {
        try
        {
            User? existingUser = await context.Users.FirstOrDefaultAsync(x => x.Id == user.Id && x.IsDeleted == false);
            if (existingUser is null) return await Task.FromResult(false);

            context.Users.Update(existingUser.UpdateDtoToUser(user));
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        try
        {
            User? existingUser = await context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted == false);
            if (existingUser is null) return await Task.FromResult(false);

            existingUser.DeleteDtoToUser();
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<ReadUserDto?> GetUserByIdAsync(int userId)
    {
        try
        {
            ReadUserDto? user = await context.Users.Where(x => x.Id == userId && x.IsDeleted == false)
               .Select(x=>x.UserToReadDto()).FirstOrDefaultAsync();

            return await Task.FromResult(user?? null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<PaginationResponse<IEnumerable<ReadUserDto>>> GetAllUsersAsync(UserFilter filter)
    {
        try
        {
            IQueryable<User> users = context.Users;
            if (filter?.FirstName is not null)
                users = users.Where(x => x.FirstName.ToLower()
                    .Contains(filter.FirstName.ToLower()));
            
            if (filter?.LastName is not null)
                users = users.Where(x => x.LastName.ToLower()
                    .Contains(filter.LastName.ToLower()));

            IQueryable<ReadUserDto> res = users.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).Select(x => x.UserToReadDto());

            int totalRecords = await context.Users.CountAsync();

            return await Task.FromResult(PaginationResponse<IEnumerable<ReadUserDto>>.Create(filter.PageNumber, filter.PageSize,
                totalRecords, res));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}