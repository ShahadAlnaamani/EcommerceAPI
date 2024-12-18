using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;

namespace EcommerceTask.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryrepository;

        public CategoryService(ICategoryRepository categoryrepo)
        {
            _categoryrepository = categoryrepo;
        }

        public int AddCategory(Category category)
        {
            return _categoryrepository.AddCategory(category);
        }

        public int GetCategoryID(string name)
        {
            int catID = _categoryrepository.GetCategoryByName(name);

            if (catID == null) { return 0; } //not found 
            else return catID;
        }

        public string GetCategoryName(int ID)
        {
            return _categoryrepository.GetCategoryNameByID(ID);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryrepository.GetCategories();
        }
    }
}
