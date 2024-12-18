using EcommerceTask.Models;

namespace EcommerceTask.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.OID;
        }

        //get my orders 
        public List<Order> GetAllOrdersByUserID(int ID)
        {
            return _context.Orders.Where(o => o.UserID == ID).ToList();
        }

        public Order GetOrdersByOrderID(int UserID, int OrderID) //userID needed to ensure that user is only viewing their own orders  
        {
            return _context.Orders.FirstOrDefault(o => o.UserID == UserID && o.OID == OrderID);
        }
    }
}
