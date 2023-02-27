using BookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Services.Interfaces;

namespace SimpleBookingSystem.Services
{
    public class BookingService : IBookingInterface
    {
        private readonly LocalDatabaseContext _databaseContext;

        public BookingService(LocalDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Booking GetAllByResourceId(DateTime DateFrom, DateTime DateTo, int BookedQunatity, int ResourceId)
        {
            //Check in database if there is a booking for the selected resoruce
            var existingBookings = _databaseContext.Bookings.Where(resourceId => resourceId.ResourceId == ResourceId).ToList();

            //Get the quantity
            var resource = _databaseContext.Resources.FirstOrDefault(resource => resource.Id == ResourceId);
            var resourceQuantity = resource.Quantity;

            // if there is no booking for that resource 
            if (existingBookings == null)
            {
                // check if the number of quantities is available 
                if (BookedQunatity <= resourceQuantity)
                {
                    // if yes create the booking
                    return CreateBooking(DateFrom, DateTo, BookedQunatity, ResourceId);
                }

                // if no, return back
                else
                {
                    return null;
                }

            }

            else
            {
                // if booking is existing for that resource check the date time and quantity
                return CheckAbiability(DateFrom, DateTo, BookedQunatity, ResourceId, resourceQuantity);

            }

   
        }


        public Booking CheckAbiability(DateTime DateFrom, DateTime DateTo, int BookedQunatity, int ResourceId, int? resourceQuantity)
        {
  
            var existingBookings = _databaseContext.Bookings
                .Where(b => b.ResourceId == ResourceId && b.DateFrom <= DateTo && b.DateTo >= DateFrom)
                .ToList();

            var totalBookedQuantity = 0;
           
            // Check for overlapping periods with existing bookings
            foreach (var item in existingBookings)
            {
                var overlapStartTime = item.DateFrom < DateFrom ? DateFrom : item.DateFrom;
                var overlapEndTime = item.DateTo > DateTo ? DateTo : item.DateTo;
                var overlapQuantity = item.BookedQunatity;

                totalBookedQuantity += overlapQuantity;
            }

            if (totalBookedQuantity + BookedQunatity <= resourceQuantity)
            {
                // Create a new booking record in the database
                return CreateBooking(DateFrom, DateTo, BookedQunatity, ResourceId);
            }
            else
            {
                return null;
            }
        }

        public Booking CreateBooking(DateTime DateFrom, DateTime DateTo, int BookedQunatity, int ResourceId)
        {
            var newBooking = new Booking
            {
                ResourceId = ResourceId,
                DateFrom = DateFrom,
                DateTo = DateTo,
                BookedQunatity = BookedQunatity
            };
            _databaseContext.Bookings.Add(newBooking);
            _databaseContext.SaveChanges();
           
            return newBooking;
        }

        public Resource GetResource(int id)
        {
            return _databaseContext.Resources.FirstOrDefault(r => r.Id == id);
            
        }
    }
}
