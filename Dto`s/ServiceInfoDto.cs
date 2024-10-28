namespace MainApp.Dto_s;

public readonly record struct ReadServiceInfoDto(
    int Id,
    string Name,
    decimal Price,
    string Address);
    
    
    
public readonly record struct CreateServiceInfoDto(
    string Name,
    decimal Price,
    string Address);
    
    
public readonly record struct UpdateServiceInfoDto(
    int Id,
    string Name,
    decimal Price,
    string Address);