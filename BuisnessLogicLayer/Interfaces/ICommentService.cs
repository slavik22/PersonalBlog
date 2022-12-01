using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

public interface ICommentService : ICrud<CommentModel>
{
    public IEnumerable<CommentModel> GetPostComments(int postId);
}