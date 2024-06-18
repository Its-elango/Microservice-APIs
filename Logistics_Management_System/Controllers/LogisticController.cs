using Logistics_Management_System.Models;
using Logistics_Management_System.Respository;
using Logistics_Management_System.Service;
using LogisticsManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "Shipper")]
    public class LogisticController : ControllerBase
    {
        private readonly ILogistics _logistics;
        private readonly InvokeApi _invokeApi;
        private readonly IConfiguration _configuration;

        public LogisticController(ILogistics logistics,InvokeApi invokeApi,IConfiguration configuration)
        {
            _logistics = logistics;
            _invokeApi = invokeApi;
            _configuration = configuration;
        }

        #region GET Methods

        [HttpGet("GetShippers")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetShippers()
        {
            var shippers = await _logistics.GetShippers();
            return shippers != null ? Ok(shippers) : BadRequest("No data found");
        }


        [HttpGet("GetShipperById")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetShipperById(int shipperId)
        {
            var shipper=await _logistics.GetShipper(shipperId);
            return shipper != null ? Ok(shipper) : BadRequest("No shipper was found for that ID");
        }

        [HttpGet("GetOrdersForShipper")]
        public async Task<IActionResult> GetShipperOrders(int shipperId)
        {
            var shipperOrder=await _logistics.GetShipperOrders(shipperId);
            return shipperOrder != null ? Ok(shipperOrder) : BadRequest("No orders for your area");
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _logistics.GetOrders();
            return orders != null ? Ok(orders) : BadRequest("No orders were found");
        }

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _logistics.GetOrder(orderId);
            return order != null ? Ok(order) : BadRequest("No order was found");
        }


        [HttpGet("GetShipments")]
        public async Task<IActionResult> GetShipments()
        {
            var shipments = await _logistics.GetShipments();
            return shipments != null ? Ok(shipments) : BadRequest("No shipments were found");
        }

        [HttpGet("GetShipmentById")]
        public async Task<IActionResult> GetShipmentById(int shipmentId)
        {
            var shipment = await _logistics.GetShipment(shipmentId);
            return shipment != null ? Ok(shipment) : BadRequest("No shipment was found");
        }

        [AllowAnonymous]
        [HttpGet("TrackOrder")]
        public async Task<IActionResult> CheckOrderStatus(int orderId)
        {
            var orderStatus=await _logistics.CheckOrderStatus(orderId);
            return orderStatus != null ? Ok(orderStatus) : BadRequest("No order was found for that ID");

        }

        #endregion GET methods

        #region POST Methods 
        [HttpPost("AddOrder")]
        public async Task<IActionResult> InsertOrder(Order order)
        {
            try
            {
                string resultMessage = await _logistics.AddOrder(order);
                return Ok(resultMessage);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if you have a logging mechanism
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while inserting the order: {ex.Message}");
            }
        }


        [HttpPost("AddShipper")]
        public async Task<string> AddShipper(Shipper shipper)
        {



            return await _logistics.AddShipper(shipper); ;
              
        }


        //[HttpPost("AddShipment")]
        //public async Task<IActionResult> AddShipment(Shipment shipment)
        //{
        //    bool IsShipmentAdded=await _logistics.AddShipment(shipment);
        //    return IsShipmentAdded ? Ok("Shipment added successfully") : BadRequest("Something went wrong");
        //}

        #endregion POST Methods

        #region PUT Methods 

        [HttpPut("UpdateShipper")]
        public async Task<string> UpdateShipper(Shipper shipper)
        {
           return await _logistics.UpdateShipper(shipper);
        }

        [HttpPut("TakeOrder")]
        public async Task<string> TakeOrder(int shipperId,int orderId)
        {
            return await _logistics.TakeOrder(shipperId, orderId);
        }

        [HttpPut("ApproveShipper")]
        public async Task<string> ApproveShipper(ShipperApprovel shipperApprovel)
        {
           return await _logistics.ApproveShipper(shipperApprovel);
        }

        [HttpPut("UpdateShipment")]
        public async Task<string> UpdateShipment(Shipment shipment)
        {
            return await _logistics.UpdateShipment(shipment);
        }


        [HttpPut("UpdateDeliveryStatus")]
        public async Task<IActionResult> UpdateDeliveryStatus(UpdateDeliveryStatus deliveryStatus)
        {
            string baseUrl = _configuration["ApiClient"] ??"";
            var result = await _logistics.UpdateDeliveryStatus(deliveryStatus);
            var message = result == "Delivery status updated successfully" ? await _invokeApi.SendDeliverydetails(baseUrl,deliveryStatus) : "Something went wrong";

            var response = new
            {
                Result = result,
                Message = message
            };

            return Ok(response);
        }




        //[HttpPut("UpdateShipping")]
        //public async Task<IActionResult> UpdateShipping(int orderId,int shipperId)
        //{
        //    Is
        //    return Ok();
        //}

        #endregion PUT Methods
    }
}
