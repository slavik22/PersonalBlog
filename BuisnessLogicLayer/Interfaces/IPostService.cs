using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

public interface IPostService : ICrud<PostModel>
{
    public IEnumerable<PostModel> GetUserPostsAsync(int userId);

    public Task<IEnumerable<PostModel>> GetPostsSearch(string text);
    public Task AddTagsAsync(int postId, IEnumerable<TagModel> tagModels);
    public Task<IEnumerable<PostModel>> GetAllPublishedAsync();
    public Task<IEnumerable<TagModel>> GetTagsAsync(int postId);

}