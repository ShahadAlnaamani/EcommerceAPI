using EcommerceTask.DTOs;
using EcommerceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpPost("AddReview")]
        public IActionResult AddReview(ReviewInDTO review)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            try
            {
                return Ok(_reviewService.AddReview(review, int.Parse(userID)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("GetAllReviewsByProduct")]
        public IActionResult GetProductReviews(int PageSize, int page, int productID)
        {
            try
            {
                return Ok(_reviewService.GetAllReviewsByProdID(PageSize, page, productID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Update Review")]
        public IActionResult UpdateReview(int reviewID, ReviewInDTO newRev)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            try
            {
                return Ok(_reviewService.UpdateReview(int.Parse(userID), reviewID, newRev));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete Review")]
        public IActionResult DeleteReview(int reviewID)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Checking if request is being done by an admin

            try
            {
                return Ok(_reviewService.DeleteReview(int.Parse(userID), reviewID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
