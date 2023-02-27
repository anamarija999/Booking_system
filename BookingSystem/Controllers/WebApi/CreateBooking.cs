using SimpleBookingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using SimpleBookingSystem.Services.Interfaces;

namespace SimpleBookingSystem.Controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateBooking : ControllerBase
    {
        private readonly IBookingInterface _bookingService;
        public CreateBooking (IBookingInterface bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpPost, Route("")]
        public Booking Get([FromForm] Booking booking)
        {
            return _bookingService.GetAllByResourceId(booking.DateFrom, booking.DateTo, booking.BookedQunatity, booking.ResourceId);
        
        }
        [HttpGet, Route("")]
        public Resource GetResource(int id)
        {
            return _bookingService.GetResource(id); ;
        } 
       
    }
}
