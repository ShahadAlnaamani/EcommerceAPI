using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceTask.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("ADMIN: AddCategory")]
        public IActionResult AddCategory(Category Category)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;  // Checking if request is being done by an admin

            if (userRole == "Admin")
            {
                try
                {
                    return Ok(_categoryService.AddCategory(Category));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else return Unauthorized("<!>This function is only available for admins<!>"); //Current user is not admin

        }


        [HttpGet("ADMIN: ViewCategories")]
        public IActionResult ViewCategories()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;  // Checking if request is being done by an admin

            if (userRole == "Admin")
            {
                try
                {
                    return Ok(_categoryService.GetAllCategories());
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
