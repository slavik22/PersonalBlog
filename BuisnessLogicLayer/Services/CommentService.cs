using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IEnumerable<CommentModel> GetPostComments(int postId)
    {
        IEnumerable<Comment> comments =  _unitOfWork.CommentRepository.GetByValueAsync(c => c.PostId == postId);

        var commentModels = new List<CommentModel>();

        foreach (var comment in comments)
        {
            commentModels.Add(_mapper.Map<CommentModel>(comment));
        }

        return commentModels;

    }

    public async Task<IEnumerable<CommentModel>> GetAllAsync()
    {
        IEnumerable<Comment> comments =  await _unitOfWork.CommentRepository.GetAllAsync();
        List<CommentModel> commentModels = new List<CommentModel>();

        foreach (var item in comments)
            commentModels.Add(_mapper.Map<CommentModel>(item));

        return commentModels;

    }

    public async Task<CommentModel> GetByIdAsync(int id)
    {
        return _mapper.Map<CommentModel>(await _unitOfWork.CommentRepository.GetByIdAsync(id));
    }

    public async Task AddAsync(CommentModel model)
    {
        await _unitOfWork.CommentRepository.AddAsync(_mapper.Map<Comment>(model));
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(CommentModel model)
    {
        _unitOfWork.CommentRepository.Update(_mapper.Map<Comment>(model));
        await _unitOfWork.SaveAsync();

    }

    public async Task DeleteAsync(int modelId)
    {
        _unitOfWork.CommentRepository.Delete(modelId);
        await _unitOfWork.SaveAsync();

    }
}