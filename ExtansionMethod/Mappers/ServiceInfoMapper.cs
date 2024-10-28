using MainApp.Dto_s;
using MainApp.Entities;

namespace MainApp.ExtansionMethod.Mapper;

public static class ServiceInfoMapper
{
    public static ReadServiceInfoDto ServiceInfoToReadDto(this ServiceInfo serviceInfo)
    {
        return new ReadServiceInfoDto()
        {
            Id = serviceInfo.Id,
            Name = serviceInfo.Name,
            Price = serviceInfo.Price,
            Address = serviceInfo.Address
        };
    }

    public static ServiceInfo UpdateDtoToServiceInfo(this ServiceInfo serviceInfo, UpdateServiceInfoDto updateDto)
    {
        serviceInfo.Id = updateDto.Id;
        serviceInfo.Name = updateDto.Name;
        serviceInfo.Price = updateDto.Price;
        serviceInfo.Address = updateDto.Address;
        serviceInfo.UpdatedAt = DateTime.UtcNow;
        serviceInfo.Version += 1;
        return serviceInfo;
    }

    public static ServiceInfo CreateDtoToServiceInfo(this CreateServiceInfoDto serviceInfoDto)
    {
        return new ServiceInfo()
        {
            Name = serviceInfoDto.Name,
            Price = serviceInfoDto.Price,
            Address = serviceInfoDto.Address,
            CreatedAt = DateTime.UtcNow,
            Version = 0
        };
    }

    public static ServiceInfo DeleteDtoToServiceInfo(this ServiceInfo serviceInfo)
    {
        serviceInfo.IsDeleted = true;
        serviceInfo.DeletedAt = DateTime.UtcNow;
        serviceInfo.Version += 1;
        return serviceInfo;
    }
}