// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-05-2022
// ***********************************************************************
// <copyright file="UnitOFWork.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Data;

/// <summary>
/// Class UnitOfWork.
/// Implements the <see cref="System.IDisposable" />
/// Implements the <see cref="IUnitOfWork" />
/// </summary>
/// <seealso cref="System.IDisposable" />
/// <seealso cref="IUnitOfWork" />
public class UnitOfWork : IDisposable, IUnitOfWork
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly PersonalBlogDbContext _context = new PersonalBlogDbContext();

    /// <summary>
    /// The user repository
    /// </summary>
    private IRepository<User>? _userRepository;
    /// <summary>
    /// The post repository
    /// </summary>
    private IRepository<Post>? _postRepository;
    /// <summary>
    /// The comment repository
    /// </summary>
    private IRepository<Comment>? _commentRepository;
    /// <summary>
    /// The tag repository
    /// </summary>
    private IRepository<Tag>? _tagRepository;
    /// <summary>
    /// The category repository
    /// </summary>
    private IRepository<Category>? _categoryRepository;

    /// <summary>
    /// Gets the user repository.
    /// </summary>
    /// <value>The user repository.</value>
    public IRepository<User> UserRepository
    {
        get
        {
            this._userRepository ??= new GenericRepository<User>(_context);
            return _userRepository;
        }
    }
    /// <summary>
    /// Gets the post repository.
    /// </summary>
    /// <value>The post repository.</value>
    public IRepository<Post> PostRepository
    {
        get
        {
            this._postRepository ??= new GenericRepository<Post>(_context);
            return _postRepository;
        }
    }
    /// <summary>
    /// Gets the comment repository.
    /// </summary>
    /// <value>The comment repository.</value>
    public IRepository<Comment> CommentRepository
    {
        get
        {
            this._commentRepository ??= new GenericRepository<Comment>(_context);
            return _commentRepository;
        }
    }
    /// <summary>
    /// Gets the tag repository.
    /// </summary>
    /// <value>The tag repository.</value>
    public IRepository<Tag> TagRepository
    {
        get
        {
            this._tagRepository ??= new GenericRepository<Tag>(_context);
            return _tagRepository;
        }
    }
    /// <summary>
    /// Gets the category repository.
    /// </summary>
    /// <value>The category repository.</value>
    public IRepository<Category> CategoryRepository
    {
        get
        {
            this._categoryRepository ??= new GenericRepository<Category>(_context);
            return _categoryRepository;
        }
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// The disposed
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    private void Dispose(bool disposing)
    {
        if (!this._disposed || disposing)
        {
                _context.Dispose();
        }
        this._disposed = true;
    }

    /// <summary>
    /// Disposes this instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}