using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartTravel.BookingService.DatabaseContext;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Interface;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.BookingService.Repositories
{
    public interface IBookingRepository : ICommonInterface<BookingEntity>
    {
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly BookingServiceDbContext _context;

        public BookingRepository(BookingServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateAsync(BookingEntity entity)
        {
            if (entity is null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                await _context.Bookings.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Booking created successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error creating booking: {ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                var booking = await GetByIdAsync(id);

                if (booking == null)
                    return new Response(ResponseResultEnum.Error, "Cannot find booking");

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Booking deleted successfully", booking);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error deleting booking: {ex.Message}");
            }
        }

        public async Task<IEnumerable<BookingEntity>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<BookingEntity> GetByIdAsync(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<Response> UpdateAsync(BookingEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                _context.Bookings.Update(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Booking updated successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error updating booking: {ex.Message}");
            }
        }
        public Task<BookingEntity> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<BookingEntity> GetByAsync(Expression<Func<BookingEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookingEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<BookingEntity>();
            }

            return await _context.Bookings
                                 .Where(e => ids.Contains(e.BookingId))
                                 .ToListAsync();
        }
    }
}
