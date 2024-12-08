using Polly;
using Polly.Registry;
using SmartTravel.BookingService.Helper.Mapping;
using SmartTravel.BookingService.Models;
using SmartTravel.BookingService.Models.Booking;
using SmartTravel.BookingService.Repositories;
using SmartTravel.Shared.Entities;
using SmartTravel.Shared.Models;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.BookingService.Services
{
    public interface IBookingBusinessLayer
    {
        Task<Response> GetUserById(int id);
        Task<Response> GetUsersByIds(IEnumerable<int> ids);
        Task<Response> CreateAsync(BookingCreateModel request);
        Task<Response> UpdateAsync(BookingUpdateModel request);
        Task<Response> DeleteAsync(int id);
        Task<Response> GetByIdAsync(int id);
        Task<Response> GetByIdsAsync(IEnumerable<int> ids);
        Task<Response> GetAllAsync();
    }

    public class BookingBusinessLayer : IBookingBusinessLayer
    {
        private readonly HttpClient _httpClient;
        private ResiliencePipelineProvider<string> _resiliencePipeline;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingMapping _bookingMapping;

        public BookingBusinessLayer(HttpClient httpClient, ResiliencePipelineProvider<string> resiliencePipeline, 
            IBookingRepository bookingRepository, IBookingMapping bookingMapping)
        {
            _httpClient = httpClient;
            _resiliencePipeline = resiliencePipeline;
            _bookingRepository = bookingRepository;
            _bookingMapping = bookingMapping;
        }

        public async Task<Response> GetUserById(int id)
        {
            var user = await _httpClient.GetAsync($"/api/User/getUserById/{id}");
            if (!user.IsSuccessStatusCode)
                return new Response(ResponseResultEnum.Error, "Can not find user");

            var response = await user.Content.ReadFromJsonAsync<Response>();

            return response;
        }

        public async Task<Response> GetUsersByIds(IEnumerable<int> ids)
        {
            var queryString = string.Join("&", ids.Select(id => $"ids={id}"));

            var users = await _httpClient.GetAsync($"/api/User/getUsersByIds?{queryString}");

            if (!users.IsSuccessStatusCode)
                return new Response(ResponseResultEnum.Error, "Can not find user");

            var response = await users.Content.ReadFromJsonAsync<Response>();

            return response;
        }

        public Task<Response> CreateAsync(BookingCreateModel request)
        {
            var entity = (BookingEntity)_bookingMapping.ToEntity(request);
            var response = _bookingRepository.CreateAsync(entity);
            return response;
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = await _bookingRepository.DeleteAsync(id);
            return await Task.FromResult(response);
        }

        public async Task<Response> GetAllAsync()
        {
            var bookings = (List<BookingModel>)_bookingMapping.ToListModels(await _bookingRepository.GetAllAsync());

            if (bookings == null || bookings.Count() == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No bookings found"));
            }

            var retryPipeline = _resiliencePipeline.GetPipeline("my-retry-pipeline");

            var listUserIds = bookings.Select(b => b.UserId).ToList();

            var users = retryPipeline.ExecuteAsync(async token => await GetUsersByIds(listUserIds));

            if (users.Result != null && users.Result.responseResult == ResponseResultEnum.Success)
            {
                var userModels = users.Result.collection;

                bookings = (List<BookingModel>)_bookingMapping.MapToMissingPropertyModel(bookings, (List<BaseModel>)userModels);
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, bookings));
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            var booking = (BookingModel)_bookingMapping.ToModel(await _bookingRepository.GetByIdAsync(id));
            if (booking == null)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, $"Cannot find booking by id: {id}"));
            }

            var retryPipeline = _resiliencePipeline.GetPipeline("my-retry-pipeline");

            var user = retryPipeline.ExecuteAsync(async token => await GetUserById(booking.UserId));

            if (user.Result != null && user.Result.responseResult != ResponseResultEnum.Error && user.Result.data != null)
            {
                booking.User = (UserModel)_bookingMapping.ToReferedModel(booking, (BaseEntity)user.Result.data);
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", booking));
        }

        public async Task<Response> UpdateAsync(BookingUpdateModel request)
        {
            if (request == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Invalid booking"));

            var bookingEntity = await _bookingRepository.GetByIdAsync(request.BookingId);

            if (bookingEntity == null)
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "Cannot find the booking"));

            var bookingToUpdate = (BookingEntity)_bookingMapping.ToEntity(bookingEntity, request);
            var response = _bookingRepository.UpdateAsync(bookingToUpdate);
            return await response;
        }

        public async Task<Response> GetByIdsAsync(IEnumerable<int> ids)
        {
            var bookings = (List<BookingModel>)_bookingMapping.ToListModels(await _bookingRepository.GetByIdsAsync(ids));

            if (bookings == null || bookings.Count() == 0)
            {
                return await Task.FromResult(new Response(ResponseResultEnum.Error, "No bookings found"));
            }

            var retryPipeline = _resiliencePipeline.GetPipeline("my-retry-pipeline");

            var listUserIds = bookings.Select(b => b.UserId).Distinct().ToList();

            var users = retryPipeline.ExecuteAsync(async token => await GetUsersByIds(listUserIds));

            if (users.Result != null && users.Result.responseResult == ResponseResultEnum.Success)
            {
                var userModels = users.Result.collection;

                bookings = (List<BookingModel>)_bookingMapping.MapToMissingPropertyModel(bookings, (List<BaseModel>)userModels);
            }

            return await Task.FromResult(new Response(ResponseResultEnum.Success, "", null, bookings));
        }
    }
}
