using EcommerceTask.DTOs;
using EcommerceTask.Models;

namespace EcommerceTask.Services
{
    public interface IOrderService
    {
        List<Order> GetMyOrders(int userID);
        Order GetOrderByID(int userID, int OrderID);
        OrderReciptDTO NewOrder(List<OrderInDTO> orders, int userID);

        int AddOrder(Order order);
    }
}