using EcommerceTask.DTOs;

namespace EcommerceTask.Services
{
    public interface IReviewService
    {
        int AddReview(ReviewInDTO review, int userID);
        int DeleteReview(int userID, int ReviewID);
        List<ReviewOutDTO> GetAllReviewsByProdID(int Page, int PageSize, int prodID);
        int UpdateReview(int userID, int reviewID, ReviewInDTO review);
    }
}