using MainApp.Dto_s;
using MainApp.Filter;
using MainApp.Responses;

namespace MainApp.Services.ScheduleServices;

public interface IScheduleService
{
    Task<bool> CreateScheduleAsync(CreateScheduleDto schedule);
    Task<bool> UpdateScheduleAsync(UpdateScheduleDto schedule);
    Task<bool> DeleteScheduleAsync(int scheduleId);
    Task<ReadScheduleDto?> GetScheduleByIdAsync(int scheduleId);
    Task<PaginationResponse<IEnumerable<ReadScheduleDto>>> GetAllSchedulesAsync(ScheduleFilter filter);
    Task<PaginationResponse<IEnumerable<ScheduleWithUserAndProviderDto>>> GetSchedulesWithUserAndProviderInfoAsync(BaseFilter filter);
}