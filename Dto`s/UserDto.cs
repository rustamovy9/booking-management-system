namespace MainApp.Dto_s;

public readonly record struct ReadUserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
    
public readonly record struct CreateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
    
public readonly record struct UpdateUserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
    