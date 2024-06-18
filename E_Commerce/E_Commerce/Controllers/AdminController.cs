using E_Commerce.Model;
using E_Commerce.Repository;
using E_Commerce.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }
        #region GetMethod
        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer(int? UserId)
        {
            try
            {
                var customerList = await _admin.GetAllCustomer(UserId);

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
        [HttpGet("GetAllSeller")]
        public async Task<IActionResult> GetAllSeller(int? UserId)
        {
            try
            {
                var sellerList = await _admin.GetAllSeller(UserId);

                if (sellerList != null && sellerList.Any())
                {
                    return Ok(sellerList);
                }
                else
                {
                    return NotFound("No seller found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllAdmin")]
        public async Task<IActionResult> GetAllAdmin(int? UserId)
        {
            try
            {
                var adminList = await _admin.GetAllAdmin(UserId);

                if (adminList != null && adminList.Any())
                {
                    return Ok(adminList);
                }
                else
                {
                    return NotFound("No admin found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct(int? ProductId)
        {
            try
            {
                var productList = await _admin.GetAllProducts();

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

        [HttpGet("GetAllActiveProduct")]
        public async Task<IActionResult> GetAllActiveProduct(int? ProductId)
        {
            try
            {
                var productList = await _admin.GetAllActiveProducts(ProductId);

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
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        #endregion

        #region PutMethod
        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus(updateOrderStatus updateOrder)
        {
            try
            {
                var isValid = await _admin.UpdateOrderStatus(updateOrder);

                if (isValid)
                {
                    return Ok("Product Updated successfully");
                }
                else
                {
                    return NotFound("Error occurs Products update.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateProductActiveState/{ProductId}/{isActive}")]
        public async Task<IActionResult> UpdateProductActiveState(int ProductId,bool isActive)
        {
            try
            {
                var isValid = await _admin.UpdateProductActiveState(ProductId, isActive);

                if (isValid)
                {
                    return Ok("Product Activestatus Updated successfully");
                }
                else
                {
                    return NotFound("Error occurs Product Activestatus ");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateProfile/{UserId}")]
        public async Task<IActionResult> UpdateProfile(int UserId,userModel user)
        {
            try
            {
                var isValid = await _admin.UpdateAdminProfile(UserId, user);

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
        #endregion

        #region PostMethod
        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser(userModel user)
        {
            try
            {
                var resultMessage = await _admin.AddNewUser(user);

                if (resultMessage == "User added successfully")
                {
                    return Ok(resultMessage);
                }
                else
                {
                    return BadRequest(resultMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
