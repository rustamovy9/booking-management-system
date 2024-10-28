using MainApp.Dto_s;
using MainApp.Entities;

namespace MainApp.ExtansionMethod.Mapper;

public static class UserMapper
{
    public static ReadUserDto UserToReadDto(this User user)
    {
        return new ReadUserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
    }

    public static User UpdateDtoToUser(this User user, UpdateUserDto updateDto)
    {
        user.Id = updateDto.Id;
        user.FirstName = updateDto.FirstName;
        user.LastName = updateDto.LastName;
        user.Email = updateDto.Email;
        user.PhoneNumber = updateDto.PhoneNumber;
        user.UpdatedAt = DateTime.UtcNow;
        user.Version += 1;
        return user;
    }

    public static User CreateDtoToUser(this CreateUserDto userDto)
    {
        return new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            Version = 0
        };
    }

    public static User DeleteDtoToUser(this User user)
    {
        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.Version += 1;
        return user;
    }
}