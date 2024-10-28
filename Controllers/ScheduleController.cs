using MainApp.Dto_s;
using MainApp.Filter;
using MainApp.Responses;
using MainApp.Services.ScheduleServices;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;


[ApiController]
[Route("/api/schedules/")]
public sealed class ScheduleController(IScheduleService scheduleService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleDto schedule)
    {
        bool res = await scheduleService.CreateScheduleAsync(schedule);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSchedule([FromBody] UpdateScheduleDto schedule)
    {
        bool res = await scheduleService.UpdateScheduleAsync(schedule);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
    
    [HttpDelete("{scheduleId:int}")]
    public async Task<IActionResult> DeleteSchedule(int scheduleId)
    {
        bool res = await scheduleService.DeleteScheduleAsync(scheduleId);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpGet("{scheduleId:int}")]
    public async Task<IActionResult> GetScheduleById(int scheduleId)
    {
        ReadScheduleDto? schedule = await scheduleService.GetScheduleByIdAsync(scheduleId);
        return schedule is not null
            ? Ok(ApiResponse<ReadScheduleDto?>.Success(null, schedule))
            : NotFound(ApiResponse<ReadScheduleDto?>.Fail(null, null));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllSchedules([FromQuery] ScheduleFilter filter)
    {
        PaginationResponse<IEnumerable<ReadScheduleDto>> res =await scheduleService.GetAllSchedulesAsync(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ReadScheduleDto>>>.Success(null, res));
    }
    
    [HttpGet("users-and-providers")]
    public async Task<IActionResult> GetSchedulesWithUserAndProviderInfo([FromQuery] BaseFilter filter)
    {
        PaginationResponse<IEnumerable<ScheduleWithUserAndProviderDto>> res = await scheduleService.GetSchedulesWithUserAndProviderInfoAsync(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ScheduleWithUserAndProviderDto>>>.Success(null, res));
    }
        
    
}