using EcommerceTask.Models;

namespace EcommerceTask.Services
{
    public interface ICategoryService
    {
        int AddCategory(Category category);
        List<Category> GetAllCategories();
        int GetCategoryID(string name);
    }
}