using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartTravel.BookingService.DatabaseContext;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Interface;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.BookingService.Repositories
{
    public interface IHotelDetailRepository : ICommonInterface<HotelDetailEntity>
    {
        Task<IEnumerable<HotelDetailEntity>> GetHotelDetailsByBookingAsync(int bookingId);
    }

    public class HotelDetailRepository : IHotelDetailRepository
    {
        private readonly BookingServiceDbContext _context;

        public HotelDetailRepository(BookingServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateAsync(HotelDetailEntity entity)
        {
            if (entity is null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                await _context.HotelDetails.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Hotel detail created successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error creating hotel detail: {ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                var hotelDetail = await GetByIdAsync(id);

                if (hotelDetail == null)
                    return new Response(ResponseResultEnum.Error, "Cannot find hotel detail");

                _context.HotelDetails.Remove(hotelDetail);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Hotel detail deleted successfully", hotelDetail);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error deleting hotel detail: {ex.Message}");
            }
        }

        public async Task<IEnumerable<HotelDetailEntity>> GetAllAsync()
        {
            return await _context.HotelDetails.ToListAsync();
        }

        public Task<HotelDetailEntity> GetByAsync(Expression<Func<HotelDetailEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<HotelDetailEntity> GetByIdAsync(int id)
        {
            return await _context.HotelDetails.FirstOrDefaultAsync(h => h.HotelDetailId == id);
        }

        public async Task<IEnumerable<HotelDetailEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<HotelDetailEntity>();
            }

            return await _context.HotelDetails
                                 .Where(e => ids.Contains(e.HotelDetailId))
                                 .ToListAsync();
        }

        public Task<HotelDetailEntity> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HotelDetailEntity>> GetHotelDetailsByBookingAsync(int bookingId)
        {
            return await _context.HotelDetails.Where(h => h.BookingId == bookingId).ToListAsync();
        }

        public async Task<Response> UpdateAsync(HotelDetailEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                _context.HotelDetails.Update(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Hotel detail updated successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error updating hotel detail: {ex.Message}");
            }
        }
    }
}
