using BookingSystem.Models;

namespace SimpleBookingSystem.Services.Interfaces
{
    public interface IBookingInterface
    {
        Booking GetAllByResourceId(DateTime DateFrom, DateTime DateTo, int BookedQunatity, int ResourceId);
        Resource GetResource(int id);
      
    }
}
