using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Generic;

namespace EcommerceTask.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderrepository;
       private readonly IHybridService _hybridService;

        public OrderService(IOrderRepository orderrepo)
        {
            _orderrepository = orderrepo;
        }

        public OrderReciptDTO NewOrder(List<OrderInDTO> orders, int userID)
        {
            return _hybridService.NewOrder(orders, userID);
        }

        public List<Order> GetMyOrders(int userID)
        {
            return _orderrepository.GetAllOrdersByUserID(userID);
        }

        public int AddOrder(Order order)
        {
            return  _orderrepository.AddOrder(order);
        }

        public Order GetOrderByID(int userID, int OrderID)
        {
            return _orderrepository.GetOrdersByOrderID(userID, OrderID);
        }
    }
}
