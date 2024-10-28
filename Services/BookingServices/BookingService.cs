using MainApp.DataContexts;
using MainApp.Dto_s;
using MainApp.Entities;
using MainApp.ExtansionMethod.Mapper;
using MainApp.Filter;
using MainApp.Responses;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Services.BookingServices;

public sealed class BookingService(DataContext context):IBookingService
{
    public async Task<bool> CreateBookingAsync(CreateBookingDto booking)
    {
        try
        {
            await context.Bookings.AddAsync(booking.CreateDtoToBooking());
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> UpdateBookingAsync(UpdateBookingDto booking)
    {
        try
        {
            Booking? existingBooking = await context.Bookings.FirstOrDefaultAsync(x => x.Id == booking.Id && x.IsDeleted == false);
            if (existingBooking is null) return await Task.FromResult(false);

            context.Bookings.Update(existingBooking.UpdateDtoToBooking(booking));
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteBookingAsync(int bookingId)
    {
        try
        {
            Booking? existingBooking = await context.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId && x.IsDeleted == false);
            if (existingBooking is null) return await Task.FromResult(false);

            existingBooking.DeleteDtoToBooking();
            return await context.SaveChangesAsync() > 0;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<ReadBookingDto?> GetBookingByIdAsync(int bookingId)
    {
        try
        {
            ReadBookingDto? booking = await context.Bookings.Where(x => x.Id == bookingId && x.IsDeleted == false)
                .Select(x=>x.BookingToReadDto()).FirstOrDefaultAsync();

            return await Task.FromResult(booking ?? null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<PaginationResponse<IEnumerable<ReadBookingDto>>> GetAllBookingsAsync(BookingFilter filter)
    {
        try
        {
            IQueryable<Booking> bookings = context.Bookings;
            if (filter?.StartDateTime is not null)
                bookings = bookings.Where(x => x.StartDateTime >= filter.StartDateTime);
            
            if (filter?.EndDateTime is not null)
                bookings = bookings.Where(x => x.EndDateTime >= filter.EndDateTime);
            
            

            IQueryable<ReadBookingDto> res = bookings.Skip((filter!.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).Select(x => x.BookingToReadDto());

            int totalRecords = await bookings.CountAsync();
            
            return await Task.FromResult(PaginationResponse<IEnumerable<ReadBookingDto>>.Create(filter.PageNumber, filter.PageSize,
                totalRecords, res));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    public async Task<PaginationResponse<IEnumerable<BookingInfoDto>>> GetBookingInfoAsync(BaseFilter filter)
    {
        try
        {
            IQueryable<BookingInfoDto> infos = from booking in context.Bookings
                join user in context.Users on booking.UserId equals user.Id
                join provider in context.Providers on booking.ProviderId equals provider.Id
                join serviceInfo in context.ServiceInfos on booking.ServiceInfoId equals serviceInfo.Id
                select new BookingInfoDto()
                {
                    Id = booking.Id,
                    UserFirstName = user.FirstName,
                    UserLastName = user.LastName,
                    ProviderName = string.Concat(provider.FirstName, " ", provider.LastName),
                    ServiceName = serviceInfo.Name,
                    StartDateTime = booking.StartDateTime,
                    EndDateTime = booking.EndDateTime,
                    TotalPrice = booking.TotalPrice
                };
            IQueryable<BookingInfoDto> res = infos.Skip((filter!.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
        
            int totalRecords = await infos.CountAsync();

            return await Task.FromResult(PaginationResponse<IEnumerable<BookingInfoDto>>.Create(filter.PageNumber, filter.PageSize,
                totalRecords, res));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    public async Task<PaginationResponse<IEnumerable<BookingDetailsDto>>> GetDetailedBookingsAsync(BaseFilter filter)
    {
        try
        {
            IQueryable<BookingDetailsDto> bookingDetailsDto = from booking in context.Bookings
                join user in context.Users on booking.UserId equals user.Id
                join provider in context.Providers on booking.ProviderId equals provider.Id
                join serviceInfo in context.ServiceInfos on booking.ServiceInfoId equals serviceInfo.Id
                join schedule in context.Schedules on provider.Id equals schedule.ProviderId
                select new BookingDetailsDto
                {
                    UserName = string.Concat(user.FirstName, " ", user.LastName),
                    BookingEndDateTime = booking.StartDateTime,
                    BookingStartDateTime = booking.EndDateTime,
                    ServiceName = serviceInfo.Name,
                    ProviderName = string.Concat(provider.FirstName, " ", provider.LastName),
                    Address = provider.Address,
                    Specialization = provider.Specialization,
                    ScheduleStartTime = schedule.StartTime,
                    ScheduleEndTime = schedule.EndTime
                };
            IQueryable<BookingDetailsDto> res = bookingDetailsDto.Skip((filter!.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
            
            int totalRecords = await bookingDetailsDto.CountAsync();
            
            return await Task.FromResult(PaginationResponse<IEnumerable<BookingDetailsDto>>.Create(filter.PageNumber, filter.PageSize,
                totalRecords, res));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    

    
    
}