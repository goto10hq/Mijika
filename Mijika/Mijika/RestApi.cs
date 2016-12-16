using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Mijika
{
    internal class RestApi
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private AuthenticationHeaderValue Authorization => new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(Username + ":" + Password)));
        
        public RestApi(string baseUrl, string username, string password)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            BaseUrl = baseUrl;
            Username = username;
            Password = password;

            Tools.SetJsonDefaultSettings();
        }

        public string Get()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = Authorization;

                var response = client.GetAsync(BaseUrl).Result;

                if (response.IsSuccessStatusCode)
                {                    
                    return response.Content.ReadAsStringAsync().Result;                    
                }
            }

            return null;
        }

        public T Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = Authorization;
                        
                var response = client.GetAsync(url).Result;
                
                if (response.IsSuccessStatusCode)
                {     
                    var value = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    return value;
                }
            }

            return default(T);
        }
        
        public T Post<T>(string url)
        {
            using (var client = new HttpClient())
            {                
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = Authorization;

                var response = client.PostAsync(url, null).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    if (typeof(T) == typeof(HttpResponseHeaders))
                        return (T)Convert.ChangeType(response.Headers, typeof(T));

                    var value = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    return value;
                }
            }

            return default(T);
        }
    }
}
