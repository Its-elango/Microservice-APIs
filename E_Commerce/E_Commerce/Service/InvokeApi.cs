﻿using E_Commerce.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace E_Commerce.Service
{
    public class InvokeApi
    {
        private readonly HttpClient _httpClient;

        public InvokeApi(HttpClient httpClient,IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _httpClient = httpClientFactory.CreateClient("NamedClient");
        }

        public async Task<string> SendToClient<T>(string baseUrl, string app, string path, T model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync($"{baseUrl}/{app}/{path}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error sending the target API: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                // Handle specific HttpRequestException here
                return $"HttpRequestException: {httpRequestException.Message}";
            }
            catch (TaskCanceledException taskCanceledException)
            {
                // Handle TaskCanceledException which can indicate a timeout
                if (!taskCanceledException.CancellationToken.IsCancellationRequested)
                {
                    return $"Request timed out: {taskCanceledException.Message}";
                }
                else
                {
                    return $"Request was canceled: {taskCanceledException.Message}";
                }
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                return $"An error occurred: {ex.Message}";
            }
        }
    }
}
