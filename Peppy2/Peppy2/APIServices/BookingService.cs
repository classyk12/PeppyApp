using Newtonsoft.Json;
using Peppy2.Constants;
using Peppy2.Helpers;
using Peppy2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Peppy2.APIServices
{
   public class BookingService
    {
        public async Task<List<BookingsModel>> AllBookings()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response = await client.GetStringAsync(UrlConstants.bookingsurl);
            var data = JsonConvert.DeserializeObject<List<BookingsModel>>(response);
            return data;
        }

        public async Task<string> GetBookingCount(string user)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response = await client.GetStringAsync($"{UrlConstants.bookingsurl}?user={user}");
            var data = JsonConvert.DeserializeObject<string>(response);
            return data;
        }

        public async Task<string> GetCleanerCount(string identity)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response = await client.GetStringAsync($"{UrlConstants.bookingsurl}?identity={identity}");
            var data = JsonConvert.DeserializeObject<string>(response);
            return data;
        }

        public async Task<BookingsModel> GetBookingDate(string userkey)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response =  await client.GetStringAsync($"{UrlConstants.bookingsurl}?userkey={userkey}");
            var data = JsonConvert.DeserializeObject<BookingsModel>(response);
            return data;
        }

        public async Task<List<BookingsModel>> GetBookingByUserId(string UserId)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response = await client.GetStringAsync($"{UrlConstants.bookingsurl}?UserId={UserId}");
            var data = JsonConvert.DeserializeObject<List<BookingsModel>>(response);
            return data;
        }

        public async Task<string> CleanerCount(string cleanerId)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response = await client.GetStringAsync($"{UrlConstants.bookingsurl}?cleanerId={cleanerId}");
            var data = JsonConvert.DeserializeObject<string>(response);
            return data;
        }

        public async Task<bool> SaveBooking(BookingsModel booking)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var json = JsonConvert.SerializeObject(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response =await client.PostAsync(UrlConstants.bookingsurl, content);
            return response.IsSuccessStatusCode;

        }

        public async Task<bool> UpdateBooking(string id, BookingsModel booking)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var json = JsonConvert.SerializeObject(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response =  await client.PutAsync(UrlConstants.bookingsurl2 + id, content);
            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteBooking(string id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);          
            var response = await client.DeleteAsync(UrlConstants.bookingsurl2 + id);
            return response.IsSuccessStatusCode;

        }

        public async Task<List<BookingsModel>> GetCompletedBookings(string UserId)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response = await client.GetStringAsync($"{UrlConstants.donebooking}?UserId={UserId}");
            var data = JsonConvert.DeserializeObject<List<BookingsModel>>(response);
            return data;
        }

        public async Task<List<BookingsModel>> GetUnCompletedBookings(string UserId)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var response = await client.GetStringAsync($"{UrlConstants.pendingbooking}?UserId={UserId}");
            var data = JsonConvert.DeserializeObject<List<BookingsModel>>(response);
            return data;
        }


    }
}
