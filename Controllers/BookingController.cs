using MainApp.Dto_s;
using MainApp.Entities;
using MainApp.Filter;
using MainApp.Responses;
using MainApp.Services.BookingServices;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("/api/bookings/")]
public sealed class BookingController(IBookingService bookingService):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto bookingDto)
    {
        bool res = await bookingService.CreateBookingAsync(bookingDto);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingDto bookingDto)
    {
        bool res = await bookingService.UpdateBookingAsync(bookingDto);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        bool res = await bookingService.DeleteBookingAsync(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        ReadBookingDto? booking = await bookingService.GetBookingByIdAsync(id);
        return booking is not null
            ? Ok(ApiResponse<ReadBookingDto?>.Success(null, booking))
             : NotFound(ApiResponse<ReadBookingDto?>.Fail(null, null));
    }


    [HttpGet]
    public async Task<IActionResult> GetBookings([FromQuery] BookingFilter filter)
    {
        PaginationResponse<IEnumerable<ReadBookingDto>> res = await bookingService.GetAllBookingsAsync(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ReadBookingDto>>>.Success(null, res));
    }

    [HttpGet("booking-info")]
    public async Task<IActionResult> GetBookingInfo([FromQuery] BaseFilter filter)
    {
        PaginationResponse<IEnumerable<BookingInfoDto>> res = await bookingService.GetBookingInfoAsync(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<BookingInfoDto>>>.Success(null, res));
    }
    
    [HttpGet("detailed-bookings")]
    public async Task<IActionResult> GetDetailedBookings([FromQuery] BaseFilter filter)
    {
        PaginationResponse<IEnumerable<BookingDetailsDto>> res = await bookingService.GetDetailedBookingsAsync(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<BookingDetailsDto>>>.Success(null, res));
    }
}