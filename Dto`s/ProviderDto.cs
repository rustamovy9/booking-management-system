namespace MainApp.Dto_s;

public readonly record struct ReadProviderDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Specialization,
    string Address);
    
public readonly record struct CreateProviderDto(
    string FirstName, 
    string LastName, 
    string Email, 
    string PhoneNumber, 
    string Specialization,
    string Address);

public readonly record struct UpdateProviderDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Specialization,
    string Address);