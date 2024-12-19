using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;
using Org.BouncyCastle.Asn1.X509;

namespace EcommerceTask.Services
{
    //Created to break cycle error 
    public class HybridService : IHybridService
    {
        private readonly IProductService _productservice;
        private readonly IReviewService _reviewservice;
        private readonly IOrderService _orderservice;
        private readonly IOrder_ProductService _orderproductservice;
        private readonly ICategoryService _categoryservice;

        public HybridService(IReviewService reviewservice, IProductService productservice, IOrderService orderservice, IOrder_ProductService orderproductservice, ICategoryService categoryservice)
        {
            _productservice = productservice;
            _orderservice = orderservice;
            _reviewservice = reviewservice;
            _orderproductservice = orderproductservice;
            _categoryservice = categoryservice;
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

            int orderID = _orderservice.AddOrder(Neworder);

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

        public int AddProduct(ProductInDTO product, int AdminID)
        {
            int category = _categoryservice.GetCategoryID(product.CategoryName);

            if (category == 0 || category == null)
            {
                return 0; //category not found
            }

            var NewProd = new Product
            {
                Name = product.Name,
                Description = product.Description,
                CatId = category,
                Price = product.Price,
                Stock = product.Stock,
                ProductActive = true,
                Modifiedt = DateTime.Now,
                ModifiedBy = AdminID,

            };
            return _productservice.AddNewProduct(NewProd);
        }

        public int UpdateProduct(ProductInDTO product, int AdminID)
        {
            int ProdID = _productservice.GetProductByName(product.Name);
            int CatID = _categoryservice.GetCategoryID(product.CategoryName);

            if (ProdID != 0 || ProdID != null)
            {
                if (CatID != 0 || CatID != null)
                {
                    bool updated = _productservice.CompleteUpdateProduct(product, ProdID, CatID, AdminID);

                    if (updated) return 0; //successful no errors
                    else return 3; //not updated error occured 
                }
                else return 2; //improper category inputted 
            }
            else return 1; //product not found
        }

        public List<ProductOutDTO> GetAllProducts(ProductFilterDTO filter)
        {
            var products = _productservice.CheckFilters(filter);

            var outProds = new List<ProductOutDTO>();

            //Mapping prod -> prodOutDTO
            foreach (var prod in products)
            {
                string CategoryName = _categoryservice.GetCategoryName(prod.CatId);

                var output = new ProductOutDTO
                {
                    PID = prod.PID,
                    Name = prod.Name,
                    Price = prod.Price,
                    Description = prod.Description,
                    Category = CategoryName,
                };

                outProds.Add(output);
            }

            return outProds;
        }

        public int GetCategoryID(string catname)
        {
            return _categoryservice.GetCategoryID(catname);
        }

        public string GetCategoryName(int catID)
        {
            return _categoryservice.GetCategoryName(catID);
        }

        public List<ReviewOutDTO> GetAllReviewsByProdID(int prodID)
        {
           return _reviewservice.GetAllReviewsByProdID(1, 1000000, prodID);
        }

        public int GetProductIDByName(string ProductName)
        {
           return _productservice.GetProductIDByName(ProductName);
        }

        public List<Order> GetMyOrders(int userID)
        {
            return _orderservice.GetMyOrders(userID);
        }

        public List<Order_Product> GetOrderProdsByOrderID(int OID)
        {
            return _orderproductservice.GetOrderProdsByOrderID(OID);
        }

        public void UpdateRating(int prodID)
        {
            _productservice.UpdateRating(prodID);
        }

        public string GetProductNameByID(int ProductID)
        {
            return _productservice.GetProductNameByID(ProductID);
        }
    }
}
