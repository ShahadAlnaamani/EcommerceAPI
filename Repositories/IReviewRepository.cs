using EcommerceTask.Models;

namespace EcommerceTask.Repositories
{
    public interface IReviewRepository
    {
        int AddReview(Review review);
        void DeleteReview(Review review);
        List<Review> GetAllReviewsByID(int page, int pageSize, int ProdID);
        void UpdateReview(Review review);
        Review CheckNewProdReview(int UserID, int prodID);
        Review GetReviewByRID(int reviewID);
    }
}