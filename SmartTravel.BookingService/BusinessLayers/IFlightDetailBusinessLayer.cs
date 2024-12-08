using SmartTravel.BookingService.Helper.Mapping;
using SmartTravel.BookingService.Models.FlightDetail;
using SmartTravel.BookingService.Repositories;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.BookingService.Services
{
    public interface IFlightDetailBusinessLayer
    {
        Task<Response> CreateAsync(FlightDetailCreateModel request);
        Task<Response> UpdateAsync(FlightDetailUpdateModel request);
        Task<Response> DeleteAsync(int id);
        Task<Response> GetByIdAsync(int id);
        Task<Response> GetFlightDetailsByBookingAsync(int bookingId);
        Task<Response> GetAllAsync();
    }
    public class FlightDetailBusinessLayer : IFlightDetailBusinessLayer
    {
        private readonly IFlightDetailRepository _flightDetailRepository;
        private readonly IFlightDetailMapping _flightDetailMapping;

        public FlightDetailBusinessLayer(IFlightDetailRepository flightDetailRepository, IFlightDetailMapping flightDetailMapping)
        {
            _flightDetailRepository = flightDetailRepository;
            _flightDetailMapping = flightDetailMapping;
        }

        public Task<Response> CreateAsync(FlightDetailCreateModel request)
        {
            var entity = (FlightDetailEntity)_flightDetailMapping.ToEntity(request);
            var response = _flightDetailRepository.CreateAsync(entity);
            return response;
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = await _flightDetailRepository.DeleteAsync(id);
            return await Task.FromResult(response);
        }

        public async Task<Response> GetAllAsync()
        {
            var flightDetails = (List<FlightDetailModel>)_flightDetailMapping.ToListModels(await _flightDetailRepository.GetAllAsync());
            if (flightDetails == null || flightDetails.Count == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No flight details found"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, flightDetails));
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            var flightDetail = (FlightDetailModel)_flightDetailMapping.ToModel(await _flightDetailRepository.GetByIdAsync(id));
            if (flightDetail == null)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, $"Cannot find flight detail by id: {id}"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", flightDetail));
        }

        public async Task<Response> GetFlightDetailsByBookingAsync(int bookingId)
        {
            var flightDetails = (List<FlightDetailModel>)_flightDetailMapping.ToListModels(await _flightDetailRepository.GetFlightDetailsByBookingAsync(bookingId));
            if (flightDetails == null || flightDetails.Count == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No flight details found"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, flightDetails));
        }

        public async Task<Response> UpdateAsync(FlightDetailUpdateModel request)
        {
            if (request == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid flight detail"));

            var flightDetailEntity = await _flightDetailRepository.GetByIdAsync(request.FlightDetailId);

            if (flightDetailEntity == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the flight detail"));

            var flightDetailToUpdate = (FlightDetailEntity)_flightDetailMapping.ToEntity(flightDetailEntity, request);
            var response = _flightDetailRepository.UpdateAsync(flightDetailToUpdate);
            return await response;
        }
    }
}
