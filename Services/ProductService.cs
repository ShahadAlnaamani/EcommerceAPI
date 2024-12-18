using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;
using System.Diagnostics.Eventing.Reader;

namespace EcommerceTask.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productrepository;

        public ProductService(IProductRepository productrepo)
        {
            _productrepository = productrepo;
        }

        public int AddProduct(ProductInDTO product, int AdminID)
        {
            //if category not found - add new category
            //int category = CategoryFinder(product.CategoryName);

            int category = 0; //*

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

        //public int CategoryFinder(string categoryName)
        //{ int CatID = _categoryserive.GetCategoryByName(categoryName);}

        public int UpdateProduct(ProductInDTO product, int AdminID)
        {
            int ProdID = _productrepository.GetProductByName(product.Name);
            //int CatID = CategoryFinder(product.CatName);
            int CatID = 0;

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
                //string CategoryName = _productrepository.CategoryFinder(prod.CatId);
                string CategoryName = " ";
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

                    if (filter.ProductName != null)
                    {
                        prodName = filter.ProductName;
                        //need to get cat ID 
                        //products = _productrepository.GetProducts(filter.Page, filter.PageSize, rating, prodName, catName);
                    }
                    //Gets products filtered by Rating and category name 
                    products = _productrepository.GetProducts(filter.Page, filter.PageSize, rating, catName);
                }

                if (filter.ProductName != null)
                {
                    prodName = filter.ProductName;
                    //need to get cat ID 
                    //products = _productrepository.GetProducts(filter.Page, filter.PageSize, rating, prodName, catName );
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
                //need to get cat ID 
                //products = _productrepository.GetProducts(filter.Page, filter.PageSize, prodName, catName);
            }

            else if (filter.CategoryName != null)
            {
                catName = filter.CategoryName;
                products = _productrepository.GetProducts(filter.Page, filter.PageSize, catName);
            }

            else
            { products = _productrepository.GetProducts(filter.Page, filter.PageSize); }
            return products;
        }

        public ProductOutDTO GetProductByID(int ID)
        {
            var product = _productrepository.GetProductByID(ID);
            //string CategoryName = _productrepository.CategoryFinder(prod.CatId);
            string CategoryName = " ";

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
