using EcommerceTask.DTOs;
using EcommerceTask.Models;

namespace EcommerceTask.Services
{
    public interface IHybridService
    {
        int AddProduct(ProductInDTO product, int AdminID);
        OrderReciptDTO NewOrder(List<OrderInDTO> orders, int userID);
        int UpdateProduct(ProductInDTO product, int AdminID);
        List<ProductOutDTO> GetAllProducts(ProductFilterDTO filter);
        int GetCategoryID(string catname);
        string GetCategoryName(int catID);
        List<ReviewOutDTO> GetAllReviewsByProdID(int prodID);
        int GetProductIDByName(string ProductName);
        List<Order> GetMyOrders(int userID);
        List<Order_Product> GetOrderProdsByOrderID(int OID);
        void UpdateRating(int prodID);
        string GetProductNameByID(int ProductID);
    }
}