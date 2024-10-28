namespace MainApp.Dto_s;

public readonly record struct ReadBookingDto(
    int Id,
    int UserId,
    int ProviderId,
    int ServiceInfoId,
    DateTime StartDateTime,
    DateTime EndDateTime,
    decimal TotalPrice);
    
public readonly record struct CreateBookingDto(
    int UserId, 
    int ProviderId, 
    int ServiceInfoId, 
    DateTime StartDateTime, 
    DateTime EndDateTime, 
    decimal TotalPrice);
        
public readonly record struct UpdateBookingDto(
    int Id,
    int UserId,
    int ProviderId, 
    int ServiceInfoId, 
    DateTime StartDateTime, 
    DateTime EndDateTime, 
    decimal TotalPrice);