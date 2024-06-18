using E_Commerce.Model;
using E_Commerce.Repository;
using E_Commerce.Repository.Interface;
using E_Commerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace E_Commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomer _customer;
        private readonly InvokeApi _invokeApi;
        private readonly IConfiguration _configuration;
        public CustomerController(ICustomer customer,InvokeApi invokeApi,IConfiguration configuration)
        {
            _customer = customer;
            _invokeApi = invokeApi;
            _configuration = configuration;
        }

        #region GetMethods

        [HttpGet("GetCustomerProfile/{UserId}")]

        public async Task<IActionResult> GetCustomerProfile(int UserId)
        {
            try
            {
                var customerList = await _customer.GetMyProfile(UserId);

                if (customerList != null && customerList.Any())
                {
                    return Ok(customerList);
                }
                else
                {
                    return NotFound("No customers found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetByOrder/{OrderId}")]

        public async Task<IActionResult> GetByOrder(int OrderId)
        {
            try
            {
                var orderList = await _customer.GetOrderDetails(OrderId);

                if (orderList != null)
                {
                    return Ok(orderList);
                }
                else
                {
                    return NotFound("No customers found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetAllActiveProduct")]
        public async Task<IActionResult> GetAllActiveProduct()
        {
            try
            {
                var productList = await _customer.GetAllActiveProducts();

                if (productList != null && productList.Any())
                {
                    return Ok(productList);
                }
                else
                {
                    return NotFound("No Products found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllActiveProductById")]
        public async Task<IActionResult> GetAllActiveProductById(int ProductId)
        {
            try
            {
                var productList = await _customer.GetAllActiveProductsById(ProductId);

                if (productList != null && productList.Any())
                {
                    return Ok(productList);
                }
                else
                {
                    return NotFound("No Products found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetByCategory")]
        public async Task<IActionResult> GetByCategory(string? CategoryName)
        {
            try
            {
                var productList = await _customer.GetByCategory(CategoryName);

                if (productList != null && productList.Any())
                {
                    return Ok(productList);
                }   
                else
                {
                    return NotFound("No Products found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
        [HttpPut("UpdateCustomerProfile/{UserId}")]
        public async Task<IActionResult> UpdateCustomerProfile(int UserId,userModel user)
        {
            try
            {
                var isValid = await _customer.UpdateCustomerProfile(UserId, user);

                if (isValid)
                {
                    return Ok("Admin Profile Updated successfully");
                }
                else
                {
                    return NotFound("Error occurs Admin Profile Updating ");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(OrderPlace order)
        {
            try
            {
                
                if (order.PaymentMethod!="COD")
                {
                    string bankBaseUrl = _configuration["Client:BankClient"] ?? "";
                    string app = "Admin";
                    string path = "VaildateCustomer";
                    var paymentInfo = new
                    {
                        PaymentMethod = order.PaymentMethod,
                        Email = order.Email,
                        Amount = order.Amount

                    };

                    var IsvaildPayment = await _invokeApi.SendToClient(bankBaseUrl, app, path, paymentInfo);
                    if (IsvaildPayment == "Validation successful. Balance updated.")
                    {
                        var orderResponse = await _customer.PlaceOrder(order);

                        if (orderResponse.ResultMessage == "Order Placed Successfully")
                        {
                            var orderDetails = await _customer.GetOrderDetails(orderResponse.OrderId);
                            if (orderDetails != null)
                            {
                                string logisticBaseUrl = _configuration["Client:LogisticClient"] ?? "";
                                string app1 = "Logistic";
                                string path1 = "AddOrder";
                                var isActive = _invokeApi.SendToClient(logisticBaseUrl, app1, path1, orderDetails);
                                return Ok(orderResponse);
                            }
                            else
                            {
                                return NotFound("Order details not found.");
                            }

                        }
                        else
                        {
                            return BadRequest(orderResponse.ResultMessage);
                        }
                    }
                    else
                    {
                        return BadRequest(IsvaildPayment);
                    }
                }
                else
                {
                    var orderResponse = await _customer.PlaceOrder(order);
                    var orderDetails = await _customer.GetOrderDetails(orderResponse.OrderId);
                    if (orderDetails != null)
                    {
                        string logisticBaseUrl = _configuration["Client:LogisticClient"] ?? "";
                        string app1 = "Logistic";
                        string path1 = "AddOrder";
                        var isActive = _invokeApi.SendToClient(logisticBaseUrl, app1, path1, orderDetails);
                        return Ok(orderResponse);
                    }
                    else
                    {
                        return Ok(orderResponse);
                    }
                  
                }
               
                

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





    }
}
