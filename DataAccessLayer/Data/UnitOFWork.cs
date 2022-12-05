using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Data;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly PersonalBlogDbContext _context = new PersonalBlogDbContext();
    
    private GenericRepository<User>? _userRepository;
    private GenericRepository<Post>? _postRepository;
    private GenericRepository<Comment>? _commentRepository;
    private GenericRepository<Tag>? _tagRepository;
    private GenericRepository<Category>? _categoryRepository;
    
    public GenericRepository<User> UserRepository
    {
        get
        {
            this._userRepository ??= new GenericRepository<User>(_context);
            return _userRepository;
        }
    }
    public GenericRepository<Post> PostRepository
    {
        get
        {
            this._postRepository ??= new GenericRepository<Post>(_context);
            return _postRepository;
        }
    } 
    public GenericRepository<Comment> CommentRepository
    {
        get
        {
            this._commentRepository ??= new GenericRepository<Comment>(_context);
            return _commentRepository;
        }
    }
    public GenericRepository<Tag> TagRepository
    {
        get
        {
            this._tagRepository ??= new GenericRepository<Tag>(_context);
            return _tagRepository;
        }
    }
    public GenericRepository<Category> CategoryRepository
    {
        get
        {
            this._categoryRepository ??= new GenericRepository<Category>(_context);
            return _categoryRepository;
        }
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool _disposed = false;

    private void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}