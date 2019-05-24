using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Peppy2.Constants;
using Peppy2.Helpers;
using Peppy2.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace Peppy2.APIServices
{
  public class UserService
    {
        public HttpClient httpclient = new HttpClient();
        //This method handles User Registration
        public async Task<bool> RegisterUser(RegisterModel model)
        {      
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //posts to the url and sends the content to the Url
            var response = await httpclient.PostAsync(UrlConstants.registerurl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UserLogin(string Username, string password)
        {
            try
            {
                var keyvaluepair = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username",Username),
                new KeyValuePair<string, string>("password",password),
                new KeyValuePair<string, string>("grant_type","password"),
            };
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, UrlConstants.loginurl)
                {
                    Content = new FormUrlEncodedContent(keyvaluepair)
                };
                var response = await httpclient.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();
                //converts the response body from login method into string at runtime
               
                JObject jobject = JsonConvert.DeserializeObject<dynamic>(content);
                //stores the Deseralized string into a variable so it can be used to store settings
                var Accesstoken = jobject.Value<string>("access_token");
                var email = jobject.Value<string>("Email");
                var date = jobject.Value<string>("DateJoined");
                var profileimage = jobject.Value<string>("ImagePath");
                var phonenumber = jobject.Value<string>("PhoneNumber");
                var Id = jobject.Value<string>("UserId");

                 //stores response in settings
                Settings.AccessToken = Accesstoken;
                Settings.UserName = Username;
                Settings.Password = password;
                Settings.Date = date;
                Settings.Email = email;
                Settings.ProfileImagePath = string.Format(UrlConstants.url2, profileimage.Substring(1));
                Settings.PhoneNumber = phonenumber;
                Settings.UserId = Id;

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }



    }
    }

