using EcommerceTask.DTOs;
using EcommerceTask.Models;

namespace EcommerceTask.Repositories
{
    public interface IProductRepository
    {
        int AddProduct(Product product);
        Product GetProductByID(int ID);
        List<Product> GetProducts(int page, int pageSize, string prodName);
        List<Product> GetProducts(int page, int pageSize, int catID);
        List<Product> GetProductsByRating(int page, int pageSize, int rating);
        List<Product> GetProducts(int page, int pageSize, string prodName, int rating);
        List<Product> GetProducts(int page, int pageSize, int catID, string prodName);
        List<Product> GetProducts(int page, int pageSize, int catID, int rating);
        List<Product> GetProducts(int page, int pageSize, int catID, string prodName, int rating);
        int GetProductByName(string Name);
        string GetProductNameByID(int ID);
        List<Product> GetProducts(int page, int PageSize);
        bool UpdateProduct(ProductInDTO newprod, int ProdID, int Category, int AdminID);
        bool UpdateAfterOrder(ProductInDTO newprod, int ProdID);
        decimal UpdateProductRating(Product product);
    }
}