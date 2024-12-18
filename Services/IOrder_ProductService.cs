using EcommerceTask.Models;

namespace EcommerceTask.Services
{
    public interface IOrder_ProductService
    {
        bool AddNewProduct_Order(int OrederID, int productID, int Qty);
        List<Order_Product> ViewOrderProds();
    }
}