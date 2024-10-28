using MainApp.Dto_s;
using MainApp.Entities;
using MainApp.Filter;
using MainApp.Responses;

namespace MainApp.Services.ProviderServices;

public interface IProviderService
{
    Task<bool> CreateProviderAsync(CreateProviderDto provider);
    Task<bool> UpdateProviderAsync(UpdateProviderDto provider);
    Task<bool> DeleteProviderAsync(int providerId);
    Task<ReadProviderDto?> GetProviderByIdAsync(int providerId);
    Task<PaginationResponse<IEnumerable<ReadProviderDto>>> GetAllProvidersAsync(ProviderFilter filter);
}