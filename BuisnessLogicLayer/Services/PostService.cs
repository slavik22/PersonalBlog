using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using PostStatus = DataAccessLayer.Enums.PostStatus;

namespace BuisnessLogicLayer.Services;

public class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<PostModel>> GetAllAsync()
    {
        IEnumerable<Post> posts =  await _unitOfWork.PostRepository.GetAllAsync(null,null,"User");
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));

        return postModels;
    }

    public async Task<IEnumerable<PostModel>> GetAllPublishedAsync()
    {
        IEnumerable<Post> posts =  await _unitOfWork.PostRepository.GetAllAsync(p => p.PostStatus == PostStatus.Published,null,"User");
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));

        return postModels;
    }
    
    public async Task<PostModel> GetByIdAsync(int id)
    {
        return _mapper.Map<PostModel>(await _unitOfWork.PostRepository.GetByIdAsync(id,"User"));
    }

    public async Task AddAsync(PostModel model)
    {

        await _unitOfWork.PostRepository.AddAsync(_mapper.Map<Post>(model));
        await _unitOfWork.SaveAsync();
    }

    public async Task AddTagsAsync(int postId, IEnumerable<TagModel> tagModels)
    {
        Post post = await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostTags");
        
        if ( post == null)
        {
            throw new Exception ("Post not found");
        }

        foreach (var tagModel in tagModels)
        {
            var tag = _mapper.Map<Tag>(tagModel);
            
            await _unitOfWork.TagRepository.AddAsync(tag);

            post.PostTags.Add(new PostTag()
            {
                Tag = tag,
                Post = post
            });
            
            
        }       
        
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<TagModel>> GetTagsAsync(int postId)
    {
        Post post = (await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostTags"));

        if ( post == null)
        {
            throw new Exception ("Post not found");
        }
        
        var tagsIds = post.PostTags.Select(pt => pt.TagId);
        var tags = await _unitOfWork.TagRepository.GetAllAsync(t => tagsIds.Contains(t.Id));
        
        List<TagModel> tagModels = new List<TagModel>();

        foreach (var item in post.PostTags.Select(pt => pt.Tag))
            tagModels.Add(_mapper.Map<TagModel>(item));

        return tagModels;
    }
    
    public async Task UpdateAsync(PostModel model)
    {
        _unitOfWork.PostRepository.Update(_mapper.Map<Post>(model));
        await _unitOfWork.SaveAsync();

    }

    public async Task DeleteAsync(int modelId)
    {
        await _unitOfWork.PostRepository.Delete(modelId);
        await _unitOfWork.SaveAsync();

    }

    public IEnumerable<PostModel> GetUserPostsAsync(int userId)
    {
        IEnumerable<Post> posts =  _unitOfWork.PostRepository.GetByValueAsync(post => post.UserId == userId,"User");

        var postModels = new List<PostModel>();

        foreach (var post in posts)
        {
            postModels.Add(_mapper.Map<PostModel>(post));
        }

        return postModels;
    }

    public async Task<IEnumerable<PostModel>> GetPostsSearch(string text)
    {
        IEnumerable<Post> posts =  await _unitOfWork.PostRepository.GetAllAsync( p => (p.Title.Contains(text) || p.Content.Contains(text)) && p.PostStatus == PostStatus.Published);
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));
        
        return postModels;
    }
}
