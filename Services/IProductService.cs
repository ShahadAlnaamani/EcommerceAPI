using EcommerceTask.DTOs;
using EcommerceTask.Models;

namespace EcommerceTask.Services
{
    public interface IProductService
    {
        int AddProduct(ProductInDTO product, int AdminID);
        public List<ProductOutDTO> GetAllProducts(ProductFilterDTO filter);
        ProductOutDTO GetProductByID(int ID);
        Product GetFullProductByID(int ID);
        int UpdateProduct(ProductInDTO product, int AdminID);
        int GetProductIDByName(string name);
        bool UpdateAfterOrder(ProductInDTO product, int prodID);

    }
}