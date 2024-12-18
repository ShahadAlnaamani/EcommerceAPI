using EcommerceTask.DTOs;
using EcommerceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace EcommerceTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase 
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin")] //Ensures that specific functions are only used by admins 
        [HttpPost("ADMIN: AddProduct")]
        public IActionResult AddProduct(ProductInDTO product)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;  // Checking if request is being done by an admin
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            if (userRole == "Admin")
            {
                try
                {
                    return Ok(_productService.AddProduct(product, int.Parse(userID)));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else return Unauthorized("<!>This function is only available for admins<!>"); //Current user is not admin
        }

        [Authorize(Roles = "Admin")] //Ensures that specific functions only used by admins
        [HttpPost("ADMIN: Update product")]
        public IActionResult UpdateProduct(ProductInDTO update)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;  // Checking if request is being done by an admin
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            if (userRole == "Admin")
            {
                return Ok(_productService.UpdateProduct(update, int.Parse(userID)));
            }
            else return Unauthorized("<!>This function is only available for admins<!>"); //Current user is not admin
        }

        [AllowAnonymous]
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts(ProductFilterDTO filters)
        {
            try
            {
                return Ok(_productService.GetAllProducts(filters));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpGet("GetProductByID {ID}")]
        public IActionResult GetProductByID(int ID)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;  // Checking if request is being done by an admin

            if (userRole == "Admin")
            {
                try
                {
                    return Ok(_productService.GetProductByID(ID));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else return Unauthorized("<!>This function is only available for admins<!>"); //Current user is not admin

        }
    }
}
