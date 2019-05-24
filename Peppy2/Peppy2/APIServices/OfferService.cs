using Newtonsoft.Json;
using Peppy2.Constants;
using Peppy2.Helpers;
using Peppy2.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Peppy2.APIServices
{
   public class OfferService
    {
        public async Task<List<OfferModel>> AllOffers()
        {
            using (var httpclient = new HttpClient()) 
            {

                try
                {
                    httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
                    var response = await httpclient.GetStringAsync(UrlConstants.offerurl);
                    var data = JsonConvert.DeserializeObject<List<OfferModel>>(response);
                    return data;
                }
                catch (Exception ex)
                {

                    throw ex.InnerException;
                }
        }
        }
    }
}
