using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GenerarXML_SAT.Services
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed = false;

        private string _api;

        public ApiClient(string api)
        {
            _api = api;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_api);
            //_httpClient.Timeout = Timeout.InfiniteTimeSpan;
        }

        public ApiClient(string api, string token)
        {
            _api = api;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_api);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BEARER", token);
            //_httpClient.Timeout = Timeout.InfiniteTimeSpan;
        }

        public ApiClient(string api, string token, string key)
        {
            _api = api;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_api);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BEARER", token);
            _httpClient.DefaultRequestHeaders.Add("APIKEY", key);
            //_httpClient.Timeout = Timeout.InfiniteTimeSpan;
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }
                _disposed = true;
            }
        }

        public async Task<string> GetDatos(string endpoint)
        {
            string responseBody = "";

            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                var response = await _httpClient.GetAsync($"{_api}/{endpoint}");
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();
            }

            catch (Exception ex)
            {

            }

            return responseBody;
        }

        public async Task<HttpResponseMessage> PostDatos(string endpoint, string jsonData)
        {

            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_api}/{endpoint}", content);
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
