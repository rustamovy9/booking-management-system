using MainApp.Dto_s;
using MainApp.Filter;
using MainApp.Responses;

namespace MainApp.Services.ServiceInfoServices;

public interface IServiceInfoService
{
    Task<bool> CreateServiceInfoAsync(CreateServiceInfoDto serviceInfo);
    Task<bool> UpdateServiceInfoAsync(UpdateServiceInfoDto serviceInfo);
    Task<bool> DeleteServiceInfoAsync(int serviceInfoId);
    Task<ReadServiceInfoDto?> GetServiceInfoByIdAsync(int serviceInfoId);
    Task<PaginationResponse<IEnumerable<ReadServiceInfoDto>>> GetAllServiceInfosAsync(ServiceInfoFilter filter);

    Task<PaginationResponse<IEnumerable<ServiceInfoDto>>> GetServiceInfosByProviderAsync(BaseFilter filter, int providerId);
}