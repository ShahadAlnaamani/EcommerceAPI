using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;
using Org.BouncyCastle.Asn1.X509;

namespace EcommerceTask.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderrepository;
        private readonly IProductService _productservice;
        private readonly IOrder_ProductService _orderproductservice;


        public OrderService(IOrderRepository orderrepo, IProductService productservice, IOrder_ProductService orderprod)
        {
            _orderrepository = orderrepo;
            _productservice = productservice;
            _orderproductservice = orderprod;
        }

        public OrderReciptDTO NewOrder(List<OrderInDTO> orders, int userID)
        {
            decimal TotalAmount = 0;
            var Allproducts = new List<Product>();
            //check valid product 
            foreach (var order in orders)
            {
                int prodID = _productservice.GetProductIDByName(order.productName);
                Product product = _productservice.GetFullProductByID(prodID);
                int availableStock = product.Stock;

                //Validation of request
                if (order.productName == null)
                { throw new Exception("<!>One or more of the products in the order are invalid<!>"); }
                if (availableStock >= order.quantity)
                {
                    Allproducts.Add(product);
                    TotalAmount = TotalAmount + order.quantity * product.Price;
                }
                else { throw new Exception("<!>There is not enough stock to complete your order<!>"); }
            }

            var Neworder = new Order
            {
                UserID = userID,
                OrderDate = DateTime.Now,
                TotalAmount = TotalAmount,
            };

            int orderID = _orderrepository.AddOrder(Neworder);

            for (int i = 0; i < Allproducts.Count; i++)
            {
                var updatedProd = new ProductInDTO
                {
                    Stock = Allproducts[i].Stock - orders[i].quantity,
                };
                bool complete = _productservice.UpdateAfterOrder(updatedProd, Allproducts[i].PID);
                _orderproductservice.AddNewProduct_Order(orderID, Allproducts[i].PID, orders[i].quantity);

            }
            //make it a transaction dont update anything until after everything goes through

            var Reciept = new OrderReciptDTO
            {
                OID = orderID,
                OrderDate = DateTime.Now,
                TotalAmount = TotalAmount,
            };

            return Reciept;
        }

        public List<Order> GetMyOrders(int userID)
        {
            return _orderrepository.GetAllOrdersByUserID(userID);
        }

        public Order GetOrderByID(int userID, int OrderID)
        {
            return _orderrepository.GetOrdersByOrderID(userID, OrderID);
        }
    }
}
