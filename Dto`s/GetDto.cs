namespace MainApp.Dto_s;

public readonly record struct  BookingInfoDto(
    int Id,
    string UserFirstName,
    string UserLastName,
    string ProviderName,
    string ServiceName,
    DateTime StartDateTime,
    DateTime EndDateTime,
    decimal TotalPrice);

public readonly record struct ScheduleWithUserAndProviderDto(
    int ScheduleId ,
    string ProviderName,
    string Specialization,
    string Address,
    string UserName,
    string UserEmail,
    DateTime StartTime,
    DateTime EndTime);

public readonly record struct ServiceInfoDto(
    string ServiceName,
    decimal ServicePrice,
    string ProviderName,
    string Specialization, 
    string Address);


public readonly record struct BookingDetailsDto(
    string UserName,
    DateTime BookingStartDateTime ,
    DateTime BookingEndDateTime ,
    string ServiceName ,
    string ProviderName ,
    string Address,
    string Specialization,
    DateTime ScheduleStartTime ,
    DateTime ScheduleEndTime );

