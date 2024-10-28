using MainApp.Dto_s;
using MainApp.Filter;
using MainApp.Responses;
using MainApp.Services.ProviderServices;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;


[ApiController]
[Route("/api/providers/")]
public sealed class ProviderController(IProviderService providerService):ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateProvider([FromBody] CreateProviderDto provider)
    {
        bool res = await providerService.CreateProviderAsync(provider);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null,res));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProvider([FromBody] UpdateProviderDto provider)
    {
        bool res = await providerService.UpdateProviderAsync(provider);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{providerId:int}")]
    public async Task<IActionResult> DeleteProvider(int providerId)
    {
        bool res = await providerService.DeleteProviderAsync(providerId);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
    
    [HttpGet("{providerId:int}")]
    public async Task<IActionResult> GetProviderById(int providerId)
    {
        ReadProviderDto? provider = await providerService.GetProviderByIdAsync(providerId);
        return provider is not null
           ? Ok(ApiResponse<ReadProviderDto?>.Success(null, provider))
            : NotFound(ApiResponse<ReadProviderDto?>.Fail(null, null));
    }

    [HttpGet]
    public IActionResult GetAllProviders([FromQuery] ProviderFilter filter)
    {
        PaginationResponse<IEnumerable<ReadProviderDto>> res = providerService.GetAllProvidersAsync(filter).Result;
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ReadProviderDto>>>.Success(null, res));
    }
}