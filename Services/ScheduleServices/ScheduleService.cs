using MainApp.DataContexts;
using MainApp.Dto_s;
using MainApp.Entities;
using MainApp.ExtansionMethod.Mapper;
using MainApp.Filter;
using MainApp.Responses;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Services.ScheduleServices;

public sealed class ScheduleService(DataContext context): IScheduleService
{
    public async Task<bool> CreateScheduleAsync(CreateScheduleDto schedule)
    {
        try
        {
            await context.Schedules.AddAsync(schedule.CreateDtoToSchedule());
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> UpdateScheduleAsync(UpdateScheduleDto schedule)
    {
        try
        {
            Schedule? existingSchedule = await context.Schedules
                .FirstOrDefaultAsync(x => x.Id == schedule.Id && x.IsDeleted == false);
            if (existingSchedule is null) return await Task.FromResult(false);

            context.Schedules.Update(existingSchedule.UpdateDtoToSchedule(schedule));
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteScheduleAsync(int scheduleId)
    {
        try
        {
            Schedule? existingSchedule = await context.Schedules
                .FirstOrDefaultAsync(x => x.Id == scheduleId && x.IsDeleted == false);
            if (existingSchedule is null) return await Task.FromResult(false);

            existingSchedule.DeleteDtoToSchedule();
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<ReadScheduleDto?> GetScheduleByIdAsync(int scheduleId)
    {
        try
        {
            ReadScheduleDto? schedule = await context.Schedules
                .Where(x => x.Id == scheduleId && x.IsDeleted == false)
                .Select(x => x.ScheduleToReadDto())
                .FirstOrDefaultAsync();

            return await Task.FromResult(schedule ?? null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<PaginationResponse<IEnumerable<ReadScheduleDto>>> GetAllSchedulesAsync(ScheduleFilter filter)
    {
        try
        {
            IQueryable<Schedule> schedules = context.Schedules;
            if (filter?.StartTime is not null)
                schedules = schedules.Where(x => x.StartTime >= filter.StartTime);
            
            if (filter?.EndTime is not null)
                schedules = schedules.Where(x => x.EndTime <= filter.EndTime);

            IQueryable<ReadScheduleDto> res = schedules
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => x.ScheduleToReadDto());

            int totalRecords = await schedules.CountAsync();

            return PaginationResponse<IEnumerable<ReadScheduleDto>>.Create(
                filter.PageNumber, filter.PageSize, totalRecords, res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    public async Task<PaginationResponse<IEnumerable<ScheduleWithUserAndProviderDto>>> GetSchedulesWithUserAndProviderInfoAsync(BaseFilter filter)
    {
        try
        {
            IQueryable<ScheduleWithUserAndProviderDto> scheduleWithUserAndProviderDto = (from schedule in context.Schedules
                join provider in context.Providers on schedule.ProviderId equals provider.Id
                join booking in context.Bookings on provider.Id equals booking.ProviderId
                join user in context.Users on booking.UserId equals user.Id
                select new ScheduleWithUserAndProviderDto()
                {
                    ScheduleId = schedule.Id,
                    ProviderName = string.Concat(provider.FirstName, " ", provider.LastName),
                    Specialization = provider.Specialization,
                    Address = provider.Address,
                    UserEmail = user.Email,
                    UserName = string.Concat(user.FirstName, " ", user.LastName),
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                }).Distinct();
            
            IQueryable<ScheduleWithUserAndProviderDto> res = scheduleWithUserAndProviderDto
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            int totalRecords = await scheduleWithUserAndProviderDto.CountAsync();
            
            return PaginationResponse<IEnumerable<ScheduleWithUserAndProviderDto>>.Create(
                filter.PageNumber, filter.PageSize, totalRecords, res);
            
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    
}