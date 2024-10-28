using MainApp.Dto_s;
using MainApp.Filter;
using MainApp.Responses;
namespace MainApp.Services.BookingServices;

public interface IBookingService
{
    Task<bool> CreateBookingAsync(CreateBookingDto booking);
    Task<bool> UpdateBookingAsync(UpdateBookingDto booking);
    Task<bool> DeleteBookingAsync(int bookingId);
    Task<ReadBookingDto?> GetBookingByIdAsync(int bookingId);
    Task<PaginationResponse<IEnumerable<ReadBookingDto>>> GetAllBookingsAsync(BookingFilter filter);
    Task<PaginationResponse<IEnumerable<BookingInfoDto>>> GetBookingInfoAsync(BaseFilter filter);
    Task<PaginationResponse<IEnumerable<BookingDetailsDto>>> GetDetailedBookingsAsync(BaseFilter filter);
}