using SmartTravel.BookingService.Helper.Mapping;
using SmartTravel.BookingService.Models.HotelDetail;
using SmartTravel.BookingService.Repositories;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.BookingService.Services
{
    public interface IHotelDetailBusinessLayer
    {
        Task<Response> CreateAsync(HotelDetailCreateModel request);
        Task<Response> UpdateAsync(HotelDetailUpdateModel request);
        Task<Response> DeleteAsync(int id);
        Task<Response> GetByIdAsync(int id);
        Task<Response> GetHotelDetailsByBookingAsync(int bookingId);
        Task<Response> GetAllAsync();
    }

    public class HotelDetailBusinessLayer : IHotelDetailBusinessLayer
    {
        private readonly IHotelDetailRepository _hotelDetailRepository;
        private readonly IHotelDetailMapping _hotelDetailMapping;

        public HotelDetailBusinessLayer(IHotelDetailRepository hotelDetailRepository, IHotelDetailMapping hotelDetailMapping)
        {
            _hotelDetailRepository = hotelDetailRepository;
            _hotelDetailMapping = hotelDetailMapping;
        }

        public Task<Response> CreateAsync(HotelDetailCreateModel request)
        {
            var entity = (HotelDetailEntity)_hotelDetailMapping.ToEntity(request);
            var response = _hotelDetailRepository.CreateAsync(entity);
            return response;
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = await _hotelDetailRepository.DeleteAsync(id);
            return await Task.FromResult(response);
        }

        public async Task<Response> GetAllAsync()
        {
            var hotelDetails = (List<HotelDetailModel>)_hotelDetailMapping.ToListModels(await _hotelDetailRepository.GetAllAsync());
            if (hotelDetails == null || hotelDetails.Count == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No hotel details found"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, hotelDetails));
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            var hotelDetail = (HotelDetailModel)_hotelDetailMapping.ToModel(await _hotelDetailRepository.GetByIdAsync(id));
            if (hotelDetail == null)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, $"Cannot find hotel detail by id: {id}"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", hotelDetail));
        }

        public async Task<Response> GetHotelDetailsByBookingAsync(int bookingId)
        {
            var hotelDetails = (List<HotelDetailModel>)_hotelDetailMapping.ToListModels(await _hotelDetailRepository.GetHotelDetailsByBookingAsync(bookingId));
            if (hotelDetails == null || hotelDetails.Count == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No hotel details found"));
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, hotelDetails));
        }

        public async Task<Response> UpdateAsync(HotelDetailUpdateModel request)
        {
            if (request == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid hotel detail"));

            var hotelDetailEntity = await _hotelDetailRepository.GetByIdAsync(request.HotelDetailId);

            if (hotelDetailEntity == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the hotel detail"));

            var hotelDetailToUpdate = (HotelDetailEntity)_hotelDetailMapping.ToEntity(hotelDetailEntity, request);
            var response = _hotelDetailRepository.UpdateAsync(hotelDetailToUpdate);
            return await response;
        }
    }
}
