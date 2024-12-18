using EcommerceTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceTask.Repositories
{
    public class Order_ProductRepository : IOrder_ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public Order_ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddProduct_Order(Order_Product Ord_Prod)
        {
            _context.Order_Products.Add(Ord_Prod);
            _context.SaveChanges();
            return true; //confirmation that all done properly 
        }

        public List<Order_Product> GetAllOrderProds()
        {
            return _context.Order_Products.ToList();
        }

        public List<Order_Product> GetOrderProdsByOrderID(int orderID) 
        {
            return _context.Order_Products.Where(op => op.OrderID == orderID).ToList();
        }

    }
}
