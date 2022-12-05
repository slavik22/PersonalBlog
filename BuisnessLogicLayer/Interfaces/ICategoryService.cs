using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

public interface ICategoryService : ICrud<CategoryModel>
{
    public Task AddCategoryAsync(int postId, CategoryModel categoryModel);
    public Task<IEnumerable<CategoryModel>> GetCategoriesAsync(int postId);
}