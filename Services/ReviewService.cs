using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;
using Org.BouncyCastle.Asn1.X509;
using System.Globalization;

namespace EcommerceTask.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewrepository;
        private readonly IHybridService _hybridservice;


        public ReviewService(IReviewRepository reviewrepo, IHybridService hybridservice)
        {
            _reviewrepository = reviewrepo;
            _hybridservice = hybridservice;
        }

        public int AddReview(ReviewInDTO review, int userID)
        {
            int prodID = _hybridservice.GetProductIDByName(review.ProductName);

            if (prodID == 0 || prodID == null)
            {
                throw new Exception("<!>The product name inputted does not exist<!>");
            }

            var pastOrders = _hybridservice.GetMyOrders(userID);
            bool valid = false;

            if (pastOrders != null)
            {
                foreach (var order in pastOrders)
                {
                    var OrdProds = _hybridservice.GetOrderProdsByOrderID(order.OID);
                    foreach (var ordprod in OrdProds)
                    {
                        if (ordprod.ProductID == prodID)
                        {
                            valid = true;
                        }
                    }
                }
            }

            if (valid)
            {
                var pastReview = _reviewrepository.CheckNewProdReview(userID, prodID);
                if (pastReview == null)
                {
                    var NewReview = new Review
                    {
                        UserID = userID,
                        ProductID = prodID,
                        Rating = review.Rating,
                        Comment = review.Comment,
                        ReviewDate = DateTime.Now,
                    };
                    _hybridservice.UpdateRating(prodID);
                    return _reviewrepository.AddReview(NewReview);
                }
                else throw new Exception("<!>It looks like you have already made a review on this product before<!>");
            }

            else throw new Exception("<!>It looks like you have not ordered this product, you may only review products you have previously ordered<!>");
        }

        public List<ReviewOutDTO> GetAllReviewsByProdID(int Page, int PageSize, int prodID)
        {
            var userRevs = _reviewrepository.GetAllReviewsByID(Page, PageSize, prodID);
            var OutReviews = new List<ReviewOutDTO>();

            foreach (var ur in userRevs)
            {
                string name = _hybridservice.GetProductNameByID(ur.ProductID);
                var review = new ReviewOutDTO
                {
                    RID = ur.RID,
                    UserID = ur.UserID,
                    ProductName = name,
                    Rating = ur.Rating,
                    Comment = ur.Comment,
                    ReviewDate = ur.ReviewDate,
                };
                OutReviews.Add(review);
            }

            return OutReviews;
        }

        public int DeleteReview(int userID, int ReviewID)
        {
            var review = _reviewrepository.GetReviewByRID(ReviewID);

            if (review.UserID == userID)
            {
                _reviewrepository.DeleteReview(review);
                return 1;
            }

            else throw new Exception("<!>You did not write this review, you can only delete your own reviews<!>");
        }

        public int UpdateReview(int userID, int reviewID, ReviewInDTO review)
        {
            var OriginalReview = _reviewrepository.GetReviewByRID(reviewID);

            if (OriginalReview.UserID == userID)
            {
                OriginalReview.Rating = review.Rating;
                OriginalReview.Comment = review.Comment;
                _reviewrepository.UpdateReview(OriginalReview);
                return 1;
            }

            else throw new Exception("<!>You did not write this review, you can only edit your own reviews<!>");

        }

    }
}
