using ViewModels;

namespace FUNewsManagement.Repository
{
    public interface ICategoryRepository
    {
        public List<CategoryDetailVM> GetAllCategory();
        public CategoryDetailVM GetCategoryById(short id);
        public void AddCategory(CategoryDetailVM category);
        public void EditCategory(CategoryDetailVM category);
        public void DeleteCategory(short categoryId);
        public bool CheckExistNewsOfCategory(short categoryId);
    }
}
