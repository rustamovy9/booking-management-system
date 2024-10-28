using MainApp.DataContexts;
using MainApp.Dto_s;
using MainApp.Entities;
using MainApp.ExtansionMethod.Mapper;
using MainApp.Filter;
using MainApp.Responses;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Services.ProviderServices;

public sealed class ProviderService(DataContext context) : IProviderService
{
     public async Task<bool> CreateProviderAsync(CreateProviderDto provider)
    {
        try
        {
            await context.Providers.AddAsync(provider.CreateDtoToProvider());
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> UpdateProviderAsync(UpdateProviderDto provider)
    {
        try
        {
            Provider? existingProvider = await context.Providers
                .FirstOrDefaultAsync(x => x.Id == provider.Id && x.IsDeleted == false);
            if (existingProvider is null) return await Task.FromResult(false);

            context.Providers.Update(existingProvider.UpdateDtoToProvider(provider));
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteProviderAsync(int providerId)
    {
        try
        {
            Provider? existingProvider = await context.Providers
                .FirstOrDefaultAsync(x => x.Id == providerId && x.IsDeleted == false);
            if (existingProvider is null) return await Task.FromResult(false);

            existingProvider.DeleteDtoToProvider();
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<ReadProviderDto?> GetProviderByIdAsync(int providerId)
    {
        try
        {
            ReadProviderDto? provider = await context.Providers
                .Where(x => x.Id == providerId && x.IsDeleted == false)
                .Select(x => x.ProviderToReadDto())
                .FirstOrDefaultAsync();

            return await Task.FromResult(provider ?? null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    public async Task<PaginationResponse<IEnumerable<ReadProviderDto>>> GetAllProvidersAsync(ProviderFilter filter)
    {
        try
        {
            IQueryable<Provider> providers = context.Providers;
            if (filter?.FirstName is not null)
                providers = providers.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            
            if (filter?.LastName is not null)
                providers = providers.Where(x => x.LastName.ToLower().Contains(filter.LastName.ToLower()));
            
            if (filter?.Address is not null)
                providers = providers.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));

            if (filter?.Specialization is not null)
                providers = providers.Where(x => x.Specialization.ToLower().Contains(filter.Specialization.ToLower()));
            
            IQueryable<ReadProviderDto> res = providers
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => x.ProviderToReadDto());

            int totalRecords = await providers.CountAsync();

            return PaginationResponse<IEnumerable<ReadProviderDto>>.Create(
                filter.PageNumber,
                filter.PageSize,
                totalRecords,
                res
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    

}