using SmartTravel.BookingService.Helper.Mapping;
using SmartTravel.BookingService.Models.CarRental;
using SmartTravel.BookingService.Repositories;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.BookingService.Services
{
    public interface ICarRentalBusinessLayer
    {
        Task<Response> CreateAsync(CarRentalCreateModel request);
        Task<Response> UpdateAsync(CarRentalUpdateModel request);
        Task<Response> DeleteAsync(int id);
        Task<Response> GetByIdAsync(int id);
        Task<Response> GetCarRentalsByBookingAsync(int bookingId);
        Task<Response> GetAllAsync();
    }

    public class CarRentalBusinessLayer : ICarRentalBusinessLayer
    {
        private readonly ICarRentalRepository _carRentalRepository;
        private readonly ICarRentalMapping _carRentalMapping;

        public CarRentalBusinessLayer(ICarRentalRepository carRentalRepository, ICarRentalMapping carRentalMapping)
        {
            _carRentalRepository = carRentalRepository;
            _carRentalMapping = carRentalMapping;
        }

        public Task<Response> CreateAsync(CarRentalCreateModel request)
        {
            var entity = (CarRentalEntity)_carRentalMapping.ToEntity(request);

            var response  = _carRentalRepository.CreateAsync(entity);

            return response;
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = await _carRentalRepository.DeleteAsync(id);

            return await Task.FromResult(response);
        }

        public async Task<Response> GetAllAsync()
        {
            var listCarRentals = (List<CarRentalModel>)_carRentalMapping.ToListModels(await _carRentalRepository.GetAllAsync());

            if (listCarRentals == null || listCarRentals.Count == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No roles found"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, listCarRentals));
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            var role = (CarRentalModel)_carRentalMapping.ToModel(await _carRentalRepository.GetByIdAsync(id));

            if (role == null)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, $"Cannot find role by id: {id}"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", role));
        }

        public async Task<Response> GetCarRentalsByBookingAsync(int bookingId)
        {
            var listCarRentals = (List<CarRentalModel>)_carRentalMapping.ToListModels(await _carRentalRepository.GetCarRentalsByBookingAsync(bookingId));

            if (listCarRentals == null || listCarRentals.Count == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No roles found"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, listCarRentals));
        }

        public async Task<Response> UpdateAsync(CarRentalUpdateModel request)
        {
            if (request == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid role"));

            var carRentalEntity = await _carRentalRepository.GetByIdAsync((int)request.CartRentalId);

            if (carRentalEntity == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the role"));

            var carRentalToUpdate = (CarRentalEntity)_carRentalMapping.ToEntity((BaseEntity)carRentalEntity, request);

            var response = _carRentalRepository.UpdateAsync(carRentalToUpdate);

            return await response;
        }
    }
}
