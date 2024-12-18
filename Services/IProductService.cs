using EcommerceTask.DTOs;

namespace EcommerceTask.Services
{
    public interface IProductService
    {
        int AddProduct(ProductInDTO product, int AdminID);
        public List<ProductOutDTO> GetAllProducts(ProductFilterDTO filter);
        ProductOutDTO GetProductByID(int ID);
        int UpdateProduct(ProductInDTO product, int AdminID);
    }
}