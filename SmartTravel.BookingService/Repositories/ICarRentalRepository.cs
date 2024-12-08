using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartTravel.BookingService.DatabaseContext;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Interface;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.BookingService.Repositories
{
    public interface ICarRentalRepository : ICommonInterface<CarRentalEntity>
    {
        Task<IEnumerable<CarRentalEntity>> GetCarRentalsByBookingAsync(int bookingId);
    }

    public class CarRentalRepository : ICarRentalRepository
    {
        private readonly BookingServiceDbContext _context;

        public CarRentalRepository(BookingServiceDbContext context)
        {
            _context = context;
        }
        public async Task<Response> CreateAsync(CarRentalEntity entity)
        {
            if (entity is null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                await _context.CarRentals.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Response(ResponseResultEnum.Success, "Create new car rental successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error creating car rental: {ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                var carRental = await GetByIdAsync(id);

                if (carRental == null)
                    return new Response(ResponseResultEnum.Error, "Cannot find car rental");

                _context.CarRentals.Remove(carRental);
                await _context.SaveChangesAsync();

                return new Response(ResponseResultEnum.Success, "Role deleted successfully", carRental);

            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error deleting car rental: {ex.Message}");
            }
        }

        public async Task<IEnumerable<CarRentalEntity>> GetAllAsync()
        {
            return await _context.CarRentals.ToListAsync();
        }

        public Task<CarRentalEntity> GetByAsync(Expression<Func<CarRentalEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<CarRentalEntity> GetByIdAsync(int id)
        {
            return await _context.CarRentals.FirstOrDefaultAsync(c => c.CarRentalId == id);
        }

        public async Task<IEnumerable<CarRentalEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<CarRentalEntity>();
            }

            return await _context.CarRentals
                                 .Where(e => ids.Contains(e.CarRentalId))
                                 .ToListAsync();
        }

        public Task<CarRentalEntity> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CarRentalEntity>> GetCarRentalsByBookingAsync(int bookingId)
        {
            return await _context.CarRentals.Where(c => c.BookingId == bookingId)?.ToListAsync();
        }

        public async Task<Response> UpdateAsync(CarRentalEntity entity)
        {
            if (entity == null)
                return new Response(ResponseResultEnum.Error, "Entity cannot be null");

            try
            {
                _context.CarRentals.Update(entity);
                await _context.SaveChangesAsync();

                return new Response(ResponseResultEnum.Success, "Car retal updated successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingExtension.LogException(ex);
                return new Response(ResponseResultEnum.Error, $"Error updating Car retal: {ex.Message}");
            }
        }
    }
}
