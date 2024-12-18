using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder(List<OrderInDTO> orders)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            try
            {
                return Ok(_orderService.NewOrder(orders, int.Parse(userID)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetMyOrders")]
        public IActionResult GetMyOrders()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            try
            {
                return Ok(_orderService.GetMyOrders(int.Parse(userID)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetOrderByID {OrderID}")]
        public IActionResult GetOrderByID(int OrderID)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            try
            {
                return Ok(_orderService.GetOrderByID(int.Parse(userID), OrderID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
