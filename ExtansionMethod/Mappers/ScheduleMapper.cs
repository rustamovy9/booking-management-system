using System.Security.Cryptography.Xml;
using MainApp.Dto_s;
using MainApp.Entities;

namespace MainApp.ExtansionMethod.Mapper;

public static class ScheduleMapper
{
    public static ReadScheduleDto ScheduleToReadDto(this Schedule schedule)
    {
        return new ReadScheduleDto()
        {
            Id = schedule.Id,
            ProviderId = schedule.ProviderId,
            ScheduleDate = schedule.ScheduleDate,
            StartTime = schedule.StartTime,
            EndTime = schedule.EndTime
        };
    }

    public static Schedule UpdateDtoToSchedule(this Schedule schedule, UpdateScheduleDto updateDto)
    {
        schedule.Id = updateDto.Id;
        schedule.ProviderId = schedule.ProviderId;
        schedule.ScheduleDate = schedule.ScheduleDate;
        schedule.StartTime = schedule.StartTime;
        schedule.EndTime = schedule.EndTime;
        schedule.UpdatedAt = DateTime.UtcNow;
        schedule.Version += 1;
        return schedule;
    }

    public static Schedule CreateDtoToSchedule(this CreateScheduleDto scheduleDto)
    {
        return new Schedule()
        {
            ProviderId = scheduleDto.ProviderId,
            ScheduleDate = scheduleDto.ScheduleDate,
            StartTime = scheduleDto.StartTime,
            EndTime = scheduleDto.EndTime,
            CreatedAt = DateTime.UtcNow,
            Version = 0
        };
    }

    public static Schedule DeleteDtoToSchedule(this Schedule schedule)
    {
        schedule.IsDeleted = true;
        schedule.DeletedAt = DateTime.UtcNow;
        schedule.Version += 1;
        return schedule;
    }
}