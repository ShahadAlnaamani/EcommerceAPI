using EcommerceTask.Models;

namespace EcommerceTask.Repositories
{
    public interface IOrder_ProductRepository
    {
        bool AddProduct_Order(Order_Product Ord_Prod);
        List<Order_Product> GetAllOrderProds();
    }
}