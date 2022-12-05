using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

public interface ITagService : ICrud<TagModel>
{
    public Task AddTagAsync(int postId, TagModel tagModel);
    public Task<IEnumerable<TagModel>> GetTagsAsync(int postId);
}