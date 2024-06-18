using E_Commerce.Model;
using E_Commerce.Repository;
using E_Commerce.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISeller _seller;

        public SellerController(ISeller seller)
        {
            _seller = seller;
        }


        [HttpGet("GetSellerProfile/{UserId}")]
        public async Task<IActionResult> GetSellerProfile(int UserId)
        {
            try
            {
                var myProfile = await _seller.GetSellerProfile(UserId);

                if (myProfile != null && myProfile.Any())
                {
                    return Ok(myProfile);
                }
                else
                {
                    return NotFound("Not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("MyAllProducts/{SellerId}")]
        public async Task<IActionResult> MyAllProducts(int SellerId)
        {
            try
            {
                var myProduct = await _seller.GetMyProduct(SellerId);

                if (myProduct != null && myProduct.Any())
                {
                    return Ok(myProduct);
                }
                else
                {
                    return NotFound("Not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("MyProducts/{SellerId}")]
        public async Task<IActionResult> MyProducts(int SellerId)
        {
            try
            {
                var myProducts = await _seller.GetSellerActiveProducts(SellerId);

                if (myProducts != null && myProducts.Any())
                {
                    return Ok(myProducts);
                }
                else
                {
                    return NotFound("Not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AddProduct/{SellerId}")]
        public async Task<IActionResult> AddProduct(int SellerId,AddProduct product)
        {
            try
            {
                var resultMessage = await _seller.AddProduct(SellerId, product);

                if (resultMessage == "Product Added Successfully")
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

        [HttpPut("UpdateProduct/{ProductId}")]
        public async Task<IActionResult> UpdateProduct(int ProductId,ProductUpdate product)
        {
            var resultMessage = await _seller.UpdateProduct(ProductId, product);
            if (resultMessage == "Product Updated Successfully")
            {
                return Ok(resultMessage);
            }
            else
            {
                return BadRequest(resultMessage);
            }
        }

        [HttpPut("UpdateSellerProfile/{UserId}")]
        public async Task<IActionResult> UpdateSellerProfile(int UserId,userModel user)
        {
            try
            {
                var isValid = await _seller.UpdateSellerProfile(UserId, user);

                if (isValid)
                {
                    return Ok("Seller Profile Updated successfully");
                }
                else
                {
                    return NotFound("Error occurs Seller Profile Updating ");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int ProductId)
        {
            try
            {
                var isValid = await _seller.DeleteProduct(ProductId);

                if (isValid)
                {
                    return Ok("Product Deleted successfully");
                }
                else
                {
                    return NotFound("Error occurs Product Deleting");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

      

    }


}
