using Microsoft.IdentityModel.Tokens;
using Models;
using ViewModels;

namespace FUNewsManagement.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsDBContext _context;

        public CategoryRepository(NewsDBContext dbContext)
        {
            _context = dbContext;
        }

        public List<CategoryDetailVM> GetAllCategory()
        {
            var categories = (from c in _context.Categories
                              where c.IsActive == true
                              select new CategoryDetailVM()
                              {
                                  CategoryId = c.CategoryId,
                                  CategoryName = c.CategoryName,
                                  CategoryDesciption = c.CategoryDesciption,
                                  IsActive = c.IsActive,
                                  Status = (c.IsActive == true ? "Active" : "InActive")
                              }).ToList();
            return categories;
        }

        public CategoryDetailVM GetCategoryById(short id)
        {
            var category = (from c in _context.Categories
                            where c.IsActive == true && c.CategoryId == id
                            select new CategoryDetailVM()
                            {
                                CategoryId = c.CategoryId,
                                CategoryName = c.CategoryName,
                                CategoryDesciption = c.CategoryDesciption,
                                IsActive = c.IsActive,
                                Status = (c.IsActive == true ? "Active" : "InActive")
                            }).FirstOrDefault();
            return category;
        }

        public void AddCategory(CategoryDetailVM category)
        {
            Category newCate = new Category();
            newCate.CategoryName = category.CategoryName;
            newCate.CategoryDesciption = category.CategoryDesciption;
            newCate.IsActive = true;

            _context.Categories.Add(newCate);
            _context.SaveChanges();
        }

        public void EditCategory(CategoryDetailVM category)
        {
            var oldCategory = _context.Categories.First(c => c.CategoryId == category.CategoryId);
            oldCategory.CategoryName = category.CategoryName;
            oldCategory.CategoryDesciption = category.CategoryDesciption;
            oldCategory.IsActive = category.IsActive;

            _context.SaveChanges();
        }

        public void DeleteCategory(short categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public bool CheckExistNewsOfCategory(short categoryId)
        {
            var news = _context.NewsArticles.Where(n => n.CategoryId == categoryId).ToList();

            return (news.IsNullOrEmpty() ? false : true);
        }
    }
}
