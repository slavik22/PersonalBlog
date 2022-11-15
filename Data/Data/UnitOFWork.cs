using Data.Entities;
using Data.Interfaces;

namespace Data.Data;

public class UnitOFWork : IDisposable, IUnitOfWork
{
    private PersonalBlogDbContext context = new PersonalBlogDbContext();
    
    private GenericRepository<User> userRepository;
    private GenericRepository<Post> postRepository;
    private GenericRepository<Comment> commentRepository;
    private GenericRepository<Tag> tagRepository;
    
    public GenericRepository<User> UserRepository
    {
        get
        {

            if (this.userRepository == null)
            {
                this.userRepository = new GenericRepository<User>(context);
            }
            return userRepository;
        }
    }
    public GenericRepository<Post> PostRepository
    {
        get
        {

            if (this.postRepository == null)
            {
                this.postRepository = new GenericRepository<Post>(context);
            }
            return postRepository;
        }
    } 
    public GenericRepository<Comment> CommentRepository
    {
        get
        {

            if (this.commentRepository == null)
            {
                this.commentRepository = new GenericRepository<Comment>(context);
            }
            return commentRepository;
        }
    }
    public GenericRepository<Tag> TagRepository
    {
        get
        {

            if (this.tagRepository == null)
            {
                this.tagRepository = new GenericRepository<Tag>(context);
            }
            return tagRepository;
        }
    }
    
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}