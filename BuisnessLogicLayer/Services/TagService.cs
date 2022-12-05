using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class TagService : ITagService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TagService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TagModel>> GetAllAsync()
    {
        IEnumerable<Tag> tags =  await _unitOfWork.TagRepository.GetAllAsync();
        List<TagModel> tagModels = new List<TagModel>();

        foreach (var tag in tags)
        {
            tagModels.Add(_mapper.Map<TagModel>(tag));
        }

        return tagModels;

    }

    public async Task<TagModel> GetByIdAsync(int id)
    {
        return _mapper.Map<TagModel>(await _unitOfWork.TagRepository.GetByIdAsync(id));
    }

    public async Task AddAsync(TagModel model)
    {
        await _unitOfWork.TagRepository.AddAsync(_mapper.Map<Tag>(model));
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(TagModel model)
    {
        _unitOfWork.TagRepository.Update(_mapper.Map<Tag>(model));
        await _unitOfWork.SaveAsync();

    }

    public async Task DeleteAsync(int modelId)
    {
       await _unitOfWork.TagRepository.Delete(modelId);
       await _unitOfWork.SaveAsync();

    }
    
    public async Task AddTagAsync(int postId, TagModel tagModel)
    {
        Post post = await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostTags");
        
        if ( post == null)
        {
            throw new Exception ("Post not found");
        }
        
        Tag tag = await _unitOfWork.TagRepository.GetByValueOneAsync(tag => tag.Title == tagModel.Title);

         if (tag == null)
         {
             tag = _mapper.Map<Tag>(tagModel);
             await _unitOfWork.TagRepository.AddAsync(tag);

         }
        
        post.PostTags.Add(new PostTag()
        {
            Tag = tag,
            Post = post
        });
            
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
}