using EcommerceTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceTask.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category.CID;
        }

        public int GetCategoryByName(string categoryName)
        {
            var cat = _context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            return cat.CID;
        }

        public string GetCategoryNameByID(int catID)
        {
            var cat = _context.Categories.FirstOrDefault(c => c.CID == catID);
            return cat.CategoryName;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
