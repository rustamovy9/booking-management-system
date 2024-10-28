namespace MainApp.Dto_s;

public readonly record struct ReadScheduleDto(
    int Id,
    int ProviderId,
    DateTime ScheduleDate,
    DateTime StartTime,
    DateTime EndTime);
    
public readonly record struct CreateScheduleDto(
    int ProviderId,
    DateTime ScheduleDate, 
    DateTime StartTime, 
    DateTime EndTime);
        
public readonly record struct UpdateScheduleDto(
    int Id,
    int ProviderId, 
    DateTime ScheduleDate, 
    DateTime StartTime,
    DateTime EndTime);