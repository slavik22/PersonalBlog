using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryModel>> GetAllAsync()
    {
        IEnumerable<Category> categories =  await _unitOfWork.CategoryRepository.GetAllAsync();
        List<CategoryModel> categoryModels = new List<CategoryModel>();

        foreach (var c in categories)
        {
            categoryModels.Add(_mapper.Map<CategoryModel>(c));
        }

        return categoryModels;

    }

    public async Task<CategoryModel> GetByIdAsync(int id)
    {
        return _mapper.Map<CategoryModel>(await _unitOfWork.CategoryRepository.GetByIdAsync(id));
    }

    public async Task AddAsync(CategoryModel model)
    {
        await _unitOfWork.CategoryRepository.AddAsync(_mapper.Map<Category>(model));
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(CategoryModel model)
    {
        _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(model));
        await _unitOfWork.SaveAsync();

    }

    public async Task DeleteAsync(int modelId)
    {
       await _unitOfWork.CategoryRepository.Delete(modelId);
       await _unitOfWork.SaveAsync();

    }
    
    
    public async Task AddCategoryAsync(int postId, CategoryModel categoryModel)
    {
        Post post = await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostCategories");
        
        if ( post == null)
        {
            throw new Exception ("Post not found");
        }
        
        Category category = await _unitOfWork.CategoryRepository.GetByValueOneAsync(category => category.Title == categoryModel.Title);
        
        if(category == null )
        {
            category = _mapper.Map<Category>(categoryModel);
            await _unitOfWork.CategoryRepository.AddAsync(category);
        }

        post.PostCategories.Add(new PostCategory()
        {
            Category = category,
            Post = post
        });
            
        await _unitOfWork.SaveAsync();
    }
    
    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync(int postId)
    {
        Post post = (await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostCategories"));

        if (post == null)
        {
            throw new Exception ("Post not found");
        }
        
        var categoriesIds = post.PostCategories.Select(pt => pt.CategoryId);
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync(t => categoriesIds.Contains(t.Id));
        
        List<CategoryModel> categoryModels = new List<CategoryModel>();

        foreach (var item in post.PostCategories.Select(pt => pt.Category))
            categoryModels.Add(_mapper.Map<CategoryModel>(item));

        return categoryModels;

    }
}