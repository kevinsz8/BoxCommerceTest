using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.ServiceClients
{
	public class HttpService
	{
		private readonly HttpClient _httpClient;

		public HttpService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<T> GetAsync<T>(string url)
		{

			var response = await _httpClient.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(json);
			}
			else
			{
				return default;
			}
		}

		public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request)
		{
			var jsonRequest = JsonConvert.SerializeObject(request);
			var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
			}
			else
			{
				return default;
			}
		}

		public async Task<TResponse> DeleteAsync<TRequest, TResponse>(string url, TRequest request)
		{
			var jsonRequest = JsonConvert.SerializeObject(request);
			var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

			var response = await _httpClient.DeleteAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
			}
			else
			{
				return default;
			}
		}


	}
}
