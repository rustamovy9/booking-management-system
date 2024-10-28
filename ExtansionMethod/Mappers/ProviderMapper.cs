using MainApp.Dto_s;
using MainApp.Entities;

namespace MainApp.ExtansionMethod.Mapper;

public static class ProviderMapper
{
    public static ReadProviderDto ProviderToReadDto(this Provider provider)
    {
        return new ReadProviderDto()
        {
            Id = provider.Id,
            FirstName = provider.FirstName,
            LastName = provider.LastName,
            Email = provider.Email,
            PhoneNumber = provider.PhoneNumber,
            Specialization = provider.Specialization,
            Address = provider.Address
        };
    }

    public static Provider UpdateDtoToProvider(this Provider provider, UpdateProviderDto updateDto)
    {
        provider.Id = updateDto.Id;
        provider.FirstName = updateDto.FirstName;
        provider.LastName = updateDto.LastName;
        provider.Email = updateDto.Email;
        provider.PhoneNumber = updateDto.PhoneNumber;
        provider.Specialization = updateDto.Specialization;
        provider.Address = updateDto.Address;
        provider.UpdatedAt = DateTime.UtcNow;
        provider.Version += 1;
        return provider;
    }

    public static Provider CreateDtoToProvider(this CreateProviderDto providerDto)
    {
        return new Provider()
        {
            FirstName = providerDto.FirstName,
            LastName = providerDto.LastName,
            Email = providerDto.Email,
            PhoneNumber = providerDto.PhoneNumber,
            Specialization = providerDto.Specialization,
            Address = providerDto.Address,
            CreatedAt = DateTime.UtcNow,
            Version = 0
        };
    }

    public static Provider DeleteDtoToProvider(this Provider provider)
    {
        provider.IsDeleted = true;
        provider.DeletedAt = DateTime.UtcNow;
        provider.Version += 1;
        return provider;
    }
}