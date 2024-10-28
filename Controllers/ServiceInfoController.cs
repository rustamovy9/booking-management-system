using MainApp.Dto_s;
using MainApp.Filter;
using MainApp.Responses;
using MainApp.Services.ServiceInfoServices;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("/api/service-info/")]
public sealed class ServiceInfoController(IServiceInfoService serviceInfoService):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateServiceInfo([FromBody] CreateServiceInfoDto serviceInfoDto)
    {
        bool res = await serviceInfoService.CreateServiceInfoAsync(serviceInfoDto);
        return res
           ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateServiceInfo([FromBody] UpdateServiceInfoDto serviceInfoDto)
    {
        bool res = await serviceInfoService.UpdateServiceInfoAsync(serviceInfoDto); 
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
             : NotFound(ApiResponse<bool>.Fail(null, res));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceInfo(int id)
    {
        bool res = await serviceInfoService.DeleteServiceInfoAsync(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
             : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetServiceInfoById(int id)
    {
        ReadServiceInfoDto? serviceInfo = await serviceInfoService.GetServiceInfoByIdAsync(id);
        return serviceInfo is not null
            ? Ok(ApiResponse<ReadServiceInfoDto?>.Success(null, serviceInfo))
            : NotFound(ApiResponse<ReadServiceInfoDto?>.Fail(null, null));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServiceInfos([FromQuery] ServiceInfoFilter filter)
    {
        PaginationResponse<IEnumerable<ReadServiceInfoDto>> res =
            await serviceInfoService.GetAllServiceInfosAsync(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ReadServiceInfoDto>>>.Success(null, res));
    }

    [HttpGet("provider/{providerId}")]
    public async Task<IActionResult> GetServiceInfosByProvider([FromQuery] BaseFilter filter, int providerId)
    {
        PaginationResponse<IEnumerable<ServiceInfoDto>> serviceInfos =
            await serviceInfoService.GetServiceInfosByProviderAsync(filter, providerId);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ServiceInfoDto>>>.Success(null, serviceInfos));
    }



}