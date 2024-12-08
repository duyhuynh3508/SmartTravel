using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartTravel.BookingService.DatabaseContext;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Interface;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.BookingService.Repositories
{
    public interface IFlightDetailRepository : ICommonInterface<FlightDetailEntity>
    {
        Task<IEnumerable<FlightDetailEntity>> GetFlightDetailsByBookingAsync(int bookingId);
    }

    public class FlightDetailRepository : IFlightDetailRepository
    {
        private readonly BookingServiceDbContext _context;

        public FlightDetailRepository(BookingServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateAsync(FlightDetailEntity entity)
        {
            if (entity is null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                await _context.FlightDetails.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Flight detail created successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error creating flight detail: {ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                var flightDetail = await GetByIdAsync(id);

                if (flightDetail == null)
                    return new Response(ResponseResultEnum.Error, "Cannot find flight detail");

                _context.FlightDetails.Remove(flightDetail);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Flight detail deleted successfully", flightDetail);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error deleting flight detail: {ex.Message}");
            }
        }

        public async Task<IEnumerable<FlightDetailEntity>> GetAllAsync()
        {
            return await _context.FlightDetails.ToListAsync();
        }

        public Task<FlightDetailEntity> GetByAsync(Expression<Func<FlightDetailEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<FlightDetailEntity> GetByIdAsync(int id)
        {
            return await _context.FlightDetails.FirstOrDefaultAsync(f => f.FlightDetailId == id);
        }

        public async Task<IEnumerable<FlightDetailEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<FlightDetailEntity>();
            }

            return await _context.FlightDetails
                                 .Where(e => ids.Contains(e.FlightDetailId))
                                 .ToListAsync();
        }

        public Task<FlightDetailEntity> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FlightDetailEntity>> GetFlightDetailsByBookingAsync(int bookingId)
        {
            return await _context.FlightDetails.Where(f => f.BookingId == bookingId).ToListAsync();
        }

        public async Task<Response> UpdateAsync(FlightDetailEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                _context.FlightDetails.Update(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Flight detail updated successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error updating flight detail: {ex.Message}");
            }
        }
    }
}
