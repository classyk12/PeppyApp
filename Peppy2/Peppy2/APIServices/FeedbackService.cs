using Newtonsoft.Json;
using Peppy2.Constants;
using Peppy2.Helpers;
using Peppy2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Peppy2.APIServices
{
    public class FeedbackService
    {
        //used to get all feedback in the database
        public async Task<List<FeedbackModel>> AllFeedbacks(int PageIndex, int pageSize)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
                var response = await client.GetStringAsync(UrlConstants.feedbackurl);
                var data = JsonConvert.DeserializeObject<ObservableCollection<FeedbackModel>>(response);
                return data.Skip(PageIndex * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }




        //will be used to get the mean feedback
        public async Task<string> MeanFeedback()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
                var response = await client.GetStringAsync(UrlConstants.meanfeedbackurl);
                var data = JsonConvert.DeserializeObject<string>(response);
                return data;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        //will be used to get the mean feedback
        public async Task<string> FeedbackCount()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
                var response = await client.GetStringAsync(UrlConstants.feedbackcounturl);
                var data = JsonConvert.DeserializeObject<string>(response);
                return data;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        //gets user count in the feedback table
        public async Task<string> UserCount()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
                var response = await client.GetStringAsync(UrlConstants.usercountfeedbackurl);
                var data = JsonConvert.DeserializeObject<string>(response);
                return data;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public async Task<bool> SendFeedback(FeedbackModel feedback)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var json = JsonConvert.SerializeObject(feedback);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(UrlConstants.feedbackurl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetUserFeedbackCount(string user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
                var response = await client.GetStringAsync($"{UrlConstants.feedbackurl}?user={user}");
                var data = JsonConvert.DeserializeObject<string>(response);
                return data;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
    }
}
