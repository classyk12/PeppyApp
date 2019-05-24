using Newtonsoft.Json;
using Peppy2.Constants;
using Peppy2.Helpers;
using Peppy2.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Peppy2.APIServices
{
   public class CleanerService
    {
        public async Task<List<CleanerModel>> GetCleaners(string location)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.AccessToken);
                var response =await client.GetStringAsync($"{UrlConstants.cleanerurl}?location={location}");
                var data = JsonConvert.DeserializeObject<List<CleanerModel>>(response);
                return data;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
    }
}
