using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;
using System.Diagnostics.Eventing.Reader;

namespace EcommerceTask.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productrepository;
        private readonly ICategoryService _categoryservice;

        public ProductService(IProductRepository productrepo, ICategoryService categoryservice)
        {
            _productrepository = productrepo;
            _categoryservice = categoryservice;
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
            return _productrepository.AddProduct(NewProd);
        }


        public int UpdateProduct(ProductInDTO product, int AdminID)
        {
            int ProdID = _productrepository.GetProductByName(product.Name);
            int CatID = _categoryservice.GetCategoryID(product.CategoryName);

            if (ProdID != 0 || ProdID != null)
            {
                if (CatID != 0 || CatID != null)
                {
                    bool updated = _productrepository.UpdateProduct(product, ProdID, CatID);

                    if (updated) return 0; //successful no errors
                    else return 3; //not updated error occured 
                }
                else return 2; //improper category inputted 
            }
            else return 1; //product not found
        }

        //Add pagination and filtering (order by price, order by category, get by category)
        public List<ProductOutDTO> GetAllProducts(ProductFilterDTO filter)
        {
           var products = CheckFilters(filter);

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

                    int CatID = _categoryservice.GetCategoryID(filter.CategoryName);

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
                int CatID = _categoryservice.GetCategoryID(filter.CategoryName);

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
            string CategoryName = _categoryservice.GetCategoryName(product.CatId);


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
    }
}
