using Logistics_Management_System.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logistics_Management_System.Service
{
    public class InvokeApi
    {
        private readonly HttpClient _httpClient;

        public InvokeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendDeliverydetails(string baseUrl, UpdateDeliveryStatus deliveryStatus)
        {
            var json = JsonConvert.SerializeObject(deliveryStatus);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{baseUrl}/Admin/UpdateOrderStatus", content);

            if (response.IsSuccessStatusCode)
            {
                return "Delivery details sent successfully";
            }
            else
            {
                return "Error sending the target API";
            }
        }
    }
}
