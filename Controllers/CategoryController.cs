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
            try
            {
                return Ok(_categoryService.AddCategory(Category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }


        [HttpGet("ADMIN: ViewCategories")]
        public IActionResult ViewCategories()
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

    }
}
