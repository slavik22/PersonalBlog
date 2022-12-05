using DataAccessLayer.Data;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IUnitOfWork
{
    GenericRepository<User> UserRepository {get;}
    GenericRepository<Post> PostRepository { get; } 
    GenericRepository<Comment> CommentRepository {get;}
    GenericRepository<Tag> TagRepository{get;}
    GenericRepository<Category> CategoryRepository{get;}
    Task SaveAsync();

}