using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;
using System.Diagnostics.Eventing.Reader;

namespace EcommerceTask.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productrepository;
        private readonly IHybridService _hybridService;

        public ProductService(IProductRepository productrepo, ICategoryService categoryservice, IReviewService reviewservice)
        {
            _productrepository = productrepo;
        }

        public int AddProduct(ProductInDTO product, int AdminID)
        {
            return _hybridService.AddProduct(product, AdminID);
        }

        public int AddNewProduct(Product Newproduct)
        {
            return _productrepository.AddProduct(Newproduct);
        }

        public int UpdateProduct(ProductInDTO product, int AdminID)
        {
            return _hybridService.UpdateProduct(product, AdminID);
        }

        public int GetProductByName(string name)
        {
            return _productrepository.GetProductByName(name);
        }

        public bool CompleteUpdateProduct(ProductInDTO product, int ProdID, int CatID, int AdminID)
        {
            return _productrepository.UpdateProduct(product, ProdID, CatID, AdminID);
        }


        //Add pagination and filtering (order by price, order by category, get by category)
        public List<ProductOutDTO> GetAllProducts(ProductFilterDTO filter)
        {
            return _hybridService.GetAllProducts(filter);
        }

        public List<Product> CheckFilters(ProductFilterDTO filter)
        {
            int rating = 0;
            string catName = null;
            string prodName = null;
            var products = new List<Product>();


            if (filter.rating != 0 && filter.rating != null)
            {
                rating = filter.rating;

                if (filter.CategoryName != null)
                {
                    catName = filter.CategoryName;

                    int CatID = _hybridService.GetCategoryID(filter.CategoryName);

                    if (CatID == 0 || CatID == null)
                    { throw new Exception("<!>Invalid category<!>"); }

                    if (filter.ProductName != null)
                    {
                        prodName = filter.ProductName;
                        products = _productrepository.GetProducts(filter.Page, filter.PageSize, rating, prodName, CatID);
                    }
                    //Gets products filtered by Rating and category name 
                    products = _productrepository.GetProducts(filter.Page, filter.PageSize, rating, CatID);
                }

                if (filter.ProductName != null)
                {
                    prodName = filter.ProductName;
                    products = _productrepository.GetProducts(filter.Page, filter.PageSize, rating, prodName);
                }
                products = _productrepository.GetProducts(filter.Page, filter.PageSize, rating);

            }

            else if (filter.ProductName != null)
            {
                prodName = filter.ProductName;

                if (filter.rating != null)
                {
                    rating = filter.rating;
                    products = _productrepository.GetProducts(filter.Page, filter.PageSize, rating, prodName);
                }
                products = _productrepository.GetProducts(filter.Page, filter.PageSize, prodName);
            }

            else if (filter.CategoryName != null)
            {
                catName = filter.CategoryName;
                int CatID = _hybridService.GetCategoryID(filter.CategoryName);

                if (CatID == 0 || CatID == null)
                { throw new Exception("<!>Invalid category<!>"); }

                products = _productrepository.GetProducts(filter.Page, filter.PageSize, CatID);
            }

            else
            { products = _productrepository.GetProducts(filter.Page, filter.PageSize); }
            return products;
        }



        public ProductOutDTO GetProductByID(int ID)
        {
            var product = _productrepository.GetProductByID(ID);
            string CategoryName = _hybridService.GetCategoryName(product.CatId);


            var output = new ProductOutDTO
            {
                PID = product.PID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = CategoryName,
            };

            return output;
        }

        public Product GetFullProductByID(int ID)
        {
            return _productrepository.GetProductByID(ID);
        }

        public int GetProductIDByName(string name)
        {
            return _productrepository.GetProductByName(name);
        }

        public string GetProductNameByID(int ID)
        {
            return _productrepository.GetProductNameByID(ID);
        }

        public bool UpdateAfterOrder(ProductInDTO product, int prodID)
        {
            return _productrepository.UpdateAfterOrder(product, prodID);
        }

        public decimal UpdateRating(int productID)
        {
            var product = _productrepository.GetProductByID(productID);

            var reviews =  _hybridService.GetAllReviewsByProdID(productID);

            decimal AverageRating = 0;

            foreach(var review in reviews) 
            {
                AverageRating += review.Rating;
            }

            AverageRating /= AverageRating;

            product.Rating = AverageRating;

            return _productrepository.UpdateProductRating(product);
        }
    }
}
