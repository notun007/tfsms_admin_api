using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Technofair.Utiity.Http
{
    public static class Request<T, TT> where T : class where TT : class
    {
       // private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<T> GetObject(string url)
        {
            T result = null;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    //New: 01.08.2024
                    var response = await httpClient.GetAsync(new Uri(url));
                    response.EnsureSuccessStatusCode();
                    var respnseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(respnseData);
                }
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        public static async Task<List<T>> GetCollecttion(string url)
        {
            List<T> result = null;

            try
            {

                using (var httpClient = new HttpClient())
                {
                    //New: 01.08.2024
                    var response = await httpClient.GetAsync(new Uri(url));
                    response.EnsureSuccessStatusCode();
                    var respnseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<T>>(respnseData);
                    
                }
            }
            catch(Exception exp)
            {
                throw exp;
            }
            return result;
        }

        public static async Task<TT> Post(string apiUrl, T postObject)
        {
            TT result = null;

            try
            {
                using (var client = new HttpClient())
                {
                    var res = JsonConvert.SerializeObject(postObject);
                    var response = await client.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                    {
                        if (x.IsFaulted)
                            throw x.Exception;
                        result = JsonConvert.DeserializeObject<TT>(x.Result);

                    });
                }
            }
            catch(Exception exp)
            {

            }

            return result;
        }
        //DELETE
        public static async Task<TT> Delete(string apiUrl)
        {
            TT result = null;

            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<TT>(x.Result);

                });
            }

            return result;
        }

        public static async Task Put(string apiUrl, T putObject)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }

       public static async Task<T> PostWithJWT(string url, string jwtToken, TT postObject)
        {
            T result = null;
            //var url = apiUrl = "https://localhost:5001/api/second/actiontwo";

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            // Serialize the data to JSON
            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true, // Pretty-print JSON
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Use camelCase properties
            //};
            //string jsonString = Newtonsoft.Json.JsonSerializer.Serialize(postObject);

            //var jsonData = System.Text.Json.JsonSerializer.Serialize(postObject);
            var content = JsonConvert.SerializeObject(postObject);

            // Add the JSON data to the request content
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            // Add the Authorization header with the Bearer token
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            using (var httpClient = new HttpClient())
            {
                
                //var res = JsonConvert.SerializeObject(postObject);
                //var response = await httpClient.PostAsync(url, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);



                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    result = JsonConvert.DeserializeObject<T>(x.Result);

                });

                //Original
                //var response = await httpClient.SendAsync(request);
                //if (response.IsSuccessStatusCode)
                //{
                //    result = await response.Content.ReadAsStringAsync();
                //    Console.WriteLine("API Response: " + result);
                //}
                //else
                //{
                //    Console.WriteLine("Error calling protected API.");
                //}
            }

            return result;
        }
                
    }

}
