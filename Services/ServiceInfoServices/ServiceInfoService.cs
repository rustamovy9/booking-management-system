using MainApp.DataContexts;
using MainApp.Dto_s;
using MainApp.Entities;
using MainApp.ExtansionMethod.Mapper;
using MainApp.Filter;
using MainApp.Responses;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Services.ServiceInfoServices;

public sealed class ServiceInfoService(DataContext context) : IServiceInfoService
{
     public async Task<bool> CreateServiceInfoAsync(CreateServiceInfoDto serviceInfo)
    {
        try
        {
            await context.ServiceInfos.AddAsync(serviceInfo.CreateDtoToServiceInfo());
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> UpdateServiceInfoAsync(UpdateServiceInfoDto serviceInfo)
    {
        try
        {
            ServiceInfo? existingServiceInfo = await context.ServiceInfos
                .FirstOrDefaultAsync(x => x.Id == serviceInfo.Id && x.IsDeleted == false);
            if (existingServiceInfo is null) return await Task.FromResult(false);

            context.ServiceInfos.Update(existingServiceInfo.UpdateDtoToServiceInfo(serviceInfo));
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteServiceInfoAsync(int serviceInfoId)
    {
        try
        {
            ServiceInfo? existingServiceInfo = await context.ServiceInfos
                .FirstOrDefaultAsync(x => x.Id == serviceInfoId && x.IsDeleted == false);
            if (existingServiceInfo is null) return await Task.FromResult(false);

            existingServiceInfo.DeleteDtoToServiceInfo();
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<ReadServiceInfoDto?> GetServiceInfoByIdAsync(int serviceInfoId)
    {
        try
        {
            ReadServiceInfoDto? serviceInfo = await context.ServiceInfos
                .Where(x => x.Id == serviceInfoId && x.IsDeleted == false)
                .Select(x => x.ServiceInfoToReadDto())
                .FirstOrDefaultAsync();

            return await Task.FromResult(serviceInfo ?? null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<PaginationResponse<IEnumerable<ReadServiceInfoDto>>> GetAllServiceInfosAsync(ServiceInfoFilter filter)
    {
        try
        {
            IQueryable<ServiceInfo> serviceInfos = context.ServiceInfos;
            if (filter?.Name is not null)
                serviceInfos = serviceInfos.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            
            if (filter?.Address is not null)
                serviceInfos = serviceInfos.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));

            if (filter?.MinPrice is not null)
                serviceInfos = serviceInfos.Where(x => x.Price >= filter.MinPrice);
        
            if (filter?.MaxPrice is not null)
                serviceInfos = serviceInfos.Where(x => x.Price <= filter.MaxPrice);
            

            IQueryable<ReadServiceInfoDto> res = serviceInfos
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => x.ServiceInfoToReadDto());

            int totalRecords = await serviceInfos.CountAsync();

            return PaginationResponse<IEnumerable<ReadServiceInfoDto>>.Create(
                filter.PageNumber, filter.PageSize, totalRecords, res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    public async Task<PaginationResponse<IEnumerable<ServiceInfoDto>>> GetServiceInfosByProviderAsync(BaseFilter filter,int providerId)
    {
        try
        {
            IQueryable<ServiceInfoDto> serviceInfos = from booking in context.Bookings
                join service in context.ServiceInfos on booking.ServiceInfoId equals service.Id
                join provider in context.Providers on booking.ProviderId equals provider.Id
                where provider.Id == providerId
                select new ServiceInfoDto
                {
                    ServiceName = service.Name,
                    ServicePrice = service.Price,
                    ProviderName = string.Concat(provider.FirstName, " ", provider.LastName),
                    Address = provider.Address,
                    Specialization = provider.Specialization
                };
            
            IQueryable<ServiceInfoDto> res = serviceInfos
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
            int totalRecords = await serviceInfos.CountAsync();
            
            return PaginationResponse<IEnumerable<ServiceInfoDto>>.Create(
                filter.PageNumber, filter.PageSize, totalRecords, res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    
    
}