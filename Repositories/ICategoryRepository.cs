using EcommerceTask.Models;

namespace EcommerceTask.Repositories
{
    public interface ICategoryRepository
    {
        int AddCategory(Category category);
        List<Category> GetCategories();
        int GetCategoryByName(string categoryName);
    }
}