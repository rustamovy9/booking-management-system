using MainApp.Dto_s;
using MainApp.Entities;

namespace MainApp.ExtansionMethod.Mapper;

public static class BookingMapper
{
    public static ReadBookingDto BookingToReadDto(this Booking booking)
    {
        return new ReadBookingDto()
        {
            Id = booking.Id,
            UserId = booking.UserId,
            ProviderId = booking.ProviderId,
            ServiceInfoId = booking.ServiceInfoId,
            StartDateTime = booking.StartDateTime,
            EndDateTime = booking.EndDateTime,
            TotalPrice = booking.TotalPrice
        };
    }

    public static Booking UpdateDtoToBooking(this Booking booking, UpdateBookingDto updateDto)
    {
        booking.Id = updateDto.Id;
        booking.UserId = updateDto.UserId;
        booking.ProviderId = updateDto.ProviderId;
        booking.ServiceInfoId = updateDto.ServiceInfoId;
        booking.StartDateTime = updateDto.StartDateTime; 
        booking.EndDateTime = updateDto.EndDateTime;
        booking.TotalPrice = updateDto.TotalPrice;
        booking.UpdatedAt = DateTime.UtcNow;
        booking.Version += 1;
        return booking;
    }

    public static Booking CreateDtoToBooking(this CreateBookingDto booking)
    {
        return new Booking()
        {
            UserId = booking.UserId,
            ProviderId = booking.ProviderId,
            ServiceInfoId = booking.ServiceInfoId,
            StartDateTime = booking.StartDateTime,
            EndDateTime = booking.EndDateTime,
            TotalPrice = booking.TotalPrice,
            CreatedAt = DateTime.UtcNow,
            Version = 0
        };
    }

    public static Booking DeleteDtoToBooking(this Booking booking)
    {
        booking.IsDeleted = true;
        booking.DeletedAt = DateTime.UtcNow;
        booking.Version += 1;
        return booking;
    }
}